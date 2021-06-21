using Amazon.SQS;
using Amazon.SQS.Model;
using Manager.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services
{
    public class AWSSQSHelper : IAWSSQSHelper
    {
        private readonly IAmazonSQS _sqs;
        private readonly AWSSQSServiceConfiguration _settings;

        public AWSSQSHelper(
           IAmazonSQS sqs,
           IOptions<AWSSQSServiceConfiguration> settings)
        {
            _sqs = sqs;
            _settings = settings.Value;
        }

        //public async Task<bool> SendMessageAsync(UserDetail userDetail)
        //{
        //    try
        //    {
        //        string message = JsonConvert.SerializeObject(userDetail);
        //        var a = _settings.AWSSQS.QueueUrl;

        //        var sendRequest = new SendMessageRequest(_settings.AWSSQS.QueueUrl, message);
        //        // Post message or payload to queue
        //        var sendResult = await _sqs.SendMessageAsync(sendRequest);

        //        return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<ICollection<Message>> ReceiveMessageAsync()
        {
            try
            {
                //Create New instance
                ReceiveMessageRequest request = new()
                {
                    QueueUrl = _settings.AWSSQS.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5
                };

                //CheckIs there any new message available to process
                ReceiveMessageResponse result = await _sqs.ReceiveMessageAsync(request);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (Exception ex)
            {
                // TODO: what to do here?
                throw new Exception(ex.Message);
            }
        }

        //public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
        //{
        //    try
        //    {
        //        //Deletes the specified message from the specified queue
        //        var deleteResult = await _sqs.DeleteMessageAsync(_settings.AWSSQS.QueueUrl, messageReceiptHandle);
        //        return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
