using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Breed { get; set; }
        public string Notes { get; set; }
        //This is to hold the actual foreign key integer
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
