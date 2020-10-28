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
            var result = await Task.FromResult(_dapper.Insert<Player>($"insert into Player(PlayerName, [Level], Health, MaxHealth, Strength, Defense, Gold, Experience, CurrentZoneId)" +
                $"values('{data.PlayerName}', {data.Level}, {data.Health}, {data.MaxHealth}, {data.Strength}, {data.Defense}, {data.Gold}, {data.Experience}, {data.CurrentZone.ZoneId})", null, CommandType.Text));
            return result;
        }

        [HttpPost(nameof(CreateWithProc))]
        public async Task<int> CreateWithProc(Player data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("PlayerName", data.PlayerName, DbType.String);
            dbparams.Add("Level", data.Level, DbType.Int32);
            dbparams.Add("Health", data.Health, DbType.Int32);
            dbparams.Add("MaxHealth", data.MaxHealth, DbType.Int32);
            dbparams.Add("Strength", data.Strength, DbType.Int32);
            dbparams.Add("Defense", data.Defense, DbType.Int32);
            dbparams.Add("Gold", data.PlayerId, DbType.Int32);
            dbparams.Add("Experience", data.Experience, DbType.Int32);
            dbparams.Add("CurrentZoneId", data.CurrentZone.ZoneId, DbType.Int32);
            var result = await Task.FromResult(_dapper.Insert<int>("[dbo].[SP_Create_Player]"
                , dbparams,
                commandType: CommandType.StoredProcedure));
            return result;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<List<Player>> GetAll()
        {
            var result = await Task.FromResult(_dapper.GetAll<Player>("select * from Player", null, commandType: CommandType.Text));
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

        [HttpPatch(nameof(UpdateWithProc))]
        public Task<int> UpdateWithProc(Player data)
        {
            var dbparams = new DynamicParameters();
            dbparams.Add("PlayerId", data.PlayerId, DbType.Int32);
            dbparams.Add("PlayerName", data.PlayerName, DbType.String);
            dbparams.Add("Level", data.Level, DbType.Int32);
            dbparams.Add("Health", data.Health, DbType.Int32);
            dbparams.Add("MaxHealth", data.MaxHealth, DbType.Int32);
            dbparams.Add("Strength", data.Strength, DbType.Int32);
            dbparams.Add("Defense", data.Defense, DbType.Int32);
            dbparams.Add("Gold", data.PlayerId, DbType.Int32);
            dbparams.Add("Experience", data.Experience, DbType.Int32);
            dbparams.Add("CurrentZoneId", data.CurrentZone.ZoneId, DbType.Int32);

            var updatePlayer = Task.FromResult(_dapper.Update<int>("[dbo].[SP_Update_Player]",
                            dbparams,
                            commandType: CommandType.StoredProcedure));
            return updatePlayer;
        }
    }
}

