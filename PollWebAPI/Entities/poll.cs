﻿using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class Poll 
    {
        [BsonId(true)]
        public int poll_Id { get; set; }
        public string  poll_description { get; set; }
        public int poll_views { get; set; }        
        public List<PollOptions> options { get; set; }
    }
}
