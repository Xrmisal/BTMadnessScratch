using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    SOActorModel actorModel;//The enemy we will use to spawn
    [SerializeField]
    [Tooltip("Spawn 1 enemy every few seconds.")]
    float spawnRate; // Spawn rate is set within unity engine.
    [SerializeField]
    [Tooltip("Number of enemies to be spawned from this spawner. Limited to max of 10 enemies!")]
    [Range(0, 10)] //Tool to limit maximum set by the artist
    int quantity;
    GameObject enemies;
    void Awake()
    {
        enemies = GameObject.Find("_Enemies");
        //Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject enemyObject = CreateEnemy();
            enemyObject.transform.SetParent(this.transform);
            enemyObject.transform.position = transform.position;

            yield return new WaitForSeconds(spawnRate);
        }

        yield return null;
    }

    GameObject CreateEnemy()
    {
        GameObject enemy=GameObject.Instantiate(actorModel.actor) as GameObject;
        enemy.GetComponent<IActorTemplate>().ActorStats(actorModel);
        enemy.name = actorModel.name.ToString();
        return enemy;
    }

}
