using System;
using DogWalking.Data;
using DogWalking.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var walkerRepo = new WalkerRepository();

            List<Walker> allWalkers = walkerRepo.GetAllWalkers();
            Console.WriteLine("Here is a list of all the walkers.");
            foreach (var walker in allWalkers)
            {
                Console.WriteLine($"{walker.Name} {walker.Neighborhood.Name}"); 
            }

            Console.WriteLine("North Nashville Walkers");

            List<Walker> allNorthWalkers = walkerRepo.GetWalkerByNeighborhood(1);
           foreach(var walker in allNorthWalkers)
            {
                Console.WriteLine($"{walker.Name} {walker.Neighborhood.Name}");
            }

            var newWalker = new Walker
            {
                Name = "Namita",
                NeighborhoodId = 1
            };
            walkerRepo.CreateNewWalker(newWalker);


        }
    }
}
