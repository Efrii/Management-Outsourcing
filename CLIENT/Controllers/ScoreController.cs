using System;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLIENT.Controllers
{
    [Authorize]
    public class ScoreController : BaseController<Score, ScoreRepository, int>
    {
        ScoreRepository ScoreRepository;

        public ScoreController(ScoreRepository scoreRepository) : base(scoreRepository)
        {
            this.ScoreRepository = scoreRepository;
        }

        [HttpGet("GetJoin")]
        public async Task<JsonResult> GetJoin()
        {
            var data = await ScoreRepository.GetJoin();

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetJoinID/{id}")]
        public async Task<JsonResult> GetJoinID(int id)
        {
            var data = await ScoreRepository.GetJoinID(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetJoinClassId/{id}")]
        public async Task<JsonResult> GetJoinClassId(int id)
        {
            var data = await ScoreRepository.GetJoinClassId(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpGet("GetJoinIdEmployee/{id}")]
        public async Task<JsonResult> GetJoinIdEmployee(int id)
        {
            var data = await ScoreRepository.GetJoinIdEmployee(id);

            if (data != null)
            {
                return Json(data);
            }

            return Json(data);
        }

        [HttpPost("PostScore")]
        [ValidateAntiForgeryToken]
        public JsonResult PostScore([FromBody] ScoreVM score)
        {
            var result = ScoreRepository.PostScore(score);

            return Json(result);
        }

        [HttpPut("PutScore")]
        public JsonResult PutScore([FromBody] ScoreVM score)
        {
            var result = ScoreRepository.PutScore(score);

            return Json(result);
        }

        [Authorize(Roles = "TRAINNER")]
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Authorize(Roles = "MANAGER")]
        [HttpGet("ScoreManager")]
        public ActionResult ScoreManager()
        {
            return View("ScoreManager");
        }
    }
}
