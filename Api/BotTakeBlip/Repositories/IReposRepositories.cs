using BotTakeBlip.Dtos.Responses;
using BotTakeBlip.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BotTakeBlip.Services
{
    public interface IReposRepositories
    {
        List<Repos> ListRepos(ILogger _logger, DefaultQueryParams defaultQueryParams);

        Repos GetReposByFullName(ILogger _logger, string name);
    }
}
