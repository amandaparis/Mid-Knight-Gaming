using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKELE_1_STATE_MACHIEN : basic_skele_class
{
    public Animator anim;

    string CurrentState ;

    public bool awaking = true;   
    float awakening_delay = 0f; 


    float ATT_delay = 0f; 
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
            case "base": // BONE state 0 
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
            case "awaking": // AWAKING STATE 1 
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
            case "IDE": // IDE STATE 2
            ///////////////////////////////////////////////////////
                {
                    anim.SetInteger("state", 2); 

                    if(stun_time <= Time.time )
                    {
                        if(trigger_attack())
                        {
                            ATT_delay  = Time.time + 1f/2;
                            //Debug.Log(ATT_delay);
                            CurrentState = "ATT";
                        }
                        else 
                        {
                            CurrentState = "WALK";
                        }
                    }
                }
                break;
            ///////////////////////////////////////////////////////
            case "ATT": // att STATE // 3 
            ///////////////////////////////////////////////////////
                anim.SetInteger("state", 3 ); 
                //Debug.Log("ATT");
                if(ATT_delay <= Time.time)
                {
                    damage_player();
                    CurrentState ="IDE";
                }
             break;
              ///////////////////////////////////////////////////////
            case "WALK": // att STATE // 4 
            ///////////////////////////////////////////////////////
            anim.SetInteger("state", 4 ); 
                if(Enemy.position.x > max_x)
                {
                    sprite_filp.flipX = true;
                }
                else if(Enemy.position.x < min_x)
                {
                    sprite_filp.flipX = false;    
                }
                walk(); 

                if(trigger_attack())
                {
                    ATT_delay  = Time.time + 1f/2;
                        CurrentState = "ATT"; 
                }
             break;
            ///////////////////////////////////////////////////////
             case "HURT": // Hurt STATE // 5 
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



    
