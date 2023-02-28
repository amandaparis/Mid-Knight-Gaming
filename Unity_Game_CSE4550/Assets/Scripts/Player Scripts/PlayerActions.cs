using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
        public /*private*/  Rigidbody2D player; 
        public /*private*/ SpriteRenderer sprite_filp; 
        public /*private/  CircleCollider2D */ BoxCollider2D head_hit_box;
        public /*private*/  BoxCollider2D coll; 

        //MovementState state;
        //private enum MovementState{ide, running, jumping, falling, sword, bow, slide, Crouch ,Crouch_walk} // 0 1 2 3 4 5 6 7 8 


      [SerializeField] private LayerMask jumpable_ground; 

        void Start()
        {
            player = GetComponent<Rigidbody2D>(); 
            coll = GetComponent<BoxCollider2D>();
            sprite_filp = GetComponent<SpriteRenderer>() ;
            head_hit_box = GetComponent</*CircleCollider2D*/ BoxCollider2D>();
        } 



    ///movements 

    public void running()
    {
        float dirx =  Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2( 7f * dirx, player.velocity.y);
        if(dirx >  0f)
            { 
              sprite_filp.flipX = false;
            }
            else if(dirx <  0f)
            {
              sprite_filp.flipX = true;   
            }
    }



  public void x_dir(float speed)
    {
        float dirx =  Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2( speed * dirx, player.velocity.y);
        if(dirx >  0f)
            { 
              sprite_filp.flipX = false;
            }
            else if(dirx <  0f)
            {
              sprite_filp.flipX = true;   
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
    //return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, jumpable_ground); 
    return Physics2D.BoxCast(head_hit_box.bounds.center, head_hit_box.bounds.size, 0f, Vector2.up, .1f, jumpable_ground); 
  }  

}
