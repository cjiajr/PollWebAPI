﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollWebAPI.Entities
{
    public class PollStats
    {
        public int views { get; set; }
        public IList<PollOptions> votes { get; set; }
    }
}
