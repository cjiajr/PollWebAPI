using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiteDB;
using PollWebAPI.Entities;

namespace PollWebAPI.Controllers
{
    [Route("poll")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private const string pathDB = @"c:\temp\polldatabase.db";

        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            using (var db = new LiteDatabase(pathDB))
            {
                db.Mapper.Entity<Poll>().DbRef(x => x.options);

                var collection = db.GetCollection<Poll>();
                var value = collection.Include(x => x.options).FindById(id);

                if (value != null)
                {
                    value.poll_views += 1;
                    collection.Update(value);

                    return new { value.poll_Id, value.poll_description, options = value.options.Select(b => new { b.option_id, b.option_description }) };                    
                }
                else
                    return NotFound();
            }
        }

        [HttpGet("{id}/stats")]
        public ActionResult<PollStats> GetStats(int id)
        {
            using (var db = new LiteDatabase(pathDB))
            {
                var collection = db.GetCollection<Poll>();
                var value = collection.Include(x => x.options).FindById(id);

                if (value != null)
                {
                    var ret = new PollStats() { views = value.poll_views, votes = value.options.Select(b => new { b.option_id, b.qty }) };
                    return ret;
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] PollRequest value)
        {

            using (var db = new LiteDatabase(pathDB))
            {
                Poll polldb = new Poll() { poll_description = value.poll_description };
                List<PollOptions> pollOptionsList = new List<PollOptions>();

                db.Mapper.Entity<Poll>().DbRef(x => x.options);

                var OptionsCollection = db.GetCollection<PollOptions>();
                for (int i = 0; i < value.options.Count; i++)
                {
                    PollOptions item = new PollOptions() { option_description = value.options[i], option_id = i + 1 };                    
                    OptionsCollection.Insert(item);

                    pollOptionsList.Add(item);
                }

                polldb.options = pollOptionsList;
                
                var collection = db.GetCollection<Poll>();
                collection.Insert(polldb);
                return polldb.poll_Id.ToString();
            }
        }

        [HttpPost("{id}/vote")]
        public ActionResult PostVote(int id, [FromBody] int option_id)
        {
            using (var db = new LiteDatabase(pathDB))
            {
                var collection = db.GetCollection<Poll>().Include(x => x.options);
                var value = collection.FindById(id);

                if (value != null)
                {
                    var OptionRef = value.options.FirstOrDefault(x => x.option_id == option_id);
                    OptionRef.qty += 1;
                    var OptionsCollection = db.GetCollection<PollOptions>();
                    OptionsCollection.Update(OptionRef);
                    return Ok();
                }
                else
                    return NotFound();
            }
        }
    }
}
