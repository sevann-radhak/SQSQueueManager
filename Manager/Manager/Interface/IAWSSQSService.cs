using Manager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface IAWSSQSService<T> where T : class
    {
        public Task<ICollection<AllMessage<T>>> GetAllMessagesAsync();
    }
}
