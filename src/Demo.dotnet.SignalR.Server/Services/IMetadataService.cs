using Demo.AWS.SignalR.Server.DTOs;

namespace Demo.AWS.SignalR.Server.Services
{
    public interface IMetadataService
    {
        string GetAWSInstanceId();

        string GetMachineName();

        Metadata GetMetadata();
    }
}