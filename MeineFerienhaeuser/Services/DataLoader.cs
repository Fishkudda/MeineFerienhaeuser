using System.Text.Json;

namespace MeineFerienhaeuser.Services
{
    public class DataLoader
    {
        private AppSettings _appSettings { get; set; }

        public DataLoader (AppSettings appSettings)
        {

            this._appSettings = appSettings;

        }

        public List<House> Load()
        {

            // Load JSON Data
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Data", "houses.json");
            var houseData = JsonSerializer.Deserialize<List<House>>(File.ReadAllText(jsonPath, System.Text.Encoding.UTF8));
            //Add the Settings to each House
            houseData.ForEach(houseData => houseData.AddSettings(_appSettings));

            return houseData;
        }




    }
}
