using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PG5200_Innlevering1.SettingsEditor.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace PG5200_Innlevering1.SettingsEditor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public Level _level;
        
        public ObservableCollection<Enemy> Enemies {
            get
            {
                return _level.Enemies;
            }
            set
            {
                _level.Enemies = value;
            }
        }
        private Enemy _selectedItem { get; set; }
        public Enemy SelectedItem {
            get
            {   
                return _selectedItem;
            }
            set
            { 
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        #region Commands

        public ICommand NewCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand DefaultCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _level = new Level();
            _selectedItem = new Enemy();
            CreateCommands();
        }

        private void CreateCommands()
        {
            NewCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem);
            DefaultCommand = new RelayCommand(DefaultLevel);
            SaveCommand = new RelayCommand(Save, CanSave);
            LoadCommand = new RelayCommand(Load);
        }
        public void AddItem()
        {
            Enemies.Add(
                new Enemy() {
                    Name = "New Enemy",
                    Health = 100,
                    MoveSpeed = 2.5f,
                    Damage = 10,
                    AttackSpeed = 0.5f,
                    ScoreValue = 10,
                    SpawnTime = 3,
                }
            );
            SelectedItem = Enemies.Last();
        }
        public void RemoveItem()
        {
            Enemies.Remove(_selectedItem);
        }
        private bool CanSave()
        {
            //TODO: Additional validation
            return true;
        }

        public void DefaultLevel()
        {
            PopulateView(new Level());
        }

        private string path = "../../Level.json";

        public void Save()
        {

            StreamWriter writer = new StreamWriter(path);
            writer.Write(_level.Save());
            writer.Close();
        }

        public void Load()
        {
            StreamReader reader = new StreamReader(path);

            Level model = _level.Load(reader.ReadToEnd());

            if (model != null)
                PopulateView(model);

            reader.Close();
        }

        public void PopulateView(Level model)
        {
            
        }
        /*private void SetDefaults()
        {
            Name = "Zom";
            Health = 100;
            MoveSpeed = 2.5f;
            Damage = 10;
            AttackSpeed = 0.5f;
            ScoreValue = 10;
            SpawnTime = 3;
        }*/
    }
}