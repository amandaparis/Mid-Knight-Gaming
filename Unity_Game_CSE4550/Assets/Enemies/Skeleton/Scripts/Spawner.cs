using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public Transform[] spawnPoints;
    public GameObject spawner;
    public Transform spawnPoint;
    public GameObject enemyPrefabs;
    bool isCreated;
    Animations skeleton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //int randEnemy = Random.Range(0, enemyPrefabs.Length);
            if (!isCreated)
            {
                Instantiate(enemyPrefabs, spawnPoint.position, Quaternion.identity);
                gameObject.SetActive(false);
                isCreated = true;
            }
            
        }
    }

    private void OnTriggeStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //int randEnemy = Random.Range(0, enemyPrefabs.Length);
            if (!isCreated)
            {
                Instantiate(enemyPrefabs, spawnPoint.position, Quaternion.identity);
                gameObject.SetActive(false);
                isCreated = true;
            }
        }
    }

}
