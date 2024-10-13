using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plantech.Enuns;

namespace Plantech.Interfaces
{
    public interface ILotesHortalicasRepository
    {
        void AtualizaLotePreco(decimal preco_venda);

        void AtualizarStatus(StatusLote statusLote);

        void AtualizarLoteDataValidade(DateTime Data_validade);
    }
}