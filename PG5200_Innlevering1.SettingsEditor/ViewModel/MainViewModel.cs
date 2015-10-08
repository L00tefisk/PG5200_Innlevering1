using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PG5200_Innlevering1.SettingsEditor.Model;
using System.Collections.ObjectModel;

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
        private Enemy _currentEnemy { get; set; }
        public Enemy CurrentEnemy {
            get
            {
                return _currentEnemy;
            }
            set
            {
                _currentEnemy = value;
            }
        }
        public string Name
        {
            get {
                if (CurrentEnemy != null)
                    return CurrentEnemy.Name;
                return "";
            }

            set
            {
                if (CurrentEnemy.Name != value)
                {
                    CurrentEnemy.Name = value;
                    SaveCommand.CanExecute(null);
                    RaisePropertyChanged();
                }
            }
        }
        public int Health
        {
            get
            {
                return CurrentEnemy.Health;
            }
            set
            {
                if (CurrentEnemy.Health != value)
                {
                    CurrentEnemy.Health = value;
                    SaveCommand.CanExecute(null);
                    RaisePropertyChanged();
                }
            }
        }


        #region Commands

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand NewCommand { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _level = new Level();
            _currentEnemy = new Enemy();
            CreateCommands();
        }

        private void CreateCommands()
        {
            NewCommand = new RelayCommand(NewCharacter);
            SaveCommand = new RelayCommand(Save, CanSave);
            LoadCommand = new RelayCommand(Load);
        }

        private bool CanSave()
        {
            //TODO: Additional validation
            return true;
        }

        public void NewCharacter()
        {
            PopulateView(new Level());
        }

        private string path = "../../Enemy.json";

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
    }
}