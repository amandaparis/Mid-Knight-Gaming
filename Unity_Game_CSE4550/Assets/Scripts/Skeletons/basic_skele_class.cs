using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basic_skele_class : MonoBehaviour
{
    public Rigidbody2D Enemy;
    int enemyHp; 
    int CurrentHp; 
    private BoxCollider2D coll;
    public SpriteRenderer sprite_filp;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        CurrentHp = GetComponent<enemy_class>().CurrentHp(); 
        Enemy = GetComponent<Rigidbody2D>();
        sprite_filp = GetComponent<SpriteRenderer>() ;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHp = GetComponent<enemy_class>().CurrentHp(); 

        if(enemyHp <= 0)
        {
            coll.isTrigger = true; 
            Enemy.bodyType = RigidbodyType2D.Static;
            coll.enabled = false;  
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp;  
        }  
    }
}
