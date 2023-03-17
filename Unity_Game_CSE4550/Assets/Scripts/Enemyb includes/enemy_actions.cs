using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_actions : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D Enemy;
    int enemyHp; 
    int CurrentHp; 
    private BoxCollider2D coll;
    public SpriteRenderer sprite_filp;
    

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
        }
        else if(enemyHp != CurrentHp)
        {
            CurrentHp = enemyHp;  
        }   

    }//*/


    public string checkHP(string State)
    {
        CurrentHp = enemyHp; 
        Debug.Log(CurrentHp);
        Debug.Log(enemyHp);
        enemyHp = GetComponent<enemy_class>().CurrentHp(); 


        if(enemyHp <= 0)
        {
            Debug.Log("DEATH");
            State = "DEATH";  
        }
        else if(enemyHp != CurrentHp)
        {
            Debug.Log("HURT");
            State = "HURT"; 
        }
        return State; 
    } 



    public float max_x; 
    public float min_x;

    public float W_speed = 1.5f; 

    public float transX;
    public float transY;


   public  void walk()
    {
        if(sprite_filp.flipX == false)
        {
            Enemy.velocity= new Vector2(  W_speed ,Enemy.velocity.y);
            attTrans.transform.localPosition = new Vector3(transX,transY,0);
        }
        else 
        {
             Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
             attTrans.transform.localPosition = new Vector3(-transX,transY,0);
        }
    }

    public void enemy_stop()
    {
        Enemy.velocity= new Vector2(  0 ,Enemy.velocity.y);
    }





/////////////////////////////////////////////////////////////////////////////////////////////

public Transform attTrans; 
public float att_range_y ;  //1 
public float att_rangex_ ;  //1


//*///////////////////////////test the saiz of the hit box //////////////////////////////////
 void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attTrans.position, att_range);
        Gizmos.DrawWireCube(attTrans.position, new Vector3(att_rangex_, att_range_y,1));
    
    }
////////////////////////////////////////////////////////////////////////////////////////////*


public LayerMask playerlayers; 

public bool  trigger_attack() 
    {      
        Debug.Log("checking if bool read");
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         
            if(hitplayer.Length > 0)
            {
                return true; 
            }
            else
                return false;
    }



public int enemy_daamge = 1 ;  



public void damage_player()
{
    Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         

           if(hitplayer.Length > 0)//for(int i = 0; i < hitplayer.Length; i++)
            {
                hitplayer[0].GetComponent<Player_Heath>().player_takeDamage(enemy_daamge);
            }
}

}
