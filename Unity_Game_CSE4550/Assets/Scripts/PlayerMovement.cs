using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

        // Start is called before the first frame update
        public /*private*/  Rigidbody2D player; 
        public /*private*/  BoxCollider2D coll; 
        public /*private*/ SpriteRenderer sprite_filp; 
        public /*private*/ Animator anim;
        public /*private*/  CircleCollider2D head_hit_box; 


      [SerializeField] private LayerMask jumpable_ground; 

      private enum MovementState{ide, running, jumping, falling}
        
        

        private void Start()
        {
         player = GetComponent<Rigidbody2D>(); 
         coll = GetComponent<BoxCollider2D>();
         sprite_filp = GetComponent<SpriteRenderer>() ;
         anim = GetComponent<Animator>();
         head_hit_box = GetComponent<CircleCollider2D>();
 
        }

    // Update is called once per frame
        private void Update()
        {
            float dirx =  Input.GetAxisRaw("Horizontal");


            if(attacking_state())
            {

              if(Crouching()) 
                player.velocity = new Vector2( 7f * dirx, player.velocity.y); // normal
              else
                player.velocity = new Vector2( 3.25f * dirx, player.velocity.y); // for crouching 

            }
            else if(Isgound()) 
            {
               player.velocity = new Vector2( 0.5f * dirx, player.velocity.y); // normal
            }
            


            if(Input.GetButtonDown("Jump") && Isgound() && !Isceiling() )
            {
              player.velocity = new Vector2(player.velocity.x,7f);
            }
             
            
            if (Crouching()) 
            {
              animations_update(dirx);
            }
            else 
            {
              animations_Crouching_update(dirx);
            }          

        }


/// different states: 

      public bool Crouching() 
      {
          if(Input.GetButton("Crouch") && Isgound()  || Isceiling() )
          {
             head_hit_box.enabled = false; // disables hit box

              GetComponent<player_combat>().enabled = false; 

            return false; 
          }
          else 
          {
             head_hit_box.enabled = true;
             GetComponent<player_combat>().enabled = true; 
            return true; 
          }
      }


  bool attacking_state()
  {
    if(Input.GetButton("Sword"))
            {
              Debug.Log("Sword action read");
             return false;
            }
      else 
      return true; 
  }



/////////////////////////////////////////


private void animations_Crouching_update(float dirx)
  {
      MovementState state; 

            if(dirx >  0f)
            {
              state = MovementState.running; 
              sprite_filp.flipX = false;
            }
            else if(dirx <  0f)
            {
              state = MovementState.running; 
              sprite_filp.flipX = true;   
            }
            else
            {
             state = MovementState.ide; 
            }
            anim.SetInteger("state", (int)state + 4 ); 
  }




///////////////////////////////////////

    
  private void animations_update(float dirx)
  {
      MovementState state; 

            if(dirx >  0f)
            {
              state = MovementState.running; 
              sprite_filp.flipX = false;
            }
            else if(dirx <  0f)
            {
              state = MovementState.running; 
              sprite_filp.flipX = true;   
            }
            else
            {
             state = MovementState.ide; 
            }

            if(player.velocity.y > .1f)
            {
              state = MovementState.jumping; 
            }
            else if(player.velocity.y < -.1f)
            {
              state = MovementState.falling; 
            }

            anim.SetInteger("state", (int)state ); 
  }

  public bool Isgound() 
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpable_ground); 
  }  

  public bool Isceiling() 
  {
    return Physics2D.BoxCast(head_hit_box.bounds.center, head_hit_box.bounds.size, 0f, Vector2.up, .1f, jumpable_ground); 
  }  




}
