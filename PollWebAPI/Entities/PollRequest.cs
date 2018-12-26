using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class PollRequest
    {
        public string  poll_description { get; set; }
        public List<string> options { get; set; }
    }
}
