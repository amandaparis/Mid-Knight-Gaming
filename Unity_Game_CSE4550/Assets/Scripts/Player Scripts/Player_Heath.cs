using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player_Heath : PlayerStateMachine// MonoBehaviour
{



    public int health;
    public int numofhearts;  

    public Image[]  hearts; 
    public Sprite fullHearts; 
    public Sprite emptyHearts; 


    
    // Start is called before the first frame update
    void Start()
    {
        health = 6;
    }

    // Update is called once per frame
    void Update()
    {

            if(health > numofhearts)
            {
                health = numofhearts;
            }

        for(int i =0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHearts;
            }
            else
            {
                hearts[i].sprite = emptyHearts;
            }


            if(i < numofhearts)
            {   
                hearts[i].enabled =true; 
            }
            else 
            {
                hearts[i].enabled = false; 
            }
        }


        if(Input.GetKeyDown("y"))
        {
            player_takeDamage(1);
        }

    }




    public void player_takeDamage(int E_damage )
    {
          health = health -E_damage; 
        if(health > 0) //hurt 
        {
            CurrentState = "hurt"; 
        }
        else // death
        {
            CurrentState = "death";
        }
    }
}
