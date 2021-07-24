using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTakeBlip.Dtos.Responses
{
    public class DefaultQueryParams
    {
        public string sort { get; set; } = "created";
        public string direction { get; set; } = "asc";
        public int per_page { get; set; } = 30;
        public int page { get; set; } = 1;

    }
}
