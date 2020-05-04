using PokeApiCore;
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

        public static PokedexEntryViewModel GetPokedexEntryFromPokemon(Pokemon result)
        {
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
            return entry;
        }
    }
}
