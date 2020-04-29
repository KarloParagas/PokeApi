using System;
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
            //Instantiate a PokeApiClient to access the client class
            PokeApiClient myClient = new PokeApiClient();

            //Get a pokemon by it's id
            Pokemon result = await myClient.GetPokemonById(1);

            //Grab the moves and assign it to a list so that you can assign it to the MoveList property below
            List<string> resultMoves = new List<string>();
            foreach (Move currentMove in result.moves) 
            {
                resultMoves.Add(currentMove.move.name);
            }
            resultMoves.Sort();

            //TODO: Refactor property names

            //Create an entry object with the data pulled from result variable
            var entry = new PokedexEntryViewModel()
            {
                Id = result.id,
                Name = result.name,
                Height = result.height.ToString(),
                Weight = result.weight.ToString(),
                PokedexImageUrl = result.sprites.front_default,
                MoveList = resultMoves
            };

            return View(entry); //Model binds the entry object to the Index view
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
