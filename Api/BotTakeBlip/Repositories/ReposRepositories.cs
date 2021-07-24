using BotTakeBlip.Dtos.Responses;
using BotTakeBlip.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BotTakeBlip.Services
{
    public class ReposRepositories : IReposRepositories
    {

         
        public List<Repos> ListRepos(ILogger _logger, DefaultQueryParams defaultQueryParams)
        {
            try
            {
                string url = $"https://api.github.com/users/takenet/repos?";
                if (defaultQueryParams.direction != "desc")
                    url += $"direction={defaultQueryParams.direction}&";
                if (defaultQueryParams.per_page != 30)
                    url += $"per_page={defaultQueryParams.per_page}&";
                if (defaultQueryParams.page != 1)
                    url += $"page={defaultQueryParams.page}&";
                    url += $"sort={defaultQueryParams.sort}&";
                _logger.LogInformation($"ListRepos running :: info url {url}");

                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    _logger.LogInformation("ListRepos ::  Successful");

                    List<Repos> listRepos = JsonConvert.DeserializeObject<List<Repos>>(response.Content);
                    return listRepos;
                }
                else
                {
                    _logger.LogWarning(response.ErrorException, $"ListRepos :: error : {response.Content}");
                    throw response.ErrorException ?? new ArgumentNullException("Api.GitHub", response.Content);
                }


            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "ListMachine :: error : an error occurred while performing this operation");
                throw;
            }
        }

        public Repos GetReposByFullName(ILogger _logger, string name)
        {
            try
            {
                string url = $"https://api.github.com/repos/takenet/{name}";
   
                _logger.LogInformation($"GetReposByFullName running :: info :: reposName {name} :: url {url}");


                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);


                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    _logger.LogInformation("GetReposByFullName :  Successful");

                    Repos repo = JsonConvert.DeserializeObject<Repos>(response.Content);
                    return repo;
                }
                else
                {
                    _logger.LogWarning(response.ErrorException, $"GetReposByFullName :: error : {response.Content}");
                    throw response.ErrorException ?? new ArgumentNullException("ApiKace", response.Content);
                }


            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "GetReposByFullName :: error : an error occurred while performing this operation");
                throw;
            }
        }


    }
}
