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
        Task <List<LotesHortalicas>> ListarLotes();
        Task <LotesHortalicas> GetLotesInsumoId(int id);
        Task <List<LotesHortalicas>> ListarLotesAtivos();
        Task  EditarLote(LotesHortalicas lotesHortalica);

    }
}