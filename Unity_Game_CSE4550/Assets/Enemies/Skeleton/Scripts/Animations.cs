using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{

        // Start is called before the first frame update
        private Rigidbody2D Enemy; 
        private BoxCollider2D coll; 
        private Animator anim;
        public GameObject head;
      


      private enum MovementState{Idle, Death}
        
        private void Start()
        {
         Enemy = GetComponent<Rigidbody2D>(); 
         coll = GetComponent<BoxCollider2D>();
         anim = GetComponent<Animator>();
        }

    // Update is called once per frame
        private void Update()
        {
          
          animations_update();
        }


private MovementState Death()
    {
        MovementState state;
        
        //enable the head, turn off physics, and turn off collisions
        head.SetActive(true);
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.isTrigger = true;

        state = MovementState.Death;

        //TODO: Add particles
        return state; 
    }

    
  private void animations_update()
  {
    MovementState state; 

    state = Death();
    anim.SetInteger("state", (int)state ); 

  }
}
