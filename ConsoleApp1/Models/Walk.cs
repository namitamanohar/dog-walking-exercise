using System;
using System.Collections.Generic;
using System.Text;

namespace DogWalking.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public string Date { get; set; }

        public int Duration { get; set; }
        public string Notes { get; set; }
        //This is to hold the actual foreign key integer
        public int WalkerId { get; set; }

        public Walker Walker { get; set; }

        public int DogId { get; set; }

        public Dog Dog { get; set; }
    }

}
