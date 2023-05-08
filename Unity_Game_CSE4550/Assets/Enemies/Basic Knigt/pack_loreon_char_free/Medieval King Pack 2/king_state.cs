using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king_state : boss_act
{
    public Animator anim;
    public string CurrentState ;

    public bool awaking = true;   
    float awakening_delay = 0f; 


    float ATT_delay = 0f; 
    float STUN_delay = 1f; 
    float stun_time =0; 
    float jump_time = 0; 


    // Start is called before the first frame update
    void Start()
    {
         CurrentState = "base"; 
        
    }

    // Update is called once per frame
    void Update()
    {
          
         CurrentState = checkHP(CurrentState); 
        
        switch(CurrentState)
        {
            case "base":
            /////////////////////////////////////////
                    anim.SetInteger("state", 0 ); 
                    if(skel_awake())
                    {
                        CurrentState = "IDE"; 
                         sprite_filp.flipX = true;
                    }
                
                break;
            /////////////////////////////////////////
            case "IDE":
            ////////////////////////////////////////
                    filp_to_player(); 
                     anim.SetInteger("state", 0 ); 
                     if(stun_time <= Time.time )
                    {
                        CurrentState = "WALK";  
                        if(jump_time < Time.time)
                         jump_time = Time.time + STUN_delay + 1; 
                    }
        
            break;
            /////////////////////////////////////////
            case "WALK": 
                {
                    filp_to_player(); 
                    anim.SetInteger("state", 1 );
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
                        ATT_delay  = Time.time + 0.5f;
                        CurrentState = "ATT1"; 
                    }
                    else if(jump_time <= Time.time )
                    {
                        CurrentState = "JUMP";
                    }
                }
            break;
            /////////////////////////////////////////
            case "ATT1":
            //////////////////////////////////////////
                anim.SetInteger("state", 2 ); 
                if(ATT_delay <= Time.time)
                {
                    damage_player();
                    CurrentState ="IDE";
                     stun_time = Time.time + STUN_delay; 
                }
            break;
            /////////////////////////////////////////
            case "JUMP":
            //////////////////////////////////////////
            anim.SetInteger("state", 3 );
            jump();
                 if(ATT_delay <= Time.time)
                {
                    ATT_delay  = Time.time + 0.5f;
                        CurrentState ="ATT3";
                          damage_player();
                }
            break;
            ////////////////////////////////////////////
            case "ATT2":
            //////////////////////////////////////////
            break;
            /////////////////////////////////////////
            case "ATT3":
            //////////////////////////////////////////
            anim.SetInteger("state", 5 ); 
                if(ATT_delay <= Time.time)
                {
                    
            
                    
                    walk();
                    if(ATT_delay +1 <= Time.time)
                    {
                          damage_player();
                    CurrentState ="IDE";
                     stun_time = Time.time + STUN_delay; 
                    }
                }
            break;
            ///////////////////////////////////////////
            case "DEATH":
            ////////////////////////////////////////
            break;
            ///////////////////////////////////////
            default:
            ///////////////////////////////////////
            break;
        }

    }
}
