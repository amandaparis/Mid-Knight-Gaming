using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKELE_1_STATE_MACHIEN : basic_skele_class
{
    public Animator anim;

    string CurrentState ;

    public bool awaking = true;   
    float awakening_delay = 0f; 


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(awaking)
        {
            CurrentState = "base";  
        }
        else
        {
            CurrentState = "IDE"; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentState) 
        {
            case "base":
                {
                    anim.SetInteger("state", 0 ); 
                    if(skel_awake())
                    {
                        awakening_delay = Time.time + 1;    
                        CurrentState = "awaking"; 
                    }
                }
                break;
            case "awaking":
                {
                     anim.SetInteger("state", 1);
                     if(awakening_delay =< Time.time )
                     {
                        CurrentState = "IDE";
                     }
                }
                break;
            case "IDE":
                {
                    anim.SetInteger("state", 2); 
                }
                break;
            default:
                Debug.Log("Unknown Action"); 
                break;
        }
    }
}
