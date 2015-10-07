using GalaSoft.MvvmLight;
using System;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using PG5200_Innlevering1.SettingsEditor.ViewModel;

namespace PG5200_Innlevering1.SettingsEditor.Model
{
	public class Enemy 
    {
        public string Notes { get; set; }

        
        public string CharacterName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MainViewModel.Races Race { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public int Health { get; set; }

        public int AttributePoints { get; set; }
        public int AttackSpeed { get; set; }
		public int Damage { get; set; }
		public int ScoreValue { get; set; }
		public int Intelligence { get; set; }
		public int Wisdom { get; set; }
		public int Charisma { get; set; }


		public Enemy()
		{
			SetDefaults();
		}

		private void SetDefaults()
		{
			CharacterName = "";
			Race = 0;
			Health = 1;

            AttributePoints = 15;
            AttackSpeed = 10;
			Damage = 10;
			ScoreValue = 10;
			Intelligence = 10;
			Wisdom = 10;
			Charisma = 10;
		}

    }
}