using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gskele_statemechine : Gskele
{

    // public Animator anim;

    public string CurrentState ;
    float awakening_delay = 0f;
    float shake_delay = 0f; 
    float shake = 0f; 
    public float shift = 0.1f;  
    // Start is called before the first frame update
    void Start()
    {
      CurrentState = "IDE";  

    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentState)
        {
            case "IDE":
            /////////////////////////////////////////////////////
                if(skel_awake())
                {
                    CurrentState = "WALK";
                    awakening_delay = Time.time + 1; 
                }
                break;
            /////////////////////////////////////////////////////
            case "WALK":
            /////////////////////////////////////////////////////
                 if(awakening_delay <= Time.time )
                    {
                        walk();

                        if(shake_delay <= Time.time)
                        {
                            shift = -shift; 
                            shake_y(shift);
                            shake_delay = Time.time +1; 
                        }
                        damage_player(); 
                    } 
                break;
            /////////////////////////////////////////////////////
            default:
                break;
        }
        
    }
}
