using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class Poll
    {
        public int poll_id { get; set; }
        public string  poll_description { get; set; }
        public int poll_views { get; set; }
        public IList<PollOptions> options { get; set; }
    }
}
