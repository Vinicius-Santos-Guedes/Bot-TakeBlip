using BotTakeBlip.Dtos;
using BotTakeBlip.Dtos.Responses;
using BotTakeBlip.Dtos.Uber;

namespace BotTakeBlip
{
    public static class Extensions
    {
        public static ReposDto AsDto(this Repos repos, Microsoft.AspNetCore.Mvc.IUrlHelper url)
        {
            return new ReposDto
            {
                schemas = new string[] { "urn:ietf:params:scim:schemas:github:Repos" },
                id = repos.id,
                description = repos.description,
                full_name = repos.full_name,
                avatar_url = repos.owner.avatar_url,
                language = repos.language,
                meta = new Meta
                {
                    created = repos.created_at,
                    lastModified = repos.updated_at,
                    resourceType = "Repos",
                    location= url.Link("GetRepos", new { }) + $"/{repos.name}"
                }

            };
        }
    }
}
