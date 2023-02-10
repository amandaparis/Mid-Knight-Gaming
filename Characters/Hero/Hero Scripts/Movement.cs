using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walk_speed = 5f;
    private float movementX;
    
    private Animator anim;
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerWalk();
        playerWalkAnimation();
        
    }

    //Function for player to walk
    void playerWalk()
    {
        //Movement X will be 1 if press right arrow or d, will -1 if left arrow or a
        movementX = Input.GetAxisRaw("Horizontal");

        //Movement x will determine if the player moves left or right, while speed is a factor 
        transform.position += new Vector3(movementX,0f,0f) * Time.deltaTime * walk_speed;

    }

    //Animates player's walk animation
    void playerWalkAnimation()
    {
        if (movementX > 0)
        {
            anim.SetBool("Walk",true);
            sprite.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool("Walk",true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("Walk", false);
        }

    }
}
