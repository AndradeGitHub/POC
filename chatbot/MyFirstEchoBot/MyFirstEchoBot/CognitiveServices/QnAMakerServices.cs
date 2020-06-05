using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using RestSharp;

using MyFirstEchoBot.Models;

namespace MyFirstEchoBot.CognitiveServices
{
    public class QnAMakerServices
    {
        private static IConfigurationRoot Configuration { get; set; }

        private dynamic builder;

        private string qnAMakerHost;
        private string qnowledgeBaseId;
        private string qnAMakerEndPointKey;

        private string FormatJson;
        private string AnswerNotFound;

        public QnAMakerServices()
        {
            builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            qnAMakerHost = Configuration["QnAMakerHost"];
            qnowledgeBaseId = Configuration["QnAMakerKnowledgeBaseId"];
            qnAMakerEndPointKey = Configuration["QnAMakerEndPointKey"];

            FormatJson = "application/json";
            AnswerNotFound = "no good match found in kb.";
        }

        public string GetAnswer(string query)
        {
            var client = new RestClient(qnAMakerHost + "/knowledgebases/" + qnowledgeBaseId + "/generateAnswer");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "EndpointKey " + qnAMakerEndPointKey);
            request.AddParameter(FormatJson, "{\"question\": \"" + query + "\"}", ParameterType.RequestBody);

            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<QnAMakerModel>(response.Content);

            if (result.Answers.Count > 0)
            {
                var ret = result.Answers[0].Answer;
                var score = result.Answers[0].Score;
                if (!ret.ToLower().Equals(AnswerNotFound) && score > 40)
                    return ret;
            }

            return AnswerNotFound;
        }
    }
}