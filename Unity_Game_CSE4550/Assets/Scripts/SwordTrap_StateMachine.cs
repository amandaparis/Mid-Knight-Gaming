using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to hold the properties
[System.Serializable]
public struct TrapProperties
{
    public Transform arm;
    public Animator anim;
    public SpriteRenderer spriteRend;
    public bool triggered;
    public bool active;
}
public class SwordTrap_StateMachine : MonoBehaviour
{
    [SerializeField] public float Damage;

    [Header("Timers")]
    [SerializeField] public float activateTime;

    [HideInInspector] public TrapProperties trapProperties;
    private string Sword_State;
    // Start is called before the first frame update
    void Start()
    {
        trapProperties.arm = transform.Find("Arm");  //Get references from child
        trapProperties.anim = trapProperties.arm.GetComponent<Animator>();
        trapProperties.spriteRend = trapProperties.arm.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////////////////////
        ////////// State Machines
        ///////////////////////////////////////////////////
        switch (Sword_State)
        {
            case "Triggered":
                if (!trapProperties.triggered && !Input.GetButton("DUCKING")) //Check if it hasn't been triggered yet, to avoid spamming it, and not ducking
                {
                    trapProperties.triggered = true;
                    trapProperties.active = true;
                    trapProperties.anim.SetBool("Slash", true);
                    StartCoroutine(ActivateTrap());
                }
                if(trapProperties.active) //If the trap is already triggered, then it's active
                {
                    Sword_State = "Active";
                }
                break;

            case "Inactive":
                trapProperties.active = false;
                trapProperties.triggered = false;
                trapProperties.anim.SetBool("Slash", false);
                break;
                
            case "Active":
                //TODO: Take damage
                Sword_State = "Triggered";
            break;
            default:
                break;
        }

    }
    ///////////////////////////////////////////////////
    ////////// Controllers
    ///////////////////////////////////////////////////
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Sword_State = "Triggered";
        }
    }

    public IEnumerator ActivateTrap()
    {
        yield return new WaitForSeconds(activateTime);
        Sword_State = "Inactive";
    }
}
