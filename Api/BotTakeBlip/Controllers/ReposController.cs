using BotTakeBlip.Dtos.Responses;
using BotTakeBlip.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTakeBlip.Controllers
{
    [ApiController]
    [Route("github/v1/[controller]")]
    public class ReposController : ControllerBase
    {
        private readonly IReposRepositories _repos;

        private readonly ILogger<ReposController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUrlHelper _urlHelper;

        public ReposController(IConfiguration configuration, ILogger<ReposController> logger, IReposRepositories repos, IUrlHelper urlHelper)
        {
            _configuration = configuration;
            _logger = logger;
            _repos = repos;
            _urlHelper = urlHelper;
        }

        [HttpGet(Name = nameof(GetRepos))]
        public async Task<ActionResult<List<Dtos.ReposDto>>> GetRepos([FromQuery] DefaultQueryParams defaultQueryParams)
        {
            try
            {
                var repos = _repos.ListRepos(_logger, defaultQueryParams).Select(repos => repos.AsDto(_urlHelper));
                return Ok(repos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message } ?? new { Message = "não foi possível fazer a requisição na api do GitHub, por favor contate um administrador" });
            }
        }


        [HttpGet("{fullName}")]
        public async Task<ActionResult<List<Dtos.ReposDto>>> GetReposByFullName(string fullName)
        {
            try
            {
                var repos = _repos.GetReposByFullName(_logger, fullName);
                if (repos is null)
                    return NotFound();

                return Ok(repos.AsDto(_urlHelper));
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message} ??new { Message= "não foi possível fazer a requisição na api do GitHub, por favor contate um administrador" });
            }
        }
    }
}
