using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
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

    public void TakeDamage(int d)
    {
        life -= d;
        Destroy(hearts[life].gameObject); 

        if(life < 1 )
        {
            dead = true;
        }
    }
}
