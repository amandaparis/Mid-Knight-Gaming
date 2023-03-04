using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player_Heath : MonoBehaviour
{



    public int health;
    public int numofhearts;  

    public Image[]  hearts; 
    public Sprite fullHearts; 
    public Sprite emptyHearts; 




    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < hearts.Length; i++)
        {
            if(i < numofhearts)
            {   
                hearts[i].enabled =true; 
            }
            else 
            {
                hearts[i].enabled = false; 
            }
        }
    }
}
