using DemoASPMVC_DAL.Models;
using DemoASPMVC_DAL.Models.Form;

namespace DemoASPMVC_DAL.Interface
{
    public interface IGameService
    {
        bool Create(GameForm game);
        bool Delete(int id);
        Game GetById(int id);
        IEnumerable<Game> GetGames();
        IEnumerable<Game> GetByUserId(int userId);
        void AddFavorite(int idUser, int idGame);
    }
}