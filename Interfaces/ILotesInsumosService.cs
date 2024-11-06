using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.DTOs;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface ILotesInsumosService
    {
        Task <List<LotesInsumoDTO>> ListarLotes();
        Task <LotesInsumoDTO> GetLotesInsumoId(int id);
        Task  EditarLote(LotesInsumoDTO lotesInsumo);
    }
}