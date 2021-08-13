using Demo.AWS.SignalR.Server.DTOs;
using System.Net.Http;

namespace Demo.AWS.SignalR.Server.Services
{
    public class MetadataService : IMetadataService
    {
        private string _awsInstanceId { get; set; }

        public MetadataService()
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = System.TimeSpan.FromSeconds(3);
                this._awsInstanceId = client.GetStringAsync("http://169.254.169.254/latest/meta-data/instance-id").GetAwaiter().GetResult();
            }
            catch
            {
                this._awsInstanceId = "UNKNOW";
            }
        }

        public string GetAWSInstanceId()
        {
            return this._awsInstanceId;
        }

        public string GetMachineName()
        {
            return System.Environment.MachineName;
        }

        public Metadata GetMetadata()
        {
            return new Metadata
            {
                AWSInstanceId = this.GetAWSInstanceId(),
                MachineName = this.GetMachineName()
            };
        }
    }
}