using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
//using Assets.Resources.Scripts.Helpers;
namespace CompleteProject
{
    public class GameManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;

        string path = "C:/Users/Eivind/Documents/GitHub/PG5200_Innlevering1/PG5200_Innlevering1.SettingsEditor/Level.json";
        // Use this for initialization
        void Start()
        {
            Level model = new Level();
            model = model.Load(path);
            
            foreach (Enemy e in model.Enemies)
            {
                GameObject prefab = (GameObject)Resources.Load("Prefabs/" + e.TypeName);

                GameObject enemy = Instantiate(prefab);
                
                enemy.GetComponent<NavMeshAgent>().speed = e.MoveSpeed;

                enemy.GetComponent<EnemyAttack>().timeBetweenAttacks = e.AttackSpeed;
                enemy.GetComponent<EnemyAttack>().attackDamage = e.Damage;

                enemy.GetComponent<EnemyHealth>().startingHealth = e.Health;
                enemy.GetComponent<EnemyHealth>().scoreValue = e.ScoreValue;

                PrefabUtility.ReplacePrefab(enemy, PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/" + e.TypeName + ".prefab"));

                AddManager(e.TypeName, e.SpawnTime);

                Destroy(enemy);
            }
            /*foreach (Component m in copy.GetComponents(typeof(Component))) {
                Type t = m.GetType();
                enemy.AddComponent(m);
            }*/
        }

        // Update is called once per frame
        void Update()
        {
             
        }
        void AddManager(string prefabName, int spawnTime)
        {
            EnemyManager enemyManagerScript = gameObject.AddComponent<EnemyManager>();
            enemyManagerScript.playerHealth = playerHealth;
            enemyManagerScript.enemy = (GameObject)Resources.Load("Prefabs/" + prefabName);
            enemyManagerScript.spawnTime = spawnTime;
            enemyManagerScript.spawnPoints = new Transform[1];
            enemyManagerScript.spawnPoints[enemyManagerScript.spawnPoints.Length - 1] = GameObject.Find(prefabName+"SpawnPoint").transform;
        }
    }
}