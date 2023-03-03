using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrap : MonoBehaviour
{
    [Tooltip("Delays the enemy spawning")]
    [SerializeField] int delay_time;
    [Tooltip("The vertical offset position for the enemy spawning")]
    [SerializeField] float spawn_offset_y;
    [Tooltip("The distance from the player to spawn the enemy")]
    [SerializeField] float spawn_distance;
    [SerializeField] private GameObject EnemyReference;
    [SerializeField] private GameObject particleObject;

    private bool active;
    private bool triggered;

    private Rigidbody2D spawnedEnemy_body;
    private GameObject spawnedEnemy;
    private Transform grave;
    private float distance;
    PlayerActions player;

    private ParticleSystem particleObject_dust;

    // Start is called before the first frame update
    void Start()
    {
        grave = GetComponent<Transform>();
        player = FindObjectOfType<PlayerActions>();
    }
    private void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= spawn_distance && !triggered)
            StartCoroutine(SpawnMonsters());  
    }


    IEnumerator SpawnMonsters()
    {

        triggered = true;

        spawnedEnemy = Instantiate(EnemyReference);
        spawnedEnemy.transform.position = new Vector2(grave.position.x, grave.position.y + spawn_offset_y );
        
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
