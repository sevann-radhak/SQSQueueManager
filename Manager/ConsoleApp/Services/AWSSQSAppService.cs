using ConsoleApp.Interface;
using ConsoleApp.Model;
using Manager.Interface;
using Manager.Model;
using Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class AWSSQSAppService : AWSSQSService<ServiceModel>, IAWSSQSAppService
    {
        public AWSSQSAppService(IAWSSQSHelper AWSSQSHelper) : base(AWSSQSHelper)
        {
        }

        public async Task RunAsync()
        {
            ICollection<AllMessage<ServiceModel>> messages = await GetAllMessagesAsync();

            Console.WriteLine($"{messages.Count} message/s found in queue.");

            try
            {
                _ = messages.Select(m =>
                {
                    Console.WriteLine($" **************************************************** ");
                    Console.WriteLine($"Message found successfuly:");
                    Console.WriteLine($"{m.Body.Id} | {m.Body.Description}");

                    return m;
                }).ToList();

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.InnerException?.Message}");
                Console.WriteLine($"{ex.InnerException?.InnerException?.Message}");
                Console.WriteLine($"{ex.Message}");

                //TODO: what to do here
                throw new Exception(ex.Message);
            }
        }
    }
}
