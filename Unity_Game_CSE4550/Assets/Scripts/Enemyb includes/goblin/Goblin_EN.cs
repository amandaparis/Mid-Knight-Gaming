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

    

    void Start()
    {
         coll = GetComponent<BoxCollider2D>();
         CurrentHp = GetComponent<enemy_class>().CurrentHp(); 
         CurrentState = "IDE"; 
         Enemy = GetComponent<Rigidbody2D>();
            coll = GetComponent<BoxCollider2D>();
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
           
        }   


    }



    public int max_x; 
    public int min_x;



    
     
}
