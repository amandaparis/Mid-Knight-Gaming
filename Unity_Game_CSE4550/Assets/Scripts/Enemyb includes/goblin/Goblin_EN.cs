using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_EN : MonoBehaviour
{
    // Start is called before the first frame update

    int enemyHp; 
    int CurrentHp; 
    string CurrentState ; 

    public BoxCollider2D coll;
    void Start()
    {
         coll = GetComponent<BoxCollider2D>();
         CurrentHp = GetComponent<enemy_class>().CurrentHp(); 
         CurrentState = "IDE"; 

    }

    // Update is called once per frame
    void Update()
    {

        enemyHp = GetComponent<enemy_class>().CurrentHp(); 

        if(enemyHp <= 0)
        {
            coll.enabled = false; 
            CurrentState = "death"; 
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp; 
        }   


    }
}
