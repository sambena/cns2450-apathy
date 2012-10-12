using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apathy.Models;

namespace Apathy.ViewModels
{
    public class EnvelopeIndexViewModel
    {
        public IEnumerable<Transaction> RecentTransactions { get; set; }
        public IEnumerable<Envelope> Envelopes { get; set; }
    }
}