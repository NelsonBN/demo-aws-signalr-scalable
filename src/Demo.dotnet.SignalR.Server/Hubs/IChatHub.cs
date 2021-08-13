using Demo.AWS.SignalR.Server.DTOs;
using System.Threading.Tasks;

namespace Demo.AWS.SignalR.Server.Hubs
{
    public interface IChatHub
    {
        Task OnMessage(Message message);
    }
}