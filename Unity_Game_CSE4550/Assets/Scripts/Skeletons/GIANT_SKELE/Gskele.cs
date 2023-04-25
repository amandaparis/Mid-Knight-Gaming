using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gskele : MonoBehaviour
{
    public SpriteRenderer sprite_filp;
    public Rigidbody2D Enemy;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public Transform attTrans; 
    public float att_range_y ;  //1 
    public float att_rangex_ ;  //1

    public Transform viewTrans; 
    public float v_range_y ;  //1 
    public float v_range_x ;  //1

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(viewTrans.position, new Vector3(v_range_x, v_range_y,1));

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attTrans.position, new Vector3(att_rangex_, att_range_y,1));    
    }


    public bool skel_awake()
    {
           // Debug.Log("skeleton will awaken ");
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(viewTrans.position, new Vector2(v_range_x, v_range_y),0, playerlayers );         
        
        if(hitplayer.Length > 0)
            return true; 
        else
            return false; 
    }


   

    public LayerMask playerlayers, enemylayer;

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


    public int attack_damage = 100; 

    public void damage_enemy()
    {
        Collider2D[] hitplayer = Physics2D.OverlapBoxAll(attTrans.position, new Vector2(att_rangex_, att_range_y),0, enemylayer);         

           for(int i = 0; i < hitplayer.Length; i++)
            {
                hitplayer[i].GetComponent<enemy_class>().Enemy_take_damage(attack_damage);
              // player_RB.AddForce(transform.right * pushback_force, ForceMode2D.Impulse);
            }
    }

     

    public float max_x; 
    public float min_x; 
    public float transX;
    public float transY;
    public float W_speed = 1.5f; 
    public float KnockBack_force = 0f; 
    float  pushback_force; 


    public void  shake_y(float value)
    {
        Enemy.velocity = new Vector2(Enemy.velocity.x, Enemy.velocity.y + value);
        Debug.Log(Enemy.position.y);
    }    


    public  void walk()
    {
    //    if(sprite_filp.flipX == false)
        //{
            Enemy.velocity= new Vector2(  W_speed ,0);//Enemy.velocity.y);
            //attTrans.transform.localPosition = new Vector3(transX,transY,0);
           // pushback_force = KnockBack_force; 
      //  }
        /*else 
        {
             Enemy.velocity= new Vector2(  -W_speed ,Enemy.velocity.y);
             attTrans.transform.localPosition = new Vector3(-transX,transY,0);
             pushback_force = -KnockBack_force; 
            
        }//*/
    }






}
