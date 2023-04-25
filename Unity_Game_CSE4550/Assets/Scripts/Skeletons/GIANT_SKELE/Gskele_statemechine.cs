using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gskele_statemechine : Gskele
{

    // public Animator anim;

    public string CurrentState ;
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
                }
                break;
            /////////////////////////////////////////////////////
            case "WALK":
            /////////////////////////////////////////////////////
                walk(); 
                break;
            /////////////////////////////////////////////////////
            default:
                break;
        }
        
    }
}
