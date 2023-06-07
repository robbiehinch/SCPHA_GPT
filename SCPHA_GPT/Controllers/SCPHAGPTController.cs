using Microsoft.AspNetCore.Mvc;
using SCPHA_GPT.Interfaces;
using System;

namespace SCPHA_GPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SCPHAGPTController : ControllerBase
    {

        private readonly ILogger<SCPHAGPTController> _logger;
        private readonly IChatGPT chatGpt;
        private readonly IDallE2 dallE2;

        public SCPHAGPTController(ILogger<SCPHAGPTController> logger, IChatGPT chatGpt, IDallE2 dallE2)
        {
            _logger = logger;
            this.chatGpt = chatGpt;
            this.dallE2 = dallE2;
        }

        [HttpGet(Name = "GetSCPHAGPT")]
        public async Task<IEnumerable<SCHPAGPT>> Get(string mood)
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
            return allResults.SelectMany(_ => _.Image.Any()
            ? _.Image.Select(uri => new SCHPAGPT { ImageUri = new Uri(uri), Prompt = _.Description, Error = _.Error })
            : new []{new SCHPAGPT { Prompt = _.Description, Error = _.Error }});
        }
    }
}