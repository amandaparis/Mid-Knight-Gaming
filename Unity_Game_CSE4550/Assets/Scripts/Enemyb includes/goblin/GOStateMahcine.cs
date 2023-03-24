using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOStateMahcine : Goblin_EN //MonoBehaviour
{
    public /*private*/ Animator anim;

    float ATT_delay = 0f; 
    float STUN_delay = 1f; 
    public string CurrentState ; 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        CurrentState  = "IDE"; 
    }


    float stun_time =0; 
    // Update is called once per frame
    void Update()
    {
        CurrentState = checkHP(CurrentState); 

        if(CurrentState != "HURT" && CurrentState != "IDE" )
            stun_time = Time.time + STUN_delay; 

        switch(CurrentState) 
        {
            case "IDE": // IDE STATEs // 0
            ////////////////////////////////////////////////////////
            anim.SetInteger("state", 0 ); 
                if(stun_time <= Time.time )
                {
                    if(trigger_attack())
                    {
                        ATT_delay  = Time.time + 1f/2;
                        Debug.Log(ATT_delay);
                        CurrentState = "ATT";
                    }
                    else
                        CurrentState = "WALK"; 
                }
                break;    
            ////////////////////////////////////////////////////////
            case "WALK": // walk STATE // 1 
            ///////////////////////////////////////////////////////
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
                    ATT_delay  = Time.time + 1f/2;
                        CurrentState = "ATT"; 
                }

                break; 
            ///////////////////////////////////////////////////////
             case "ATT": // att STATE // 2 
            /////////////////////////////////////////////////////// 
                anim.SetInteger("state", 2 ); 
                Debug.Log("ATT");
                if(ATT_delay <= Time.time)
                {
                    damage_player();
                    CurrentState ="IDE";
                }

                break; 
            ///////////////////////////////////////////////////////
             case "HURT": // Hurt STATE // 3 
            /////////////////////////////////////////////////////// 
                //enemy_stop();
                if(stun_time <= Time.time )
                {
                     stun_time = Time.time + STUN_delay; 
                    CurrentState = "IDE"; 
                }
                break; 
            ///////////////////////////////////////////////////////
             case "DEATH": // Death STATE // 4
            /////////////////////////////////////////////////////// 
                break; 
            ///////////////////////////////////////////////////////
            default:
                 Debug.Log("Unknown Action"); 
                break;
        }
    }
}
