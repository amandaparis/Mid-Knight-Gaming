using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : PlayerActions//: MonoBehaviour
{
    // Start is called before the first frame update
    public /*private*/ Animator anim;
    public string CurrentState; 
   /////////////////////////////////////////////////////////////////////////
    float an_delay = 0f;
    float attack_rate = 1f ;
    float ATTACK_delay = 0f; // (Time.time >= ATTACK_delay)  ATTACK_delay= Time.time +1f/ attack_rate;
   //////////////////////////////////////////////////////////////////////////
   float slide_rate = 1f; 
    float SLIDE_delay = 0f ; 
    float slide_an_delay = 0f;
    /////////////////////////////////////////////////////////////////////////
    [SerializeField] private AudioSource attackSoundEffect;
    void Start()
    {
        CurrentState = "IDE";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    ///STATES: IDE RUN JUMP  2ndJUMP  FALLING  crouching crouchwalking  attck#1 attck#2 attck#3 Airattack   slide |  Special STATES:  hurt  Death 
    //ACTIONS:  0   1   2       3        4         5           6           7       8      9         10       11   |                    12     13    

    void Update()
    {
        if (CurrentState == "ATT_1" || CurrentState == "ATT_2" || CurrentState == "ATT_3" || CurrentState == "ATT_AIR" )
        {
            attackSoundEffect.Play();
        }
        switch(CurrentState)            
        {
                case "IDE": // IDE STATEs // 0
                ////////////////////////////////////////////////////////
                    anim.SetInteger("state", 0 ); 
                    if(player.velocity.y < -.1f)
                    {
                        CurrentState = "FALLING";
                    }
                    else if(Input.GetButtonDown("Jump") )  
                    {
                        jumping();
                        anim.SetInteger("state", 2 );
                        CurrentState = "JUMP";
                    }
                    else if(Input.GetButton("Sword") && (Time.time >= ATTACK_delay/2)) //(Time.time >= ATTACK_delay) )
                    {
                        attack();
                        an_delay  = Time.time +1f/2; 
                        
                        CurrentState = "ATT_1";
                    }
                    else if(Input.GetButton("DUCKING"))
                    {
                        head_hit_box.enabled = false; 
                        CurrentState ="DUCK_IDE";
                    }
                    else if(Input.GetButton("Horizontal"))
                    {
                        CurrentState = "RUN";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "RUN": // RUNNING STATE // 1 
                /////////////////////////////////////////////////////// 
                    running(); 
                    anim.SetInteger("state", 1 ); 
                    if(player.velocity.y < -.1f)
                    {
                        CurrentState = "FALLING";
                    }
                    else if(Input.GetButton("Jump"))
                    {
                        jumping();
                        anim.SetInteger("state", 2 );
                        CurrentState = "JUMP";
                    }
                    else if(Input.GetButton("DUCKING"))
                    {
                        head_hit_box.enabled = false; 
                        CurrentState ="DUCK_IDE";
                    }
                    else if(Input.GetButton("Sword") && (Time.time >= ATTACK_delay/2))//(Time.time >= ATTACK_delay) )
                    {
                        an_delay  = Time.time +1f/2; 
                        CurrentState = "ATT_1";
                    }
                    else if(!Input.GetButton("Horizontal"))
                    {
                         CurrentState = "IDE";
                    }
                    else if(Input.GetButton("slide") && (Time.time >= SLIDE_delay) )
                    {
                         slide_an_delay  = Time.time +1f/2;
                        head_hit_box.enabled = false; 
                        CurrentState = "SLIDE";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "JUMP": // JUMPING state // 2
                ////////////////////////////////////////////////////////
                    running();
                     if(Input.GetButtonDown("Jump")) 
                    {
                        jumping();
                        CurrentState = "2ndJMP";
                    }
                    else if(player.velocity.y < -.1f)
                    {
                        CurrentState ="FALLING";
                    }
                    else if(Input.GetButton("Sword") && (Time.time >= ATTACK_delay/2) )
                        {
                        attack();
                        an_delay  = Time.time +1f/2; 
                        CurrentState = "ATT_AIR";
                        }
                        else if(Isgound())
                        {
                            CurrentState ="IDE";
                        }
                    break; 
                /////////////////////////////////////////////////////////////
                case "2ndJMP": // air Rolling State // 3 
                //////////////////////////////////////////////////////////////
                    anim.SetInteger("state", 3 );
                    running();
                        if(Isgound())
                        {
                            CurrentState ="IDE";
                        }
                    break; 
                ///////////////////////////////////////////////////////
                case "FALLING": // Falling State // 4 
                //////////////////////////////////////////////////////
                       running(); 
                       anim.SetInteger("state", 4 );     
                        if(Input.GetButtonDown("Jump"))
                        {
                          jumping();
                          CurrentState = "2ndJMP";
                         } 
                       else if(player.velocity.y > -.1f)
                        {
                            CurrentState ="IDE";
                        }
                        else if(Input.GetButton("Sword") && (Time.time >= ATTACK_delay/2) )
                        {
                        attack();
                        an_delay  = Time.time +1f/2; 
                        CurrentState = "ATT_AIR";
                        }
                    break;  
                /////////////////////////////////////////////////////////
                case "DUCK_IDE": // crouching state // 5
                /////////////////////////////////////////////////////////
                    anim.SetInteger("state", 5 ); 
                    if(player.velocity.y < -.1f)
                    {
                        head_hit_box.enabled = true; 
                        CurrentState = "FALLING";
                    }
                    else if(Input.GetButton("Horizontal"))
                    {
                        CurrentState ="DUCK_WALK";
                    }
                    else if(!Input.GetButton("DUCKING") )
                    {
                        head_hit_box.enabled = true; 
                        if(Isceiling())
                        {
                            head_hit_box.enabled = false; 
                            break;
                        }
                        CurrentState ="IDE";
                    } 
                    break;
                ////////////////////////////////////////////////////////
                case "DUCK_WALK": // crouching walk state // 6
                /////////////////////////////////////////////////////////
                    anim.SetInteger("state", 6 ); 
                     x_dir(3.5f);
                    if(player.velocity.y < -.1f)
                    {
                        head_hit_box.enabled = true; 
                        CurrentState = "FALLING";
                    }
                    else if(!Input.GetButton("Horizontal"))
                    {
                        CurrentState ="DUCK_IDE";
                    }
                    else if(Input.GetButton("slide") && (Time.time >= SLIDE_delay) )
                    {
                         slide_an_delay  = Time.time +1f/2;
                        head_hit_box.enabled = false; 
                        CurrentState = "SLIDE";
                    }
                    else if(!Input.GetButton("DUCKING") )
                    {
                        head_hit_box.enabled = true; 
                        if(Isceiling())
                        {
                            head_hit_box.enabled = false; 
                            break;
                        }
                        CurrentState ="IDE";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "ATT_1": // attack state 1 // 7
                ////////////////////////////////////////////////////////
                x_dir(0.1f);// stops sliding when attacking
                anim.SetInteger("state", 7);
                if(an_delay <= Time.time)
                {
                    if(Input.GetButton("Sword") )
                    {
                        attack();
                        an_delay  = Time.time +1f/2; 
                        CurrentState = "ATT_2";
                    }
                 else
                    {
                    ATTACK_delay= Time.time +1f/ attack_rate;
                    CurrentState ="IDE";
                    x_dir(1.5f);
                    }
                }
                break;
                ////////////////////////////////////////////////////////
                case "ATT_2": // attack state 1 // 8
                ////////////////////////////////////////////////////////
                anim.SetInteger("state", 8);
                if(an_delay <= Time.time)
                {
                    if(Input.GetButton("Sword") )
                    {
                        attack();
                        an_delay  = Time.time +1f/2; 
                        CurrentState = "ATT_3";
                    }
                 else
                    {
                        ATTACK_delay= Time.time +1f/ attack_rate;
                        CurrentState ="IDE";
                        x_dir(1.5f);
                    }
                }
                break;
                ////////////////////////////////////////////////////////
                case "ATT_3": // attack state 1 // 9
                ////////////////////////////////////////////////////////
                anim.SetInteger("state", 9);
                if(an_delay <= Time.time)
                {
                    ATTACK_delay= Time.time +1f/ attack_rate;
                    CurrentState ="IDE";
                     x_dir(1.5f);
                }
                break;
                ////////////////////////////////////////////////////////
                case "ATT_AIR": // attack state 1 // 10
                ////////////////////////////////////////////////////////
                anim.SetInteger("state", 10);
                x_dir(3.5f);
                if(an_delay <= Time.time)
                {
                    if(Input.GetButtonDown("Jump"))
                        {
                          jumping();
                          CurrentState = "2ndJMP";
                        } 
                    else 
                        {
                          CurrentState ="FALLING";
                        }
                    ATTACK_delay= (Time.time +1f/ attack_rate)/2;
                }
                break;
                ///////////////////////////////////////////////////////
                case "SLIDE": // slide // 11
                ///////////////////////////////////////////////////////
                    anim.SetInteger("state", 11);
                    slide(); 
                if((slide_an_delay <= Time.time))
                {
                    x_dir(0.1f);
                    if(player.velocity.y < -.1f)
                    {
                        SLIDE_delay = Time.time +1f/slide_rate; 
                        head_hit_box.enabled = true; 
                        CurrentState = "FALLING";
                    }
                    else 
                    {
                        SLIDE_delay = Time.time +1f/slide_rate; 
                        CurrentState ="DUCK_IDE";
                    }   
                }
                break;
                ///////////////////////////////////////////////////////
                default:
                    Debug.Log("Unknown Action"); 
                    break;
        }
    }
}

