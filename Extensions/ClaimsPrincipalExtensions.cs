using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Plantech.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetFuncionarioId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0; // Retorna 0 se o claim não for encontrado
        }
    }

}