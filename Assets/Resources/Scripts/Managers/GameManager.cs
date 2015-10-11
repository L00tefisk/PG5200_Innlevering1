using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
//using Assets.Resources.Scripts.Helpers;
using CompleteProject;

public class GameManager : MonoBehaviour {
    string path = "C:/Users/Eivind/Documents/GitHub/PG5200_Innlevering1/PG5200_Innlevering1.SettingsEditor/Level.json";
	// Use this for initialization
	void Awake () {
        Level model = new Level();
        model = model.Load(path);

        foreach (Enemy e in model.Enemies)
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/"+e.Name);

            GameObject enemy = Instantiate(prefab);

            NavMeshAgent nma = enemy.GetComponent<NavMeshAgent>();
            nma.speed = e.MoveSpeed;
            
            //((EnemyAttack)enemy.GetComponent(typeof(EnemyAttack))).timeBetweenAttacks = e.AttackSpeed;
            //enemy.GetComponent<EnemyAttack>().attackDamage = e.Damage;

            EnemyHealth eh = (EnemyHealth)enemy.GetComponentInChildren(typeof(EnemyHealth));
            while (!eh)
            {
                eh = enemy.GetComponent<EnemyHealth>();
                break;
            }
            eh.startingHealth = 200;
            eh.scoreValue = 200;
            PrefabUtility.ReplacePrefab(enemy, PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/"+e.Name+"_test.prefab"));

            Destroy(enemy);
        }
        /*foreach (Component m in copy.GetComponents(typeof(Component))) {
            Type t = m.GetType();
            enemy.AddComponent(m);
        }*/
    }

    // Update is called once per frame
    void Update () {
	
	}
    
}