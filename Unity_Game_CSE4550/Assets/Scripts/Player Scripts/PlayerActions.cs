using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
        public /*private*/  Rigidbody2D player; 
        public /*private*/ SpriteRenderer sprite_filp; 
        public /*private/  CircleCollider2D */ BoxCollider2D head_hit_box;
        public /*private*/  BoxCollider2D coll; 

      [SerializeField] private LayerMask jumpable_ground; 

        void Start()
        {
            player = GetComponent<Rigidbody2D>(); 
            coll = GetComponent<BoxCollider2D>();
            sprite_filp = GetComponent<SpriteRenderer>() ;
            head_hit_box = GetComponent</*CircleCollider2D*/ BoxCollider2D>();
        } 



/////////////////////////////////////////////////////////////////////////////////////////////

public Transform attTrans; 

public float att_range; 

 void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attTrans.position, att_range);
    }


public LayerMask enemylayers; 
int attack_damage = 50;

public void attack() 
    {      
            Debug.Log("Sword read");
            Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attTrans.position, att_range, enemylayers ); 
            
            foreach(Collider2D en in hitenemies)
            {
                //en.GetComponent<Animations>().takedamage(attack_damage); 
            } 
    }



////////////////////////////////////////////////////////////////////////////////////////////


    ///movements 

    public void running()
    {
       Debug.Log(attTrans.transform.localPosition.x); 


        float dirx =  Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2( 7f * dirx, player.velocity.y);
        if(dirx >  0f)
            { 
              sprite_filp.flipX = false;
              attTrans.transform.localPosition = new Vector3(0.1f,0,0); 
            }
            else if(dirx <  0f)
            {
                sprite_filp.flipX = true;
              attTrans.transform.localPosition = new Vector3(-0.1f,0,0);
            }
    }



  public void x_dir(float speed)
    {
        float dirx =  Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2( speed * dirx, player.velocity.y);
        if(dirx >  0f)
            { 
             sprite_filp.flipX = false;
              attTrans.transform.localPosition = new Vector3(0.1f,0,0); 
            }
            else if(dirx <  0f)
            {
                sprite_filp.flipX = true;
              attTrans.transform.localPosition = new Vector3(-0.1f,0,0);
            }
    }


  public void jumping()
  {
    player.velocity = new Vector2(player.velocity.x,7f);
  }

  ///check for states: 
   public bool Isgound() 
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpable_ground); 
  } 
 
  public bool Isceiling() 
  {
    return Physics2D.BoxCast(head_hit_box.bounds.center, head_hit_box.bounds.size, 0f, Vector2.up, .1f, jumpable_ground); 
  }  

}
