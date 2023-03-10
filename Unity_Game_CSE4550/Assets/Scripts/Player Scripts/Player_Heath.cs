using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player_Heath :  MonoBehaviour
{

    private Animator anim;
    private  Rigidbody2D player; 

    public int health;
    public int numofhearts;  

    public Image[]  hearts; 
    public Sprite fullHearts; 
    public Sprite emptyHearts; 

    //GameObject varGameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>(); 
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

    //*//// testing player taking damage ////////
        if(Input.GetKeyDown("y"))
        {
            player_takeDamage(1);
        }
    //////////////////////////////////////////*/
    }




    public void player_takeDamage(int E_damage )
    {
          health = health -E_damage; 
        if(health > 0 )
        {
            anim.SetTrigger("hurt");
            
        }
        else // death
        {
            anim.SetTrigger("death");
           // player.bodyType = RigidbodyType2D.Static; 
            GetComponent<PlayerStateMachine>().enabled = false;
        }
    }


    
    private void OnCollisionEnter2D(Collision2D collision) //Player_Attack_point
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player_takeDamage(1);
        }
    }

   
}
