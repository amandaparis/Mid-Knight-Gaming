using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : PlayerActions//: MonoBehaviour
{
    // Start is called before the first frame update
    public /*private*/ Animator anim;
    public string CurrentState; 
   
     
    void Start()
    {
        CurrentState = "IDE";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    ///STATES: IDE RUN JUMP  2ndJUMP  FALLING  crouching crouchwalking slide attcking bow hurt  Death
    //ACTIONS:  0   1   2       3        4         5           6         7       8     9   10     11

    void Update()
    {

        switch(CurrentState)            
        {
                case "IDE": // IDE STATEs // 0
                ////////////////////////////////////////////////////////
                    Debug.Log("IDE State"); 
                    anim.SetInteger("state", 0 ); 
                    if(player.velocity.y < -.1f)
                    {
                        CurrentState = "FALLING";
                    }
                    else if(Input.GetButton("Jump"))// && Isgound())
                    {
                        jumping();
                        anim.SetInteger("state", 2 );
                        CurrentState = "JUMP";
                    }
                    else if(Input.GetButtonDown("DUCKING"))
                    {
                        head_hit_box.enabled = false; 
                        CurrentState ="DUCK_IDE";
                    }
                    else if(Input.GetButton("Horizontal"))
                    {
                         Debug.Log("Hor. Action read"); 
                        CurrentState = "RUN";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "RUN": // RUNNING STATE // 1 
                ///////////////////////////////////////////////////////
                    Debug.Log("Running State"); 
                    running(); 
                    anim.SetInteger("state", 1 ); 
                    if(player.velocity.y < -.1f)
                    {
                        CurrentState = "FALLING";
                    }
                    else if(Input.GetButton("Jump"))// && Isgound())
                    {
                        jumping();
                        anim.SetInteger("state", 2 );
                        CurrentState = "JUMP";
                    }
                    else if(Input.GetButtonDown("DUCKING"))
                    {
                        head_hit_box.enabled = false; 
                        CurrentState ="DUCK_IDE";
                    }
                    else if(!Input.GetButton("Horizontal"))
                    {
                         CurrentState = "IDE";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "JUMP": // JUMPING state // 2
                ////////////////////////////////////////////////////////
                    Debug.Log("JUMP STATE");
                    
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
                    else if(!Input.GetButton("DUCKING") )//&& Isceiling())
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
                    else if(!Input.GetButton("DUCKING") )//&&  Isceiling())
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
                default:
                    Debug.Log("Unknown Action"); 
                    break;
        }
    }
}

//head_hit_box.enabled = false; // disables hit boxs