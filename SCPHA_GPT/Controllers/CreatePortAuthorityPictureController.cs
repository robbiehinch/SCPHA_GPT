using Microsoft.AspNetCore.Mvc;
using SCPHA_GPT.Interfaces;
using SCPHA_GPT.Persistence;
using System;

namespace SCPHA_GPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatePortAuthorityPictureController : ControllerBase
    {

        private readonly ILogger<CreatePortAuthorityPictureController> _logger;
        private readonly IChatGPT chatGpt;
        private readonly IDallE2 dallE2;
        private readonly SCPHAContext dbContext;

        public CreatePortAuthorityPictureController(ILogger<CreatePortAuthorityPictureController> logger, IChatGPT chatGpt, IDallE2 dallE2, SCPHAContext dbContext)
        {
            _logger = logger;
            this.chatGpt = chatGpt;
            this.dallE2 = dallE2;
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "CreatePicture")]
        public async Task<IEnumerable<GeneratedPortAuthorityImage>> Create(string mood)
        {
            _logger.LogDebug($"Generating description for mood: {mood}...");
            var descriptions = await chatGpt.Generate(mood);

            var imageTasks = descriptions
                //.Take(1)    //could take more but exceeds quota
                .Select(_ =>
                {
                    _logger.LogDebug($"Generating image for description: {_}...");
                    var descriptionTrimmed = _.Substring(0, 990);
                    return dallE2.CreateImageFromDescription(descriptionTrimmed)
                        .ContinueWith(_ => new {
                            Description = descriptionTrimmed,
                            Image = _.IsCompleted ? _.Result.ToList() : new List<string>(),
                            Error =  _.IsCompleted ? string.Empty : _.Exception.ToString()
                        });
                });

            var allResults = await Task.WhenAll(imageTasks);
            var retVal =  allResults.SelectMany(_ => _.Image.Any()
                ? _.Image.Select(uri => new GeneratedPortAuthorityImage { RequestTime = DateTime.UtcNow, ImageUri = new Uri(uri), Prompt = _.Description, Error = _.Error })
                : new []{new GeneratedPortAuthorityImage { Prompt = _.Description, Error = _.Error }}
                ).ToList();


            await dbContext.Images.AddRangeAsync(retVal);
            await dbContext.SaveChangesAsync();

            return retVal;
        }
    }
}