using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InputSearch.Controllers
{
    public class HomeController : Controller
    {
        private IPartyServices _partyServices;
        private static int count = 6;

        public HomeController(IPartyServices partyServices) => _partyServices = partyServices;

        public IActionResult Parties() => View();

        [HttpGet("/api/parties")]
        public IActionResult GetParties() => Json(_partyServices.GetAll().Take(count));

        [HttpGet("/api/show")]
        public IActionResult ShowMore()
        {
            IEnumerable<PartyDTO>? list = _partyServices
                .GetAll();

            if (list is not null)
            {
                var parties = list
                    .Skip(count)
                    .Take(6);

                count += 6;

                return Json(parties);
            }

            return NotFound( new { message = "Parties not found!" } );
        }

        [HttpGet("/api/parties/{id}")]
        public async Task<IActionResult> AddStudent(string id)
        {
            bool response = await _partyServices
                .AddStudent(id);

            if (response == false)
                NotFound();

            return Ok();
        }

        [HttpGet("/api/check")]
        public IActionResult CheckShowMore() => Json(_partyServices.GetAll().Count());

        [HttpPost("/api/search")]
        public async Task<IActionResult> GetPartiesByName()
        {
            string? search = await Request
                .ReadFromJsonAsync<string>();

            if(search is not null)
            {
                IEnumerable<PartyDTO>? dtos = _partyServices
                    .GetPartiesByName(search);

                if(dtos is not null)
                    if(dtos.Count() > 0)
                        return Json(dtos.Take(6));
            }

            return NotFound();
        }
    }
}