using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextBasedAdventure.Models;
using TextBasedAdventure.Services;

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {

        private readonly IDapperService _dapper;

        public EventController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public int Create(EventDto Event)
        {
            var result = _dapper.Insert<int>(
                "insert into Event(EventSummary, EventAlreadyEncountered, EventPassed, MonsterId, ItemId, ZoneId)"
                + " values(@EventSummary, @EventAlreadyEncountered, @EventPassed, @MonsterId, @ItemId, @ZoneId);"
                + " select cast(scope_identity() as int)",
                //+ $" values('{Event.EventSummary}', {(Event.EventAlreadyEncountered ? 1 : 0)}, {(Event.EventPassed ? 1 : 0)}, {Event.Monster?.MonsterId.ToString() ?? "null"}, " 
                //+ $"{Event.Item?.ItemId.ToString() ?? "null"}, {Event.Zone.ZoneId})", 
                new DynamicParameters(Event), commandType: CommandType.Text);
            return result;
        }

        [Route("GetById/{eventId}")]
        public Event GetById(int eventId)
        {
            var result = _dapper.Get<Event>($"select * from Event where EventId = {eventId}", null, commandType: CommandType.Text);
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Event>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Event>("select * from Event", null, commandType: CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetRandom))]
        public async Task<Event> GetRandom(Player p)
        {
            // Had an idea on this one but wasn't 100% sure of it. But we may want to check monster level and if it's a crazy amount higher than the players then we may not want to select that
            // event. 
            Random rand = new Random();
            //Just throwing this in here for now, but at some point we'll have to create some sort of algorithm for dynamically switching zones
            var list = await Task.FromResult(_dapper.GetAll<Event>($"select * from Event where ZoneId = {p.CurrentZone.ZoneId}", null, commandType: CommandType.Text));
            return list[rand.Next(list.Count)];
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int EventId)
        {
            var result = await Task.FromResult(_dapper.Execute($"delete from Event where EventId = {EventId}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpPatch(nameof(Update))]
        public async Task<Event> Update(Event Event)
        {
            var result = await Task.FromResult(_dapper.Update<Event>($"update Event set EventSummary = '{Event.EventSummary}', EventAlreadyEncountered = {(Event.EventAlreadyEncountered ? 1 : 0)}, " +
            $" EventPassed = {(Event.EventPassed ? 1 : 0)}, MonsterId = {Event.Monster?.MonsterId.ToString() ?? "null"}, ItemId = {Event.Item?.ItemId.ToString() ?? "null"}, " +
            $"ZoneId = {Event.Zone.ZoneId} where EventId = {Event.EventId}", null, commandType: CommandType.Text));
            return result;
        }
    }
}
