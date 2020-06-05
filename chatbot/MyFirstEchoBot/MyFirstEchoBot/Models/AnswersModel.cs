using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstEchoBot.Models
{
    public class AnswersModel
    {
        public List<string> Questions { get; set; }
        public string Answer { get; set; }
        public double Score { get; set; }
        public int Id { get; set; }
        public string Source { get; set; }
        public List<MetadataModel> Metadata { get; set; }
    }
}
