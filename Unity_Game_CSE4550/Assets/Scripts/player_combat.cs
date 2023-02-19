using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat /*: MonoBehaviour */: PlayerMovement
{
    
    public Transform attackpoint;
    public float attack_range = 0.5f; 
    public LayerMask enemylayers;  

    int attack_damage = 50; 

    int delay = 100000; 
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Sword"))
        {
          attack() ; 
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

        for(int i =0; i < delay; i++);

    }

   


}


