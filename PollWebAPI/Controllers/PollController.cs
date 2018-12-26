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
        private const string pathDB = @"c:\temp\poll.db";

        [HttpGet("{id}")]
        public ActionResult<Poll> Get(int id)
        {

            using (var db = new LiteDatabase(pathDB))
            {
                var collection = db.GetCollection<Poll>();
                var value = collection.FindById(id);

                if (value != null)
                {
                    value.poll_views += 1;
                    collection.Update(value);
                    return value;
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
                var value = collection.FindById(id);

                if (value != null)
                {
                    var ret = new PollStats() { views = value.poll_views, votes = value.options };
                    return ret;
                }
                else
                    return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] Poll value)
        {
            using (var db = new LiteDatabase(pathDB))
            {
                db.Mapper.Entity<Poll>().DbRef(x => x.options);
                var collection = db.GetCollection<Poll>();
                collection.Insert(value);
                return value.poll_id.ToString();
            }
        }

        [HttpPost("{id}/vote")]
        public ActionResult PostVote(int id, [FromBody] int option_id)
        {
            using (var db = new LiteDatabase(pathDB))
            {
                db.Mapper.Entity<Poll>().DbRef(x => x.options);
                var collection = db.GetCollection<Poll>();
                var value = collection.Include(x => x.options).FindById(id);
                if (value != null)
                {
                    value.options.FirstOrDefault(x => x.option_id == option_id).option_qty_vote += 1;
                    collection.Update(value);
                    return Ok();
                }
                else
                    return NotFound();
            }
        }
    }
}
