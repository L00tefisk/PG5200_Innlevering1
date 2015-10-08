using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PG5200_Innlevering1.SettingsEditor.Model
{
    public class Level
    {
        public ObservableCollection<Enemy> Enemies { get; set; }

        public Level()
        {
            Enemies = new ObservableCollection<Enemy>();
            Enemy ZomBear = new Enemy()
            {
                Name = "ZomBear",
                Health = 100,
                MoveSpeed = 2.5f,
                Damage = 15,
                AttackSpeed = 0.5f,
                ScoreValue = 10,
                SpawnTime = 3
            };
            Enemy ZomBunny = new Enemy()
            {
                Name = "ZomBunny",
                Health = 100,
                MoveSpeed = 3f,
                Damage = 10,
                AttackSpeed = 0.5f,
                ScoreValue = 10,
                SpawnTime = 3
            };
            Enemy Hellephant = new Enemy()
            {
                Name = "Hellephant",
                Health = 100,
                MoveSpeed = 2f,
                Damage = 30,
                AttackSpeed = 0.5f,
                ScoreValue = 50,
                SpawnTime = 15
            };
            Enemies.Add(ZomBear);
            Enemies.Add(ZomBunny);
            Enemies.Add(Hellephant);
        }

        private JsonSerializerSettings _config = new JsonSerializerSettings
        {
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Save()
        {
            return JsonConvert.SerializeObject(this, _config);
        }
        public Level Load(string value)
        {
            try
            {
                Level c = JsonConvert.DeserializeObject<Level>(value, _config);
                return c;

            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
                return null;
            }
        }
    }
}
