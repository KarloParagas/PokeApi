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

        public async Task<IActionResult> Index()
        {
            int desiredId = 1;

            Pokemon result = await PokeAPIHelper.GetById(desiredId);

            //Create an entry object with the data pulled from result variable
            var entry = new PokedexEntryViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Height = result.Height.ToString(),
                Weight = result.Weight.ToString(),
                PokedexImageUrl = result.Sprites.FrontDefault,
                //MoveList = result.moves.OrderBy(m => m.move.name)
                //                       .Select(m => m.move.name)
                //                       .ToArray()
                MoveList = (from m in result.moves
                            orderby m.move.name ascending
                            select m.move.name).ToArray()
            };
            //entry.Name = entry.Name[0].ToString().ToUpper() + entry.Name.Substring(1); //Uppercases the first letter of name then concatenate's the rest of the name
            entry.Name = entry.Name.FirstCharToUpper(); //Using StringExtensions class that was created

            return View(entry); //Model binds the entry object to the Index view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
