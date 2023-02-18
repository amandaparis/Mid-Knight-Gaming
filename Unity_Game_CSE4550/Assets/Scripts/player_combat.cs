using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat /*: MonoBehaviour */: PlayerMovement
{
    
    public Transform attackpoint;
    public float attack_range = 0.5f; 
    public LayerMask enemylayers;  

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
            Collider2D[] hitenemies = Physics2D.OverLapCircleAll(attackpoint.position, attack_range, enemylayers ); 

            foreach(Collider2D enemy in hitenemies)
            {
                Debug.Log("Hit Enemy:" + enemy.name);
            } 

    }
}
