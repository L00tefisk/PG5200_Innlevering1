using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PG5200_Innlevering1.SettingsEditor.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows;

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
        private string _selectedType { get; set; }
        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                RaisePropertyChanged(() => SelectedType);
            }
        }
        public ObservableCollection<String> EnemyTypes
        {
            get
            {
                return _level.EnemyTypes;
            }
        }

        private Level _level;

        public Level Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                RaisePropertyChanged(() => Level);
            }
        }
        public ObservableCollection<Enemy> Enemies {
            get
            {
                return _level.Enemies;
            }
            set
            {
                _level.Enemies = value;
                RaisePropertyChanged(() => Enemies);
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
                RaisePropertyChanged(() => SelectedItem);
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
            Level = new Level();
            
            CreateCommands();
            
            Load();
        }

        private void CreateCommands()
        {
            NewCommand = new RelayCommand(AddItem);
            RemoveCommand = new RelayCommand(RemoveItem, CanRemove);
            DefaultCommand = new RelayCommand(DefaultLevel);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
        }
        public void AddItem()
        {
            Enemies.Add(
                new Enemy() {
                    TypeName = "",
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
            int i = Enemies.IndexOf(SelectedItem);
            Enemies.Remove(SelectedItem);

            if (Enemies.Count > 0)
            {
                if (i == Enemies.Count)
                    SelectedItem = Enemies[i - 1];
                else
                    SelectedItem = Enemies[i];
            } 
        }

        private bool CanRemove()
        {
            //return SelectedItem != null;
            return true;
        } 

        public void DefaultLevel()
        {
            Level model = new Level();
            model.Enemies = new ObservableCollection<Enemy>
            {
                new Enemy()
                {
                    TypeName = "ZomBear",
                    Health = 100,
                    MoveSpeed = 2.5f,
                    Damage = 15,
                    AttackSpeed = 0.5f,
                    ScoreValue = 10,
                    SpawnTime = 3
                },
                new Enemy()
                {
                    TypeName = "ZomBunny",
                    Health = 100,
                    MoveSpeed = 3f,
                    Damage = 10,
                    AttackSpeed = 0.5f,
                    ScoreValue = 10,
                    SpawnTime = 3
                },
                new Enemy()
                {
                    TypeName = "Hellephant",
                    Health = 100,
                    MoveSpeed = 2f,
                    Damage = 30,
                    AttackSpeed = 0.5f,
                    ScoreValue = 50,
                    SpawnTime = 15
                }
            };
            model.EnemyTypes = new ObservableCollection<string>()
            {
                "ZomBear",
                "ZomBunny",
                "Hellephant"
            };
            PopulateView(model);
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

            Level model = Level.Load(reader.ReadToEnd());
        
            reader.Close();

            PopulateView(model);
        }

        public void PopulateView(Level model)
        {
            Level = model;
            Enemies = model.Enemies;
        }
    }
}