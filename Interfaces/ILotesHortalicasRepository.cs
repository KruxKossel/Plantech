using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Enuns;
using Plantech.Models;

namespace Plantech.Interfaces
{
    public interface ILotesHortalicasRepository
    {
        Task <List<LotesHortalica>> ListarLotes();
        Task <LotesHortalica> GetLotesInsumoId(int id);
        Task  EditarLote(LotesHortalica lotesHortalica);

    }
}