﻿using PokeApiCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeApiConsole
{
    class Program
    {
        static async Task Main()
        {
            PokeApiClient client = new PokeApiClient();

            try
            {
                //Pokemon result = await client.GetPokemonByName("bulbasaur");
                Pokemon result = await client.GetPokemonById(1);

                Console.WriteLine($"Pokemon Id: {result.Id} \nName: {result.Name}" +
                    $"\nWeight(in hectograms): {result.Weight}" +
                    $"\nHeight(in inches): {result.Height}");
                Console.ReadKey();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("That Pokemon does not exist");
            }
            catch (HttpRequestException) 
            {
                Console.WriteLine("Please try again later");
            }
        }
    }
}
