using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotTakeBlip.Dtos.Responses
{
    public abstract class DefaultObject
    {
        public string[] schemas { get; set; }
        public int id { get; set; }   
    }
}
