using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextBasedAdventure.Models;
using TextBasedAdventure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TextBasedAdventure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IDapperService _dapper;
        public ItemController(IDapperService dapper)
        {
            _dapper = dapper;
        }
        
        [HttpPost(nameof(Create))]
        public async Task<Item> Create(Item item)
        {
            var result = await Task.FromResult(_dapper.Insert("insert into Item (ItemName, LevelRequirement, ItemType, Equipped, StrengthBonus, DefenseBonus, HealthBonus, PlayerId)" +
            $" values({item.ItemName}, {item.LevelRequirement}, {item.Equipped}, {item.StrengthBonus}, {item.DefenseBonus}, {item.HealthBonus}, {item.Player.PlayerId})", null, commandType: CommandType.Text));
            return result;
        }
    }
}
