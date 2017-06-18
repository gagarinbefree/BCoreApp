using System.Threading.Tasks;

namespace BCoreIdentity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
