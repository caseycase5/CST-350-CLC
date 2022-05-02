using CST_350_CLC.Models;
using System.Collections.Generic;

namespace CST_350_CLC.Services
{

    public interface ISavesDataService
    {

        List<GameDataModel> GetGames();
        GameDataModel GetGameById(int id);
        int Insert(GameDataModel gameDataModel);
        int Delete(GameDataModel gameDataModel);

    }
}
