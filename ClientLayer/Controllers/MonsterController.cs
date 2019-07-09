using System.Net;
using DL.Application.Monsters;
using DL.Domain.Monsters;
using Microsoft.AspNetCore.Mvc;

namespace DL.ClientLayer.Controllers
{
    [Route("api/monsters")]
    [ApiController]
    public class MonsterController : Controller
    {
        private readonly IMonsterService _monsterService;

        public MonsterController(IMonsterService monsterService)
        {
            _monsterService = monsterService;
        }

        [HttpGet]
        public JsonResult Monsters()
        {
            var models = _monsterService.All();
            return Result(HttpStatusCode.OK, models);
        }
        
        [HttpGet("{id}")]
        public JsonResult Monster(int id)
        {
            var model = _monsterService.Find(id);
            return Result(HttpStatusCode.OK, model);
        }
        
        [HttpPost]
        public JsonResult AddMonster([FromBody] Monster monster)
        {
            var model = _monsterService.Create(monster);
            return Result(HttpStatusCode.OK, model);
        }
                
        [HttpPost("{id}/rewards")]
        public JsonResult AddMonsterReward(int id, [FromBody] Reward reward)
        {
            var model = _monsterService.AddReward(id, reward);
            return Result(HttpStatusCode.OK, model);
        }
                
        [HttpDelete("{id}")]
        public JsonResult RemoveMonster(int id)
        {
            _monsterService.Remove(id);
            return Result(HttpStatusCode.OK);
        }

        private JsonResult Result(HttpStatusCode status)
        {
            return Result(status, new {});
        }

        private JsonResult Result(HttpStatusCode status, dynamic model)
        {
            var result = new JsonResult(new { Success = true, Model = model }) 
            {
                StatusCode = (int) status
            };
            return result;
        }
    }
}