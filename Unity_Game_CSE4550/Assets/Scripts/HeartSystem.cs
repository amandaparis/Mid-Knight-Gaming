using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem /*: MonoBehaviour */: PlayerMovement
{

        public int maxHP = 200; 
        public int currentHp;

    public GameObject[] hearts;
    private int life; 
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        if(dead == true)
        {
            //death animation plays / lock character 
            //trow up end game screen
        }
    }

    public void Player_TakeDamage(int damage)
    {

         currentHp = currentHp - damage;

        Debug.Log("Enemy HP : " + damage);
        if(currentHp <= 0) 
        {
            player.bodyType = RigidbodyType2D.Static;
             anim.SetTrigger("death");
        }
        else
        {
            anim.SetTrigger("hurt");
            player.velocity = new Vector2( -1f, player.velocity.y);
        }

        /*life -= d;
        Destroy(hearts[life].gameObject); 

        if(life < 1 )
        {
            dead = true;
        }*/
    }
}
