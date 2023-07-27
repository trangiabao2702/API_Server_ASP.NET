using MimeKit;

namespace API_Server.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; } = string.Empty; 
        public string Body { get; set; } = string.Empty;
        public Message(IEnumerable<string> to, string subject, string body)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(m => new MailboxAddress("email", m)));
            Subject = subject;
            Body = body;
        }
    }
}
