using Plantech.DTOs;


namespace Plantech.Interfaces

{
    public interface ILotesHortalicasService
    {
        Task <List<LotesHortalicaDTO>> ListarLotes();
        Task <LotesHortalicaDTO> GetLotesInsumoId(int id);
        Task  EditarLote(LotesHortalicaDTO lotesHortalica);

    }
}