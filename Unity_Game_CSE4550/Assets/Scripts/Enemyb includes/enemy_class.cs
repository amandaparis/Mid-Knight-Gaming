using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_class : MonoBehaviour
{
        public Animator anim;
        public int enemyHp;
        public int maxHp; 
       //public Player_Heath playerHP; 

 /*   void Start()
    {   
    }
    // Update is called once per frame
    void Update()
    {   
    }
*/

    public void Enemy_take_damage(int damage)
    {
            enemyHp = enemyHp - damage;
            anim.SetTrigger("hurt");
            Debug.Log("Enemy HP : " + damage);
        
            if (enemyHp <= 0)
            {
            anim.SetTrigger("death");
            Debug.Log("Enemy Death");
            }

    }

    public int CurrentHp()
    {
        return enemyHp;
    } 

    /*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          playerHP.player_takeDamage(1);
        }
    }//*/

}