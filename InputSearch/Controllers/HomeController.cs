using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using InputSearch.Models;
using InputSearch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InputSearch.Controllers
{
    public class HomeController : Controller
    {
        private IPartyServices _partyServices;

        private ListOfModels models = new ListOfModels();

        public HomeController(IPartyServices partyServices) => _partyServices = partyServices;

        public IActionResult Parties()
        {
            ListOfModels.getPartiesStep = ListOfModels.step;
            ListOfModels.searchStep = ListOfModels.step;
            ListOfModels.lastName = "";

            return View();
        }

        [HttpGet("/api/parties")]
        public IActionResult GetParties() => Json(_partyServices.GetAll().Take(ListOfModels.step));

        [HttpGet("/api/show")]
        public IActionResult ShowMore()
        {
            IEnumerable<PartyDTO>? list = _partyServices
                .GetAll();

            if (list is not null)
            {
                var parties = list
                    .Skip(ListOfModels.getPartiesStep)
                    .Take(ListOfModels.step);

                ListOfModels.getPartiesStep += ListOfModels.step;

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
                        if(search == ListOfModels.lastName)
                        {
                            ListOfModels.searchStep += ListOfModels.step;

                            return Json(list.Skip(ListOfModels.searchStep)
                                .Take(ListOfModels.step));
                        }

                        ListOfModels.searchStep = ListOfModels.step;
                        ListOfModels.lastName = search;

                        return Json(list.Take(ListOfModels.searchStep));
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
            }

            return NotFound();
        }
    }
}