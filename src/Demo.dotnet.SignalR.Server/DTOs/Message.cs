namespace Demo.AWS.SignalR.Server.DTOs
{
    public class Message
    {
        public string Text { get; init; }
        public string AWSInstanceId { get; init; }
        public string MachineName { get; init; }

        public Message(string text, string awsInstanceId, string machineName)
        {
            this.Text = text;
            this.AWSInstanceId = awsInstanceId;
            this.MachineName = machineName;
        }
    }
}