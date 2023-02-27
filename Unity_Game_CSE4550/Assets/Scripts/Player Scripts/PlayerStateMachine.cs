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

    ///STATES: IDE RUN JUMP  2ndJMP  FALLING  crouching crouchwalking slide 
    //ACTIONS:  0   1   2       3        4        5          6         7

    void Update()
    {

        switch(CurrentState)            
        {
                case "IDE": // IDE STATEs
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
                    else if(Input.GetButton("Horizontal"))
                    {
                         Debug.Log("Hor. Action read"); 
                        CurrentState = "RUN";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "RUN": // RUNING STATE
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
                    else if(!Input.GetButton("Horizontal"))
                    {
                         CurrentState = "IDE";
                    }
                    break;
                ////////////////////////////////////////////////////////
                case "JUMP": // JUMPING state
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
                case "2ndJMP":
                //////////////////////////////////////////////////////////////
                    anim.SetInteger("state", 3 );
                    running();
                        if(Isgound())
                        {
                            CurrentState ="IDE";
                        }
                    break; 
                ///////////////////////////////////////////////////////
                case "FALLING": // Falling State
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
                default:
                    Debug.Log("Unknown Action"); 
                    break;
        }
    }
}
