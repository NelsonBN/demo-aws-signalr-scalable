namespace Demo.AWS.SignalR.Server.DTOs
{
    public record Metadata
    {
        public string AWSInstanceId { get; set; }
        public string MachineName { get; set; }
    }
}