﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeApiCore;
using PokeApiWebsite.Models;

namespace PokeApiWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? id)
        {
            int desiredId = id ?? 1; //Set desiredId to "id", if null, set it to 1
            ViewData["Id"] = desiredId;

            Pokemon result = await PokeAPIHelper.GetById(desiredId);

            PokedexEntryViewModel entry = PokeAPIHelper.GetPokedexEntryFromPokemon(result);

            return View(entry); //Model binds the entry object to the Index view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
