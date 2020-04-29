﻿using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeApiCore
{
    /// <summary>
    /// Client class to consume PokeApi
    /// https://pokeapi.co
    /// </summary>
    public class PokeApiClient
    {
        static readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Retrieve pokemon by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Pokemon> GetPokemonByName(string name)
        {
            name = name.ToLower(); //Pokemon name must be lower case
            return await GetPokemonByNameOrId(name);
        }

        /// <summary>
        /// Gets a Pokemon by their Pokedex ID number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Pokemon> GetPokemonById(int id) 
        {
            return await GetPokemonByNameOrId(id.ToString());
        }

        /// <summary>
        /// Retrieve pokemon by name or id
        /// </summary>
        /// <exception cref="HttpRequestException"></exception>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException">Thrown when Pokemon is not found</exception>
        /// <returns></returns>
        private static async Task<Pokemon> GetPokemonByNameOrId(string name)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{name}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync(); //This gets the response from the service and put into one big string
                return JsonConvert.DeserializeObject<Pokemon>(responseBody);
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ArgumentException($"{name} does not exist");
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}
