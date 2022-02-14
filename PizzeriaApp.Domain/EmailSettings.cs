namespace PizzeriaApp.Domain
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpServerPort { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailDisplayName { get; set; }
        public string SenderName { get; set; }

        public EmailSettings()
        {

        }

        public EmailSettings(string SmtpServer, string SmtpUsername, string SmtpPassword, int SmtpServerPort)
        {
            this.SmtpServer = SmtpServer;
            this.SmtpUsername = SmtpUsername;
            this.SmtpPassword = SmtpPassword;
            this.SmtpServerPort = SmtpServerPort;
        }
    }
}
