using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InputSearch.Controllers
{
    public class HomeController : Controller
    {
        private IPartyServices _partyServices;

        private const int step = 9;
        private static int getPartiesStep = 6;

        private static int searchStep = 6;
        private static string lastName = "";

        public HomeController(IPartyServices partyServices) => _partyServices = partyServices;

        public IActionResult Parties()
        {
            getPartiesStep = step;
            searchStep = step;
            lastName = "";

            return View();
        }

        [HttpGet("/api/parties")]
        public IActionResult GetParties() => Json(_partyServices.GetAll().Take(step));

        [HttpGet("/api/show")]
        public IActionResult ShowMore()
        {
            IEnumerable<PartyDTO>? list = _partyServices
                .GetAll();

            if (list is not null)
            {
                var parties = list
                    .Skip(getPartiesStep)
                    .Take(step);

                getPartiesStep += step;

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
                IEnumerable<PartyDTO>? list = _partyServices
                    .GetPartiesByName(search);

                if(list is not null)
                    if(list.Count() > 0)
                    {
                        if(search == lastName)
                        {
                            searchStep += step;

                            return Json(list.Skip(searchStep).Take(step));
                        }

                        searchStep = step;
                        lastName = search;

                        return Json(list.Take(searchStep));
                    }
            }

            return NotFound();
        }
    }
}