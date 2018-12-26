using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class PollOptions
    {
        [BsonId]
        public int Id { get; set; }
        public int option_id { get; set; }
        public string option_description { get; set; }
        public int qty { get; set; }
    }
}
