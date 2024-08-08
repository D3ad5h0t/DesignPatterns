public class EmailService
{
    public class EmailBuilder
    {
        private readonly Email email;
        public EmailBuilder(Email email)
        {
            this.email = email;
        }

        public EmailBuilder From(string from)
        {
            email.From = from;
            return this;
        }

        public EmailBuilder To(string to)
        {
            email.To = to;
            return this;
        }

        public EmailBuilder Subject(string subject)
        {
            email.Subject = subject;
            return this;
        }

        public EmailBuilder Body(string body)
        {
            email.Body = body;
            return this;
        }
    }
    
    public class Email
    {
        public string From;

        public string To;

        public string Subject;

        public string Body;
    }

    private void SendEmailInternal(Email email)
    {
        //
    }
    
    public void SendEmail(Action<EmailBuilder> builder)
    {
        var email = new Email();
        builder(new EmailBuilder(email));
        SendEmailInternal(email);
    }
}