using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraVert
{
    public class Plant
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public int LightNeeds { get; set; }
        public decimal AskingPrice { get; set; }
        public string City { get; set; }
        public int Zip {  get; set; }
        public bool Sold { get; set; }
        public DateTime AvailableUntil { get; set; }
    }
}
