using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_EN : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Enemy;
    int enemyHp; 
    int CurrentHp; 
    string CurrentState ; 

      private BoxCollider2D coll;
    

    public /*private*/ SpriteRenderer sprite_filp;
    

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
            CurrentState = "death"; 
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp; 

            CurrentState = "hurt"; 
           
        }   
        else{

        if(Enemy.position.x > max_x)
        {
            sprite_filp.flipX = true;
            Debug.Log("flip NEG" ); //+ Enemy.position.x);
            //Enemy.velocity= new Vector2(  W_speed ,Enemy.velocity.y);
        }
        else if(Enemy.position.x < min_x)
        {
            sprite_filp.flipX = false;
            Debug.Log("flip POS" );
            Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
            
        }

        Debug.Log("position: " + Enemy.position.x);
        Debug.Log("MAX: " + max_x);
        walk(); 
        }

    }



    public float max_x; 
    public float min_x;

    public float W_speed = 1.5f; 

    void walk()
    {

        if(sprite_filp.flipX == false)
            Enemy.velocity= new Vector2(  W_speed ,Enemy.velocity.y);
        else 
             Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
    }

    void enemy_stop()
    {
        Enemy.velocity= new Vector2(  0 ,Enemy.velocity.y);
    }



}
