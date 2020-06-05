using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;

namespace MyFirstEchoBot.Models
{
    public partial class LuisModel : IRecognizerConvert
    {
        public enum Intent
        {
            QnAMakerMicrosoft,
            Cotacao,
            Cumprimento,
            Card,
            Sobre,
            None
        };

        public Dictionary<Intent, IntentScore> Intents;

        public (Intent intent, double score) TopIntent()
        {
            Intent maxIntent = Intent.None;
            var max = 0.0;
            foreach (var entry in Intents)
            {
                if (entry.Value.Score > max)
                {
                    maxIntent = entry.Key;
                    max = entry.Value.Score.Value;
                }
            }
            return (maxIntent, max);
        }

        public class _Entities
        {            
            public object Moeda;
            public object CardType;
        }

        public _Entities Entities;

        public string Text;
        public string AlteredText;

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public IDictionary<string, object> Properties { get; set; }

        public void Convert(dynamic result)
        {
            var app = JsonConvert.DeserializeObject<LuisModel>(JsonConvert.SerializeObject(result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            Text = app.Text;
            AlteredText = app.AlteredText;
            Intents = app.Intents;
            Entities = app.Entities;
            Properties = app.Properties;
        }
    }
}