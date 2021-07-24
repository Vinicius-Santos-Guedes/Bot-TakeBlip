using BotTakeBlip.Dtos.Responses;


namespace BotTakeBlip.Dtos
{
    public class ReposDto : DefaultObject
    {
        public string full_name { get; set; }
        public string description { get; set; }
        public string avatar_url { get; set; }
        public string language { get; set; }

        public Meta meta { get; set; }


    }


}
