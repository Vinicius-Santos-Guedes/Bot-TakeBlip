using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTakeBlip.Dtos.Responses
{
    public class DefaultList<T> where T : DefaultObject
    {
        public string[] schemas { get; set; } = { "urn:ietf:params:scim:api:messages:2.0:ListResponse" };

        public List<T> resources { get; set; }
        public int totalResults { get; set; }
        public int startIndex { get; set; }
        public int itemsPerPage { get; set; }
    }
}
