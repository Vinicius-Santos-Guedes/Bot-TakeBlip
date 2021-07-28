using BotTakeBlip.Dtos.Responses;
using BotTakeBlip.Dtos.Uber;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BotTakeBlip.Services
{
    public class ReposRepositories : IReposRepositories
    {

        /// <summary>
        /// Lista os repositorios na API GitHub
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="defaultQueryParams"></param>
        /// <returns></returns>
        public List<Repos> ListRepos(ILogger _logger, DefaultQueryParams defaultQueryParams)
        {
            try
            {
                string url = $"https://api.github.com/users/takenet/repos?";

                //caso a direção não for a padrão adiciona o novo valor na query
                if (defaultQueryParams.direction != "desc")
                    url += $"direction={defaultQueryParams.direction}&";
                //caso a quantidade de items por pagina não for a padrão adiciona o novo valor na query
                if (defaultQueryParams.per_page != 30)
                    url += $"per_page={defaultQueryParams.per_page}&";
                //caso a pagina não for a padrão adiciona o novo valor na query
                if (defaultQueryParams.page != 1)
                    url += $"page={defaultQueryParams.page}&";
                //adiciona a sort na query
                url += $"sort={defaultQueryParams.sort}&";


                _logger.LogInformation($"ListRepos running :: info url {url}");

                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    _logger.LogInformation("ListRepos ::  Successful");
                    //convert a resposta no tipo List<Repos>
                    List<Repos> listRepos = JsonConvert.DeserializeObject<List<Repos>>(response.Content);
                    return listRepos;
                }
                else
                {
                    _logger.LogWarning(response.ErrorException, $"ListRepos :: error : {response.Content}");
                    //caso o response.ErrorException for nulo, forma uma erro com a resposta da requisição
                    throw response.ErrorException ?? new ArgumentNullException("Api.GitHub", response.Content);
                }


            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "ListMachine :: error : an error occurred while performing this operation");
                throw;
            }
        }

        /// <summary>
        /// pega o repositorio pelo nome na API GitHub
        /// </summary>
        /// <param name="_logger"></param>
        /// <param name="name"></param>
        /// <returns></returns>
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
                    //convert a resposta no tipo Repos
                    Repos repo = JsonConvert.DeserializeObject<Repos>(response.Content);
                    return repo;
                }
                else
                {
                    //verifica se motivo de ter dado errado é pq não existe o repositório
                    if (((int)response.StatusCode) == 404)
                    {
                        _logger.LogInformation("GetReposByFullName :: error : there is no such repository :: reposName {name}");
                        return null;
                    }

                    _logger.LogWarning(response.ErrorException, $"GetReposByFullName :: error : {response.Content}");
                    //caso o response.ErrorException for nulo, forma uma erro com a resposta da requisição
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
