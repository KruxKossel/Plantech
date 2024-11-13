using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Models;

namespace Plantech.ViewModels
{
    public class RelatorioViewModel
    {
        public long TotalVenda{get;set;}
        public long TotalCompra{get;set;}
        public double Gastos{get;set;}
        public double Faturamento{get;set;}
        public long TotalColheita{get;set;}
        public long TotalPlantioAtivo{get;set;}
        
    }
}