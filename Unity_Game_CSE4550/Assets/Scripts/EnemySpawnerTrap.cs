using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrap : MonoBehaviour
{
    [SerializeField]
     int delay_time;

    private bool active;
    private bool triggered;

    [SerializeField]
    private GameObject EnemyReference;

    private GameObject spawnedEnemy;
    private Rigidbody2D spawnedEnemy_body;
    private Transform grave;


    [SerializeField]
    private GameObject particleObject;
    private ParticleSystem particleObject_dust;

    // Start is called before the first frame update
    void Start()
    {
        grave = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(!triggered)
            StartCoroutine(SpawnMonsters());    
    }


    IEnumerator SpawnMonsters()
    {

        triggered = true;

        spawnedEnemy = Instantiate(EnemyReference);
        spawnedEnemy.transform.position = grave.position;
        
        //Start Sequence
        particleObject_dust = particleObject.GetComponent<ParticleSystem>();

        particleObject_dust.Play();
        spawnedEnemy_body = spawnedEnemy.GetComponent<Rigidbody2D>();
        //Make him jump so it can seem like he came out of the ground
        spawnedEnemy_body.velocity = new Vector2(spawnedEnemy_body.velocity.x, 4.5f);

        yield return new WaitForSeconds(delay_time);
        triggered = false;
    }

}
