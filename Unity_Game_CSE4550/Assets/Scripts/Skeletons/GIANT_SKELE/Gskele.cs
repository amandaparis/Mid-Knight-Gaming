using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gskele : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/




    private void OnCollisionEnter2D(Collision2D collision) //Player_Attack_point
    {
        Debug.Log("Giant_SKELETON_COLLIDE");
        if (collision.gameObject.CompareTag("Enemy"))
        {
           GetComponent<enemy_class>().Enemy_take_damage(100);
           Debug.Log("ENEMY HAS DIED");
        }

        if(collision.gameObject.CompareTag("Player"))
        {
             GetComponent<Player_Heath>().player_takeDamage(1);
              Debug.Log("PLAYER HAS DIED");
        }
    }




}
