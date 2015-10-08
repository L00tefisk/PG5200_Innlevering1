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
        public string Name { get; set; }
        
        public int Health { get; set; }
        public float MoveSpeed { get; set; }
        public int Damage { get; set; }
        public float AttackSpeed { get; set; }

        public int ScoreValue { get; set; }
        public int SpawnTime { get; set; }


		public Enemy()
		{
			SetDefaults();
		}

		private void SetDefaults()
		{
            Name = "Zom";
            Health = 100;
            MoveSpeed = 2.5f;
            Damage = 10;
            AttackSpeed = 0.5f;
            ScoreValue = 10;
            SpawnTime = 3;
        }

    }
}