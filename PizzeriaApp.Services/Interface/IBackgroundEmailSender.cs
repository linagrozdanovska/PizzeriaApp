using System.Threading.Tasks;

namespace PizzeriaApp.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
