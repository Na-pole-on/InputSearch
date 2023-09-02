using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InputSearch.Controllers
{
    public class HomeController : Controller
    {
        IPartyServices _partyServices;

        public HomeController(IPartyServices partyServices) => _partyServices = partyServices;

        public IActionResult Parties() => View();

        [HttpGet("/api/parties")]
        public JsonResult GetParties() => Json(_partyServices.GetAll().Take(12));
    }
}