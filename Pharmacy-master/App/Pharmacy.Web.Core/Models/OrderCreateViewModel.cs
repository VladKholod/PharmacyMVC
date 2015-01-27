using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Core;

namespace Pharmacy.Web.Core.Models
{
    public class OrderCreateViewModel : OrderViewModel
    {
        public List<Pharmacy.Core.Pharmacy> Pharmacies { get; set; } 
    }
}
