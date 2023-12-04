namespace SqsMessageConsummer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SqsService _qsService;
        private readonly DynamoService _dynamoService;
        private readonly string sqsUrl_fifo = "https://sqs.us-east-2.amazonaws.com/131949953531/BtgQueue.fifo";
        private readonly string sqsUrl = "https://sqs.us-east-2.amazonaws.com/131949953531/BtgQueue";

        public Worker(ILogger<Worker> logger, SqsService _qService, DynamoService dynamo)
        {
            _logger = logger;
            _qsService = _qService;
            _dynamoService = dynamo;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var client = _qsService.getClient();

                var result = _qsService.GetMessage(client, sqsUrl);

                if (result != null)
                {
                    foreach (var message in result.Result.Messages)
                    {
                        var text = message.Body;

                        var msg = new MessageDto
                        {
                            Id = message.MessageId,
                            Message = text
                        };


                        if(_dynamoService.createItem(msg).Result)
                        {
                            _qsService.DeleteMessage(client, message, sqsUrl);

                        }
                        Console.WriteLine(text);
                    }
                }

                //await Task.Delay(1000, stoppingToken);
            }
        }

    }
}