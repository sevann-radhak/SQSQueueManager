using Amazon.SQS.Model;
using Manager.Interface;
using Manager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class AWSSQSService<T> : IAWSSQSService<T> where T : class
    {
        private readonly IAWSSQSHelper _AWSSQSHelper;

        public AWSSQSService(IAWSSQSHelper AWSSQSHelper)
        {
            _AWSSQSHelper = AWSSQSHelper;
        }

        public async Task<ICollection<AllMessage<T>>> GetAllMessagesAsync()
        {
            ICollection<AllMessage<T>> allMessages;
            try
            {
                ICollection<Message> messages = await _AWSSQSHelper.ReceiveMessageAsync();
                allMessages = messages
                    .Select(m => new AllMessage<T>
                    {
                        MessageId = m.MessageId,
                        ReceiptHandle = m.ReceiptHandle,
                        Body = JsonConvert.DeserializeObject<T>(m.Body)
                    })?
                    .ToList();

                return allMessages;
            }
            catch (Exception ex)
            {
                //TODO: what to to here?
                throw new Exception(ex.Message);
            }
        }
    }
}
