using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SqsMessagePublisher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly SqsService _qsService;
    private readonly string sqsUrl_fifo = "https://sqs.us-east-2.amazonaws.com/131949953531/BtgQueue.fifo";
    private readonly string sqsUrl = "https://sqs.us-east-2.amazonaws.com/131949953531/BtgQueue";
    //private readonly string sqsUrl =    "https://sqs.us-east-2.amazonaws.com/131949953531/BtgQueue2.fifo";


    public Worker(ILogger<Worker> logger, SqsService sqsService)
    {
        _logger = logger;
        _qsService = sqsService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var qtd = 5;

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = "Worker running at: " + DateTimeOffset.Now;

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(message);


                Thread main = Thread.CurrentThread;
                main.Name = "main";

                for (int i = 0; i < qtd; i++)
                {
                    var name = $"Thread_{i}";
                    Thread t = new Thread(() => messaging(name));
                    t.Start();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    public void messaging(string name)
    {

        IAmazonSQS sqs = getClient();

        #region Create queue
        //var createQueueResponse = await CreateQueue(sqs, "Teste2BTG");
        //string queueUrl = createQueueResponse.QueueUrl; 
        #endregion
        string queueUrl = sqsUrl;

        Dictionary<string, MessageAttributeValue> messageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            { "GeneratorName", new MessageAttributeValue { DataType = "String", StringValue = name } },
            { "GeneratedAt", new MessageAttributeValue { DataType = "String", StringValue = DateTime.Now.ToString()} },
        };

        string messageBody = $"Message created by {name} on {DateTime.Now}";

        //var sendMsgResponse = await SendMessage(sqs, queueUrl, messageBody, messageAttributes);
        var sendMsgResponse = SendMessage(sqs, queueUrl, messageBody, null).Result;

        //Task.Delay(1000);
    }

    ///// <summary>
    ///// Creates a new Amazon SQS queue using the queue name passed to it
    ///// in queueName.
    ///// </summary>
    ///// <param name="client">An SQS client object used to send the message.</param>
    ///// <param name="queueName">A string representing the name of the queue
    ///// to create.</param>
    ///// <returns>A CreateQueueResponse that contains information about the
    ///// newly created queue.</returns>
    //public static async Task<CreateQueueResponse> CreateQueue(IAmazonSQS client, string queueName)
    //{
    //    var request = new CreateQueueRequest
    //    {
    //        QueueName = queueName,
    //        Attributes = new Dictionary<string, string>
    //            {
    //                { "DelaySeconds", "60" },
    //                { "MessageRetentionPeriod", "86400" },
    //            },
    //        Tags = new Dictionary<string, string> { { "TesteBTG", "TesteBTG"} }
    //    };

    //    var response = await client.CreateQueueAsync(request);
    //    Console.WriteLine($"Created a queue with URL : {response.QueueUrl}");

    //    return response;
    //}


    public static async Task<SendMessageResponse> SendMessage(
        IAmazonSQS client,
        string queueUrl,
        string messageBody,
        Dictionary<string, MessageAttributeValue> messageAttributes)
    {
        var sendMessageRequest = new SendMessageRequest
        {
            //DelaySeconds = 10,
            MessageAttributes = messageAttributes,
            MessageBody = messageBody,
            QueueUrl = queueUrl,
            //MessageGroupId = "TesteBTG",
            //MessageDeduplicationId = Guid.NewGuid().ToString(),
        };

        var response = await client.SendMessageAsync(sendMessageRequest);
        Console.WriteLine($"Message id : {response.MessageId}");

        return response;
    }









    private IAmazonSQS getClient()
    {
        //var chain = new CredentialProfileStoreChain();
        //AWSCredentials awsCredentials;

        ////BasicAWSCredentials basicAWSCredentials = new BasicAWSCredentials();
        //if (chain.TryGetAWSCredentials("default", out awsCredentials))
        //{
        //    // Use awsCredentials to create an Amazon S3 service client
        //    using (var client = new AmazonSQSClient(awsCredentials))
        //    {
        //        //var response = await client.ListBucketsAsync();
        //        //Console.WriteLine($"Number of buckets: {response.Buckets.Count}");

        //        return client;
        //    }
        //}

        //throw new InvalidOperationException();

        //AmazonSQSClient client = new AmazonSQSClient(new EnvironmentVariablesAWSCredentials());

        var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("", "");
        //AWS KEY

        AmazonSQSClient client = new AmazonSQSClient(awsCredentials, Amazon.RegionEndpoint.USEast2);
        return client;
    }
}
