using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PG5200_Innlevering1.SettingsEditor.Model;

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
        private Enemy _enemy;

        #region Properties

        public enum Races
        {
            Dwarf,
            Elf,
            Gnome,
            HalfElf,
            HalfOrc,
            Halfling,
            Human
        }

        public IEnumerable<Races> e_Races
        {
            get { return (IEnumerable<Races>) Enum.GetValues(typeof (Races)); }
        }

  
        public string CharacterName
        {
            get { return _enemy.CharacterName; }

            set
            {
                if (_enemy.CharacterName != value)
                {
                    _enemy.CharacterName = value;
                    SaveCommand.CanExecute(null);
                    RaisePropertyChanged();
                }
            }
        }

        public Races Race
        {
            get { return _enemy.Race; }

            set
            {
                if (_enemy.Race != value)
                {
                    _enemy.Race = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int Level
        {
            get { return _enemy.Health; }

            set
            {
                if (_enemy.Health != value)
                {
                    // Example of input validation.
                    if (value > 0)
                    {
                        _enemy.Health = value;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        public int AttributePoints
        {
            get { return _enemy.AttributePoints; }
            set
            {
                if (_enemy.AttributePoints != value)
                {
                    _enemy.AttributePoints = value;
                    SaveCommand.CanExecute(null);
                    RaisePropertyChanged();
                }
            }
        }

        public int Strength
        {
            get { return _enemy.AttackSpeed; }

            set
            {
                if (_enemy.AttackSpeed != value)
                {
                    _enemy.AttackSpeed = value;
                    
                    RaisePropertyChanged();
                }
            }
        }


        public int Dexterity
        {
            get { return _enemy.Damage; }

            set
            {
                if (_enemy.Damage != value)
                {
                    _enemy.Damage = value;
                    
                    RaisePropertyChanged();
                }
            }
        }


        public int Constitution
        {
            get { return _enemy.ScoreValue; }

            set
            {
                if (_enemy.ScoreValue != value)
                {
                    _enemy.ScoreValue = value;
                    
                    RaisePropertyChanged();
                }
            }
        }


        public int Intelligence
        {
            get { return _enemy.Intelligence; }

            set
            {
                if (_enemy.Intelligence != value)
                {
                    _enemy.Intelligence = value;
                    
                    RaisePropertyChanged();
                }
            }
        }


        public int Wisdom
        {
            get { return _enemy.Wisdom; }

            set
            {
                if (_enemy.Wisdom != value)
                {
                    _enemy.Wisdom = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        public int Charisma
        {
            get { return _enemy.Charisma; }

            set
            {
                if (_enemy.Charisma != value)
                {
                    _enemy.Charisma = value;
                    
                    RaisePropertyChanged();
                }
            }
        }

        #endregion


        private int ptsConversion(int pts)
        {
            switch (pts)
            {
                case 7:
                    return 4;
                case 8:
                    return 2;
                case 9:
                    return 1;
                case 10:
                    return 0;
                case 11:
                    return -1;
                case 12:
                    return -2;
                case 13:
                    return -3;
                case 14:
                    return -5;
                case 15:
                    return -7;
                case 16:
                    return -10;
                case 17:
                    return -13;
                case 18:
                    return -17;
                default:
                    return 0;
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
            _enemy = new Enemy();

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
            PopulateView(new Enemy());
        }

        private string path = "../../Enemy.json";

        public void Save()
        {

            StreamWriter writer = new StreamWriter(path);
            writer.Write(_enemy.Save());
            writer.Close();
        }

        public void Load()
        {
            StreamReader reader = new StreamReader(path);

            Enemy model = _enemy.Load(reader.ReadToEnd());

            if (model != null)
                PopulateView(model);

            reader.Close();
        }

        public void PopulateView(Enemy model)
        {
            CharacterName = model.CharacterName;
            Race = model.Race;
            Level = model.Health;

            AttributePoints = model.AttributePoints;
            Strength = model.AttackSpeed;
            Dexterity = model.Damage;
            Constitution = model.ScoreValue;
            Intelligence = model.Intelligence;
            Wisdom = model.Wisdom;
            Charisma = model.Charisma;
        }
    }
}