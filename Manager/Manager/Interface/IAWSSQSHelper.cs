using Amazon.SQS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interface
{
    public interface IAWSSQSHelper
    {
        //Task<bool> SendMessageAsync(UserDetail userDetail);

        public Task<ICollection<Message>> ReceiveMessageAsync();

        //Task<bool> DeleteMessageAsync(string messageReceiptHandle);
    }
}
