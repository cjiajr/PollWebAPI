using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class PollOptions
    {
        public int poll_id { get; set; }
        public int option_id { get; set; }
        public string option_description { get; set; }
        public int? option_qty_vote { get; set; }
    }
}
