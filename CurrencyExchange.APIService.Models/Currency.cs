using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.APIService.Models
{
    public class Currency
    {
        public int Id;
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
