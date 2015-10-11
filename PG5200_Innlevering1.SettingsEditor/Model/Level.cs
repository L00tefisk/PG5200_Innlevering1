using System;
using System.Windows;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using UnityEditor;
using UnityEngine;
//using CompleteProject;

namespace PG5200_Innlevering1.SettingsEditor.Model
{
    public class Level
    {
        
        public ObservableCollection<Enemy> Enemies { get; set; }

        public Level()
        {
            Enemies = new ObservableCollection<Enemy>();
        }

        private JsonSerializerSettings _config = new JsonSerializerSettings
        {
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
                return JsonConvert.DeserializeObject<Level>(value, _config);
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR: " + e.Message);
                return null;
            }
        }
    }
}
