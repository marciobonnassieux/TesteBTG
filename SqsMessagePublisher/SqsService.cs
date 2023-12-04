using Amazon.SQS.Model;
using Amazon.SQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;

namespace SqsMessagePublisher
{
    public class SqsService// : SqsConfiguration
    {
        // Method to put a message on a queue
        // Could be expanded to include message attributes, etc., in a SendMessageRequest
        public async Task SendMessage(
          IAmazonSQS sqsClient, string qUrl, string messageBody)
        {
            try
            {
            SendMessageResponse responseSendMsg =
              sqsClient.SendMessageAsync(qUrl, messageBody).Result;
            Console.WriteLine($"Message added to queue\n  {qUrl}");
            Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
