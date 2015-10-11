using System;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level
{
    public List<string> EnemyTypes { get; set; }
    public List<Enemy> Enemies { get; set; }

    public Level()
    {
        Enemies = new List<Enemy>();

        EnemyTypes = new List<string>();
    }

    private JsonSerializerSettings _config = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore
    };
    public Level Load(string path)
    {
        StreamReader reader = new StreamReader(path);
        String value = reader.ReadToEnd();
        reader.Close(); 

        try
        {
            Level l = JsonConvert.DeserializeObject<Level>(value, _config);
            return l;
        }
        catch (Exception e)
        {
            Debug.Log("ERROR: "+e.Message);
            return new Level();
        }
    }
}