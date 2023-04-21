using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKELE_1_STATE_MACHIEN : basic_skele_class
{
    public Animator anim;

    string CurrentState ;

    public bool awaking = true;   
    float awakening_delay = 0f; 


    float STUN_delay = 1f; 
    float stun_time =0; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(awaking)
        {
            CurrentState = "base"; 
            anim.SetInteger("state", 0 );  
        }
        else
        {
            CurrentState = "IDE"; 
            anim.SetInteger("state", 1 ); 
        }
    }

    // Update is called once per frame
    void Update()
    {

        if( CurrentState != "base" &&  CurrentState !="awaking")
            CurrentState = checkHP(CurrentState); 

        if(CurrentState != "HURT" && CurrentState != "IDE")
            stun_time = Time.time + STUN_delay; 



        switch(CurrentState) 
        {
            /////////////////////////////////////////////////
            case "base":
            //////////////////////////////////////////////////
                {
                    anim.SetInteger("state", 0 ); 
                    if(skel_awake())
                    {
                        awakening_delay = Time.time + 1/2;    
                        CurrentState = "awaking"; 
                    }
                }
                break;
            /////////////////////////////////////////////////////
            case "awaking":
            /////////////////////////////////////////////////////
                {
                     anim.SetInteger("state", 1);
                     if(awakening_delay <= Time.time )
                     {
                        CurrentState = "IDE";
                     }
                }
                break;
            ///////////////////////////////////////////////////////
            case "IDE":
            ///////////////////////////////////////////////////////
                {
                    anim.SetInteger("state", 2); 
                }
                break;
            ///////////////////////////////////////////////////////
             case "HURT": // Hurt STATE // 3 
            /////////////////////////////////////////////////////// 
                //enemy_stop();
                  anim.SetInteger("state", 5); 
                if(stun_time <= Time.time )
                {
                    stun_time = Time.time + STUN_delay; 
                    Debug.Log("Hurt_skeleton");
                    CurrentState = "IDE"; 
                }
                break; 
            default:
                Debug.Log("Unknown Action"); 
                break;
        }
    }
}
