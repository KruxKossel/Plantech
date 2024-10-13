using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plantech.DTOs
{
    public class PermissaoDTO
    {
        public int Id { get; set; }

        public int CargoId { get; set; }

        public int TelaId { get; set; }

        public int? CanCreate { get; set; }

        public int? CanRead { get; set; }

        public int? CanUpdate { get; set; }

        public int? CanDelete { get; set; }
    }
}