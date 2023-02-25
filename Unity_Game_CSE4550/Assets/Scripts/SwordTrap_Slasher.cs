using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrap_Slasher : MonoBehaviour
{
    [SerializeField] private float Damage;

    [Header ("Trap Timer")]
    [SerializeField] private float activateTime;


    private Transform arm;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        arm = transform.Find("Arm");  //Get references from child
        anim = arm.GetComponent<Animator>();
        spriteRend = arm.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

   private void OnTriggerEnter2D(Collider2D collision) 
   
    {
       if (collision.tag == "Player") 
            {
                {
                    if(!triggered)
                    {
                        StartCoroutine(ActivateTrap());
                    }
                    if(active)
                    {
                       //TODO: Take Damage
                    }
                }
            }
    }

    private IEnumerator ActivateTrap()
    {
        triggered = true;
        active = true;
        anim.SetBool("Slash",true);

        yield return new WaitForSeconds(activateTime);
        active = false;
        triggered = false;
        anim.SetBool("Slash",false);
    }
}
