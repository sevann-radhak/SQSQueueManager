using ConsoleApp.Model;
using Manager.Interface;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    public interface IAWSSQSAppService : IAWSSQSService<ServiceModel>
    {
        public Task RunAsync();
    }
}
