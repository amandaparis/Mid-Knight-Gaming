using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat_Movement : MonoBehaviour
{
    public float speed; // adjust this to control the speed of the enemy

    private Transform player; // reference to the player's transform

    private Vector3 newPosition;

    void Start()
    {
        // Find the player game object and get its transform
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }


    public void FollowPlayer()
    {
        //Calculate the difference, to get the distance
        Vector3 direction = new Vector3((player.position.x - transform.position.x), 0f, 0f);
        
        //Used to conserve the direction, but not the distance
        direction.Normalize();

        Vector3 velocity =  direction * speed;

                     //Position of enemy    //New position of player moving within an elapsed frame
        newPosition = transform.position +  (velocity  * Time.deltaTime) ;

        /* 
        Used to check if the distance falls within the value so, the enemy
        doesn't does not constantly does not overshoot due floating precision errors
        */
        if(Mathf.Abs(player.position.x - transform.position.x) > 0.1f )
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition , 2.5f);
        }



    }
    
}
