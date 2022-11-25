using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.APIService.Models
{
    public class CurrencyExchangeRate
    {
        public int Id;
        public int SourceCurrencyId { get; set; }
        public string SourceCurrencyCode { get; set; }
        public int TargetCurrencyId { get; set; }
        public string TargetCurrencyCode { get; set; }
        public decimal TargetCurrencyExchangeRate { get; set; }
        public DateTime RecordedOn { get; set; }
    }
}
