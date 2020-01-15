using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVendas.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Saller Saller { get; set; }
        public ICollection<Departament> Departaments { get; set; }
    }
}
