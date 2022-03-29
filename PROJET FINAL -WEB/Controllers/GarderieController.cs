using Microsoft.AspNetCore.Mvc;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PROJET_FINAL__WEB.Controllers
{
    public class GarderieController : Controller
    {
        [Route("")]
        [Route("Garderie")]
        [Route("Garderie/Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("/Garderie/AjouterCegep")]
        [HttpPost]
        public async Task<IActionResult> AjouterGarderie()
        {
            return View();
        }

        [Route("/Garderie/FormulaireModifierGarderie")]
        [HttpGet]
        public async Task<IActionResult> FormulaireModifierGarderie()
        {
            return View();
        }

        [Route("/Garderie/ModifierGarderie")]
        [HttpPost]
        public async Task<IActionResult> ModifierGarderie()
        {
            return View();
        }
        [Route("/Garderie/SupprimerGarderie")]
        [HttpPost]
        public async Task<IActionResult> SupprimerGarderie()
        {
            return View();
        }

        [Route("/Garderie/ViderListeGarderie")]
        [HttpPost]
        public async Task<IActionResult> ViderListeGarderie()
        {
            return View();
        }
    }
