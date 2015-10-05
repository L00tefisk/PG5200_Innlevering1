using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using PG5200_Innlevering1.SettingsEditor.Model;
using Øving2.Model;

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
    private Character _character;

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
        get
        {
            return (IEnumerable<Races>)Enum.GetValues(typeof(Races));
        }
    }

    private int[] attributes = new int[6];

    public void updateAttr()
    {
        attributes[0] = Strength;
        attributes[1] = Dexterity;
        attributes[2] = Constitution;
        attributes[3] = Intelligence;
        attributes[4] = Wisdom;
        attributes[5] = Charisma;
    }

    public string CharacterName
    {
        get
        {
            return _character.CharacterName;
        }

        set
        {
            if (_character.CharacterName != value)
            {
                _character.CharacterName = value;
                SaveCommand.CanExecute(null);
                RaisePropertyChanged();
            }
        }
    }

    public Races Race
    {
        get
        {
            return _character.Race;
        }

        set
        {
            if (_character.Race != value)
            {
                _character.Race = value;
                RaisePropertyChanged();
            }
        }
    }

    public int Level
    {
        get
        {
            return _character.Level;
        }

        set
        {
            if (_character.Level != value)
            {
                // Example of input validation.
                if (value > 0)
                {
                    _character.Level = value;
                }
                RaisePropertyChanged();
            }
        }
    }

    public int AttributePoints
    {
        get
        {
            return _character.AttributePoints;
        }
        set
        {
            if (_character.AttributePoints != value)
            {
                _character.AttributePoints = value;
                SaveCommand.CanExecute(null);
                RaisePropertyChanged();
            }
        }
    }

    public int Strength
    {
        get
        {
            return _character.Strength;
        }

        set
        {
            if (_character.Strength != value)
            {
                _character.Strength = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }


    public int Dexterity
    {
        get
        {
            return _character.Dexterity;
        }

        set
        {
            if (_character.Dexterity != value)
            {
                _character.Dexterity = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }


    public int Constitution
    {
        get
        {
            return _character.Constitution;
        }

        set
        {
            if (_character.Constitution != value)
            {
                _character.Constitution = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }


    public int Intelligence
    {
        get
        {
            return _character.Intelligence;
        }

        set
        {
            if (_character.Intelligence != value)
            {
                _character.Intelligence = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }


    public int Wisdom
    {
        get
        {
            return _character.Wisdom;
        }

        set
        {
            if (_character.Wisdom != value)
            {
                _character.Wisdom = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }

    public int Charisma
    {
        get
        {
            return _character.Charisma;
        }

        set
        {
            if (_character.Charisma != value)
            {
                _character.Charisma = value;

                PerformPointSpend();
                RaisePropertyChanged();
            }
        }
    }

    #endregion

    public void PerformPointSpend()
    {
        updateAttr();
        int pts = 15;
        for (int i = 0; i < 5; i++)
        {
            pts += ptsConversion(attributes[i]);
        }
        AttributePoints = pts;
    }
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

    public ICommand SaveCommand
    {
        get; set;
    }

    public ICommand LoadCommand
    {
        get; private set;
    }
    public ICommand NewCommand
    {
        get; private set;
    }
    #endregion

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel()
    {
        _character = new Character();

        CreateCommands();
    }
    private void CreateCommands()
    {
        NewCommand = new RelayCommand(NewCharacter);
        SaveCommand = new RelayCommand(Save, CanSave);
        LoadCommand = new RelayCommand(Load);
    }

    private bool CanSave()
    { //TODO: Additional validation
        return (!string.IsNullOrWhiteSpace(CharacterName) && AttributePoints >= 0 && Level >= 1);
    }

    public void NewCharacter()
    {
        PopulateView(new Character());
    }

    string path = "../../Character.json";
    public void Save()
    {

        StreamWriter writer = new StreamWriter(path);
        writer.Write(_character.Save());
        writer.Close();
    }

    public void Load()
    {
        StreamReader reader = new StreamReader(path);

        Character model = _character.Load(reader.ReadToEnd());

        if (model != null)
            PopulateView(model);

        reader.Close();
    }

    public void PopulateView(Character model)
    {
        CharacterName = model.CharacterName;
        Race = model.Race;
        Level = model.Level;

        AttributePoints = model.AttributePoints;
        Strength = model.Strength;
        Dexterity = model.Dexterity;
        Constitution = model.Constitution;
        Intelligence = model.Intelligence;
        Wisdom = model.Wisdom;
        Charisma = model.Charisma;
    }
}