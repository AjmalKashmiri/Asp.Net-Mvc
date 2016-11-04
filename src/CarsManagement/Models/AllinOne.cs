using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsManagement.Models
{
    public class AllinOne
    {
        public IList<Categry>categry { get; set; }
        public IList<Products>products { get; set; }
    }
}
