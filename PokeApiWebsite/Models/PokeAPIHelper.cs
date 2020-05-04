﻿using PokeApiCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeApiWebsite.Models
{
    public static class PokeAPIHelper
    {
        /// <summary>
        /// Get a pokemon by id, moves will be sorted in alphabetical order
        /// </summary>
        /// <param name="desiredId"></param>
        /// <returns></returns>
        public static async Task<Pokemon> GetById(int desiredId) 
        {
            //Instantiate a PokeApiClient to access the client class
            PokeApiClient myClient = new PokeApiClient();

            //Get a pokemon by it's id
            Pokemon result = await myClient.GetPokemonById(desiredId);

            //Sort moves by name alphabetically
            result.moves.OrderBy(m => m.move.name);

            return result;
        }
    }
}
