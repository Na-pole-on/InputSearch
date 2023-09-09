using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using InputSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InputSearch.Controllers
{
    public class HomeController : Controller
    {
        private IPartyServices _partyServices;

        private static  ListOfModels models = new ListOfModels();

        public HomeController(IPartyServices partyServices) => _partyServices = partyServices;

        public IActionResult Parties()
        {
            models.getPartiesStep = models.step;
            models.searchStep = models.step;
            models.lastName = "";

            return View();
        }

        [HttpGet("/api/parties")]
        public IActionResult GetParties() => Json(_partyServices.GetAll().Take(models.step));

        [HttpGet("/api/show")]
        public IActionResult ShowMore()
        {
            IEnumerable<PartyDTO>? list = _partyServices
                .GetAll();

            if (list is not null)
            {
                var parties = list
                    .Skip(models.getPartiesStep)
                    .Take(models.step);

                models.getPartiesStep += models.step;

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
                        if(search == models.lastName)
                        {
                            models.searchStep += models.step;

                            return Json(list.Skip(models.searchStep)
                                .Take(models.step));
                        }

                        models.searchStep = 0;
                        models.lastName = search;

                        return Json(list.Take(models.step));
                    }
            }

            return NotFound();
        }

        [HttpPost("/api/filter")]
        public async Task<IActionResult> CheckFilter()
        {
            FilterDTO? filter = await Request
                .ReadFromJsonAsync<FilterDTO>();

            if(filter is not null)
            {
                IEnumerable<PartyDTO>? dtos = _partyServices
                    .FilterParties(filter);

                if (filter.Equals(models.filter))
                {
                    models.filterStep += models.step;

                    return Json(dtos.Skip(models.filterStep)
                        .Take(models.step));
                }

                models.filterStep = 0;
                models.filter = filter;

                return Json(dtos.Take(models.step));
            }

            return NotFound();
        }
    }
}