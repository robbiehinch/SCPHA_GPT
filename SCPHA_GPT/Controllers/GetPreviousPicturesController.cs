using Microsoft.AspNetCore.Mvc;
using OpenAI.Images;
using SCPHA_GPT.Interfaces;
using SCPHA_GPT.Persistence;
using System;

namespace SCPHA_GPT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetPreviousPicturesController : ControllerBase
    {

        private readonly ILogger<GetPreviousPicturesController> _logger;
        private readonly SCPHAContext dbContext;
        private readonly int pageSize;

        public GetPreviousPicturesController(ILogger<GetPreviousPicturesController> logger, SCPHAContext dbContext, int pageSize = 20)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.pageSize = pageSize;
        }

        [HttpGet(Name = "GetPictures")]
        public async Task<IEnumerable<GeneratedPortAuthorityImage>> Get(int page=0)
        {
            _logger.LogDebug($"Getting page: {page}...");
            return dbContext.Images
                .Skip(page * pageSize)
                .Take(pageSize);
        }
    }
}