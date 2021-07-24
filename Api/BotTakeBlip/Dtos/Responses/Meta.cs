using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTakeBlip.Dtos.Responses
{
    public class Meta
    {
        public string resourceType { get; set; }
        public DateTime created { get; set; }
        public DateTime lastModified { get; set; }
        public string location { get; set; }
    }
}
