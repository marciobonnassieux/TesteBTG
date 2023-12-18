using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqsMessageConsummer
{
    public class SqsService
    {

        public IAmazonSQS getClient()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("", ""); //new Amazon.Runtime.BasicAWSCredentials("
                                                                                 //AKIAR5O 
                                                                                 //GUQH 
                                                                                 //5TEWM3TU3 ",
                                                                                 //"N1tM4CsiI
                                                                                 //oohglJhJ+g3
                                                                                 //UIYTot3H58Mh
                                                                                 //FLUNDgws");
            AmazonSQSClient client = new AmazonSQSClient(awsCredentials, Amazon.RegionEndpoint.USEast2);
            return client;
        }

        public async Task<ReceiveMessageResponse> GetMessage(
          IAmazonSQS sqsClient, string qUrl, int waitTime = 0)
        {
            var res =  await sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = qUrl,
                MaxNumberOfMessages = 10,
                //WaitTimeSeconds = waitTime
                // (Could also request attributes, set visibility timeout, etc.)
            });

            return res;


        }

        // Method to delete a message from a queue
        public async Task DeleteMessage(
          IAmazonSQS sqsClient, Message message, string qUrl)
        {
            Console.WriteLine($"\nDeleting message {message.MessageId} from queue...");
            await sqsClient.DeleteMessageAsync(qUrl, message.ReceiptHandle);
        }
    }
}
