using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using Øving2.ViewModel;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace Øving2.Model
{
	public class Character 
    {

        private JsonSerializerSettings _config = new JsonSerializerSettings
        {
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public DateTimeOffset SaveTime { get; set; }
        public string Notes { get; set; }
        public enum AlignmentEnum { Good, Neutral, Evil, }

        
        public string CharacterName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MainViewModel.Races Race { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AlignmentEnum Alignment { get; set; }
        public int Level { get; set; }

        public int AttributePoints { get; set; }
        public int Strength { get; set; }
		public int Dexterity { get; set; }
		public int Constitution { get; set; }
		public int Intelligence { get; set; }
		public int Wisdom { get; set; }
		public int Charisma { get; set; }


		public Character()
		{
			SetDefaults();
		}

		private void SetDefaults()
		{
			CharacterName = "";
			Race = 0;
			Level = 1;

            AttributePoints = 15;
            Strength = 10;
			Dexterity = 10;
			Constitution = 10;
			Intelligence = 10;
			Wisdom = 10;
			Charisma = 10;
		}

		public string Save()
		{
		    SaveTime = DateTime.Now;
            
            return JsonConvert.SerializeObject(this, _config);
        }
		public Character Load(string value)
		{
            try
            {
				Character c = JsonConvert.DeserializeObject<Character>(value, _config);
				return c;

            } catch (Exception e)
            { 
                MessageBox.Show("ERROR: "+e.Message);
                return null;
            }
		}
        public override string ToString() {
            string f = "Name: " + CharacterName +
                "\nRace: " + Race.ToString() +
                "\nLevel: " + Level +
                "\n\nAttributePoints: " + AttributePoints +
                "\nStrength: " + Strength +
                "\nDexterity: " + Dexterity +
                "\nConstitution: " + Constitution +
                "\nIntelligence: " + Intelligence +
                "\nWisdom: " + Wisdom +
                "\nCharisma: " + Charisma;
            return f;
        }
    }
}