namespace FeedMailer
{
    public class EmailSenderOptions
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
    }
}