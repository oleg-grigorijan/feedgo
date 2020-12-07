namespace FeedControl.Controllers
{
    public class FeedRequestForm
    {
        public string? Urls { get; set; }
        public string? ContentKeyWords { get; set; }
        public string? Categories { get; set; }
        public string? Emails { get; set; }

        public bool IsEmpty()
        {
            return Urls == null && ContentKeyWords == null && Categories == null && Emails == null;
        }
    }
}