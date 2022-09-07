namespace disneyapi.Models
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string apikey { get; set; } = "APIKEY";
        public string From { get; set; } = "kiralawlietk@gmail.com";

    }
}
