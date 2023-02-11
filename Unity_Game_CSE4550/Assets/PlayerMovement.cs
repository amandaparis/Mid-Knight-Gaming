using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

        // Start is called before the first frame update
        private Rigidbody2D player; 
        private BoxCollider2D coll; 
        private SpriteRenderer sprite_filp; 
        private Animator anim;


      [SerializeField] private LayerMask jumpable_ground; 

        private enum MovementState{ide, running, jumping, falling}
        


        private void Start()
        {
         player = GetComponent<Rigidbody2D>(); 
         coll = GetComponent<BoxCollider2D>();
         sprite_filp = GetComponent<SpriteRenderer>() ;
         anim = GetComponent<Animator>(); 
        }

    // Update is called once per frame
        private void Update()
        {
          float dirx =  Input.GetAxisRaw("Horizontal");

         player.velocity = new Vector2( 7f * dirx, player.velocity.y);


            if(Input.GetButtonDown("Jump") && Isgound() )
            {
            player.velocity = new Vector2(player.velocity.x,7f);
            }
            

            animations_update(dirx);
        }



    
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

  private bool Isgound() 
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpable_ground); 
  }  


}
