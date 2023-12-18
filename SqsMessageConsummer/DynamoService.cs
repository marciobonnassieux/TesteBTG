using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace SqsMessageConsummer
{
    public class DynamoService
    {
        private readonly string tableName = "messages";
        private readonly string partitionKey = "dc3702f36943609c766a673f556afba45c74e9154a8d31ab77564e9393417f31";



        public IAmazonDynamoDB getClient()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("",""); 
            //AWS KEY

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(awsCredentials, Amazon.RegionEndpoint.USEast2);
            return client;
        }


        public async Task<bool> createItem(MessageDto message)
        {
            //var request = new PutItemRequest
            //{
            //    TableName = tableName,
            //    Item = new Dictionary<string, AttributeValue>()
            //    {
            //        { "Id", new AttributeValue { N = "201" }},
            //        { "Title", new AttributeValue { S = "Book 201 Title" }},
            //        { "ISBN", new AttributeValue { S = "11-11-11-11" }},
            //        { "Price", new AttributeValue { S = "20.00" }},
            //        {
            //        "Authors",
            //        new AttributeValue
            //        { SS = new List<string>{"Author1", "Author2"}   }
            //        }
            //    }
            //};
            try
            {
                var messageJson = JsonSerializer.Serialize(message);
                var itemAsDocument = Document.FromJson(messageJson);
                var itemAttributes = itemAsDocument.ToAttributeMap();


                var request = new PutItemRequest
                {
                    TableName = tableName,
                    Item = itemAttributes
                };
                var resp = getClient().PutItemAsync(request).Result;
                return resp.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
