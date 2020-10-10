using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using TextBasedAdventure.Services;
using System.Data;
using TextBasedAdventure.Models;

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IDapperService _dapper;
        public PlayerController(IDapperService dapper)
        {
            _dapper = dapper;
        }

        [HttpPost(nameof(Create))]
        public async Task<Player> Create(Player data)
        {
            //this is still missing current zone
            var result = await Task.FromResult(_dapper.Insert<Player>($"insert into Player(PlayerName, [Level], Health, MaxHealth, Strength, Defense, Gold, Experience, CurrentZoneId)" +
                $"values('{data.PlayerName}', {data.Level}, {data.Health}, {data.MaxHealth}, {data.Strength}, {data.Defense}, {data.Gold}, {data.Experience}, {data.CurrentZone.ZoneId})", null, CommandType.Text));
            return result;
        }

        [HttpGet(nameof(GetById))]
        public async Task<Player> GetById(int Id)
        {
            var result = await Task.FromResult(_dapper.Get<Player>($"Select * from [Player] where PlayerId = {Id}", null, commandType: CommandType.Text));
            return result;
        }

        [HttpDelete(nameof(Delete))]
        public async Task<int> Delete(int Id)
        {
            var result = await Task.FromResult(_dapper.Execute($"Delete [Player] Where PlayerId = {Id}", null, commandType: CommandType.Text));
            return result;
        }

		[HttpPatch(nameof(Update))]
		public Task<Player> Update(Player data)
		{
            var updatedPlayer = Task.FromResult(_dapper.Update<Player>($"update Player set PlayerName = '{data.PlayerName}', [Level] = {data.Level}," +
                $" Health = {data.Health}, MaxHealth = {data.MaxHealth}, Strength = {data.Strength}, Defense = {data.Defense}, Gold = {data.Gold}, Experience = {data.Experience}," +
                $" CurrentZoneId = {data.CurrentZone.ZoneId} where PlayerId = {data.PlayerId}", null, CommandType.Text));
			return updatedPlayer;
		}
	}
}

