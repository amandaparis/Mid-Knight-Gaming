using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_EN : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Enemy;
    int enemyHp; 
    int CurrentHp; 
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
    //*
    void Update()
    {

        enemyHp = GetComponent<enemy_class>().CurrentHp(); 

        if(enemyHp <= 0)
        {
            coll.isTrigger = true; 
            Enemy.bodyType = RigidbodyType2D.Static;
            coll.enabled = false; 
            //CurrentState = "DEATH"; 
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp; 

            //CurrentState = "HURT"; 
           
        }   

    }//*/


    public string checkHP(string State)
    {
        CurrentHp = enemyHp; 
        enemyHp = GetComponent<enemy_class>().CurrentHp(); 

    
    
    //Debug.Log(" Enemy: " + enemyHp);

    //Debug.Log(" Current: " + CurrentHp);

        if(enemyHp <= 0)
        {
            //coll.isTrigger = true; 
            //Enemy.bodyType = RigidbodyType2D.Static;
            //coll.enabled = false; 
            State = "DEATH";  
        }
        else if(enemyHp != CurrentHp)
        {
            //CurrentHp = enemyHp;
            State = "HURT"; 
        }
        return State; 
    } 



    public float max_x; 
    public float min_x;

    public float W_speed = 1.5f; 

   public  void walk()
    {

        if(sprite_filp.flipX == false)
            Enemy.velocity= new Vector2(  W_speed ,Enemy.velocity.y);
        else 
             Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
    }

    public void enemy_stop()
    {
        Enemy.velocity= new Vector2(  0 ,Enemy.velocity.y);
    }





/////////////////////////////////////////////////////////////////////////////////////////////

public Transform attTrans; 

//public float att_range; 

public float att_range_y ;// 1 
public float att_rangex_ ;  //1


//*///////////////////////////test the saiz of the hit box //////////////////////////////////
 void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attTrans.position, att_range);
        Gizmos.DrawWireCube(attTrans.position, new Vector3(att_rangex_, att_range_y,1));
    
    }
////////////////////////////////////////////////////////////////////////////////////////////*






}
