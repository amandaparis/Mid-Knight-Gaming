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
        public GameObject deadParticle;
      


      // private enum MovementState{Idle, Death}
        
        private void Start()
        {
         Enemy = GetComponent<Rigidbody2D>(); 
         coll = GetComponent<BoxCollider2D>();
         anim = GetComponent<Animator>();

          animations_update();

        }

    // Update is called once per frame
        private void Update()
        {
          

        }

//Activates on keyframe
private void Death()
    {
        
        //enable the head, turn off physics, and turn off collisions
        head.SetActive(true);
        Enemy.bodyType = RigidbodyType2D.Static;
        coll.isTrigger = true;
        
    }
  
  //Activates on keyframe
  private void activeParticles()
  {
      deadParticle.SetActive(true);
  }

    
  private void animations_update()
  {
    anim.SetTrigger("Skeleton_death");

  }
}
