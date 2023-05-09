using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETLESKELE : basic_skele_class
{
   public Animator anim;

    public string CurrentState ;

    public bool awaking = true;   
    float awakening_delay = 0f; 


    float ATT_delay = 0f; 
    float STUN_delay = 1f; 
    float stun_time =0; 
    // Start is called before the first frame update
    public bool right_facing = false; 

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

        if(right_facing) 
        {
            sprite_filp.flipX = false;
        }
        else 
        {
             sprite_filp.flipX = true;
        }


    }

    // Update is called once per frame
    void Update()
    {

        if( CurrentState != "base" &&  CurrentState !="awaking")
            CurrentState = checkHP(CurrentState); 
        else if(CurrentState != "HURT" && CurrentState != "IDE")
        {
            enemy_stop();
            stun_time = Time.time + STUN_delay; 
        }

        if(CurrentState == "DEATH")
        {
            stun_time = Time.time + STUN_delay; 
        }


        switch(CurrentState) 
        {
            /////////////////////////////////////////////////
            case "base": // BONE state 0 
            //////////////////////////////////////////////////
                {
                    anim.SetInteger("state", 0 ); 
                    if(skel_awake())
                    {
                         Debug.Log("BASE");
                        awakening_delay = Time.time + 1;    
                        CurrentState = "awaking"; 
                         anim.SetInteger("state", 1);
                    }
                }
                break;
            /////////////////////////////////////////////////////
            case "awaking": // AWAKING STATE 1 
            /////////////////////////////////////////////////////
                {
                    
                     if(awakening_delay <= Time.time )
                     {
                        CurrentState = "IDE";
                        Debug.Log("awaking");
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
                            enemy_stop();
                            ATT_delay  = Time.time + 0.52f;
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
                    stun_time = Time.time + STUN_delay;
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
                    //enemy_stop();
                    ATT_delay  = Time.time + 0.52f;
                        CurrentState = "ATT"; 
                }
             break;
            ///////////////////////////////////////////////////////
             case "HURT": // Hurt STATE // 5 
            /////////////////////////////////////////////////////// 
                //enemy_stop();
                  //anim.SetInteger("state", 5); 
                if(stun_time <= Time.time )
                {
                    stun_time = Time.time + STUN_delay; 
                    Debug.Log("Hurt_skeleton");
                    CurrentState = "IDE"; 
                }
                break; 
            /////////////////////////////////////////////////////
            case "DEATH":
            ////////////////////////////////////////////////////
                if(stun_time <= Time.time )
                {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
            /////////////////////////////////////////////////////
            default:
            if(stun_time <= Time.time )
                {
                Debug.Log("Unknown Action"); 
                }
                break;
        }
    }
}
