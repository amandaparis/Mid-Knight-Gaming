using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat /*: MonoBehaviour */: PlayerMovement
{
    
    public Transform attackpoint;
    public float attack_range = 0.5f; 
    public LayerMask enemylayers;  

    int attack_damage = 50; 

    public float attack_rate = 2f ;  // changes the delay of the attack rate 
    float delay = 0f;  
    
    // Update is called once per frame
    void Update()
    {

        
    
        if( Time.time >= delay   )
        {
            if(Input.GetButton("Sword")  )
            {
            attack() ; 
            delay = Time.time +1f/ attack_rate; 
            }
        } 
  
        
    }

    


    void attack() 
    {      

            anim.SetTrigger("attack");
            //Debug.Log("Redaing Sword input");
            Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackpoint.position, attack_range, enemylayers ); 

            foreach(Collider2D en in hitenemies)
            {
                en.GetComponent<Animations>().takedamage(attack_damage); 
            } 

        

    }

   


}


