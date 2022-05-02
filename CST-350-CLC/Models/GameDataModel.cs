namespace CST_350_CLC.Models
{
    public class GameDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DateTime { get; set; }
        public string GameData { get; set; }

        public GameDataModel(int id, int userId, string dateTime, string gameData)
        {
            Id = id;
            UserId = userId;
            DateTime = dateTime;
            GameData = gameData;
        }
        public GameDataModel()
        { }

    }


}
