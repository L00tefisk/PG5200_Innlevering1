using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace PG5200_Innlevering1.SettingsEditor.Model
{
    public class Level
    {
        public List<Enemy> Enemies { get; set; }

        public Level()
        {

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
        public Enemy Load(string value)
        {
            try
            {
                Enemy c = JsonConvert.DeserializeObject<Enemy>(value, _config);
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
