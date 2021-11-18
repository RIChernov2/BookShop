using System.Threading.Tasks;

namespace Data.MessageBrokerClients
{
    public interface IRpcClient
    {
        public Task<string> CallAsync(string command, string message);
    }
}