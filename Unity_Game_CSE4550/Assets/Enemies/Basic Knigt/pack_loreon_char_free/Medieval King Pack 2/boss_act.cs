using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_act : MonoBehaviour
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

    public LayerMask playerlayers; 


    public Transform viewTrans; 
    public float v_range_y ;  //1 
    public float v_range_x ;  //1


//*///////////////////////////test the saiz of the hit box //////////////////////////////////
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(viewTrans.position, new Vector3(v_range_x, v_range_y,1));


        Gizmos.color = Color.red;
       Gizmos.DrawWireCube(attTrans.position, new Vector3(att_rangex_, att_range_y,1));

    
    }


    public bool skel_awake()
    {
            Debug.Log("skeleton will awaken ");
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(viewTrans.position, new Vector2(v_range_x, v_range_y),0, playerlayers );         
        
        if(hitplayer.Length > 0)
            return true; 
        else
            return false; 
    }

    public Transform attTrans; 
    public float att_range_y ;  //1 
    public float att_rangex_ ;  //1


    public bool  trigger_attack() 
    {      
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         
            if(hitplayer.Length > 0)
            {
                return true; 
            }
            else
                return false;
    }

    public int enemy_daamge = 1 ;

    public Rigidbody2D player_RB;

    public void damage_player()
    {
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, playerlayers );         

           if(hitplayer.Length > 0)//for(int i = 0; i < hitplayer.Length; i++)
            {
                hitplayer[0].GetComponent<Player_Heath>().player_takeDamage(enemy_daamge);
               player_RB.AddForce(transform.right * pushback_force, ForceMode2D.Impulse);
            }
    }

    public float max_x; 
    public float min_x; 
    public float transX;
    public float transY;
    public float W_speed = 1.5f; 
    public float KnockBack_force = 0f; 
    float  pushback_force; 



    public void walk()
    {
        if(sprite_filp.flipX == false)
        {
            Enemy.velocity= new Vector2(  W_speed ,Enemy.velocity.y);
            attTrans.transform.localPosition = new Vector3(transX,transY,0);
            pushback_force = KnockBack_force; 
        }
        else 
        {
             Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
             attTrans.transform.localPosition = new Vector3(-transX,transY,0);
             pushback_force = -KnockBack_force; 
            
        }
    }





    public void jump()
    {
        Enemy.velocity = new Vector2(Enemy.velocity.x,7f);
    }






     public string checkHP(string State)
    {
        CurrentHp = enemyHp; 
        enemyHp = GetComponent<enemy_class>().CurrentHp(); 
        if(enemyHp <= 0)
        {
            Debug.Log("DEATH");
            State = "DEATH";  
        }
        return State; 
    } 

    public void enemy_stop()
    {
        Enemy.velocity= new Vector2(  0 ,Enemy.velocity.y);
    }



    /////////////////////////////////////// charge SKELE

    public LayerMask ground; 

    public bool ground_col() 
    {      
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, ground );         
            if(hitplayer.Length > 0)
            {
                return true; 
            }
            else
                return false;
    }

    public void kill_enemy()
    {
        GetComponent<enemy_class>().enemyHp = 0; 
    }
    
    //////////////////////////////////////


    public Transform playerpos; 
    public Transform enemypos; 


    public void filp_to_player()
    {
        if( sprite_filp.flipX == true && (playerpos.position.x > enemypos.position.x ) )
        {
            sprite_filp.flipX =false; 
        }
        else if(sprite_filp.flipX == false && (playerpos.position.x < enemypos.position.x ))
        {
            sprite_filp.flipX =true; 
        }
        return; 
    }


}
