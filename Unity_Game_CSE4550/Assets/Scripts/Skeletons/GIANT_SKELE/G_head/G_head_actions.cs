using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_head_actions : MonoBehaviour
{
    
    public Rigidbody2D Enemy;
    //public Transform g_skele; 
    public Transform attTrans; 
    public float radius;  //1 


    public Transform viewTrans; 
    public float v_range_y ;  //1 
    public float v_range_x ;  //1

void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(viewTrans.position, new Vector3(v_range_x, v_range_y,1));

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attTrans.position, radius);    
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
        Collider2D[] hitplayer =  Physics2D.OverlapCircleAll(attTrans.position, radius,0, playerlayers );         

           if(hitplayer.Length > 0)//for(int i = 0; i < hitplayer.Length; i++)
            {
                hitplayer[0].GetComponent<Player_Heath>().player_takeDamage(enemy_daamge);
              // player_RB.AddForce(transform.right * pushback_force, ForceMode2D.Impulse);
            }
    }


    public int attack_damage = 100; 

    public void damage_enemy()
    {
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attTrans.position, radius ,0, enemylayer);         



           for(int i = 0; i < hitplayer.Length; i++)
            {
                Debug.Log(i);
                hitplayer[i].GetComponent<enemy_class>().Enemy_take_damage(attack_damage);
              // player_RB.AddForce(transform.right * pushback_force, ForceMode2D.Impulse);
            }
    }


     public float W_speed = 1.5f; 
        public float rotate_speed; 

      public  void walk()
    {
         Enemy.velocity= new Vector2(  W_speed ,0);
         transform.Rotate(0 , 0 , rotate_speed);
    }

}
