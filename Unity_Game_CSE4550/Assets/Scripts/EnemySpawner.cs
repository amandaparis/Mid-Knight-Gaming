using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyReference;

    private GameObject spawnedEnemy;

    private Transform grave;
    private Rigidbody2D spawnedEnemy_body;

    [SerializeField]
    private int time; //time to spawn

    [SerializeField]
    private GameObject particleObject;
    private ParticleSystem particleObject_dust;

    // Start is called before the first frame update
    void Start()
    {
        grave = GetComponent<Transform>();
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while(true)
        {
            yield return new WaitForSeconds(time);

            spawnedEnemy = Instantiate(EnemyReference);
            spawnedEnemy.transform.position = grave.position;
            
            //Play dust particle
            particleObject_dust = particleObject.GetComponent<ParticleSystem>();
            particleObject_dust.Play();

            spawnedEnemy_body = spawnedEnemy.GetComponent<Rigidbody2D>();
            //Make him jump so it can seem like he came out of the ground
            spawnedEnemy_body.velocity = new Vector2(spawnedEnemy_body.velocity.x, 4.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
