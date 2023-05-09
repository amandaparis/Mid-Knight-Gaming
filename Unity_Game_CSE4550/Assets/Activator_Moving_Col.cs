using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator_Moving_Col : MonoBehaviour
{
    private bool triggered = false;
    public float speed = 1.5f; // adjust this to control the speed

    private Animator anim;
    private Collider2D coll;
    private Vector3 newPosition;
    private LayerMask playerlayer;
    public GameObject obj_to_move;
    public Transform move_position;
    public Transform move_position1;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = gameObject.GetComponent<Collider2D>();
        playerlayer = LayerMask.GetMask("PlayerLayer");

        // move_position = transform.Find("Moving Point").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        is_triggered();
        if (triggered)
        {
            FollowObject(move_position);
        }
        if(GameObject.Find("BOSS_KING").GetComponent<enemy_class>().enemyHp == 0 )
        {
            Debug.Log("MOVING DOWN");
            FollowObject(move_position1);
            triggered = false;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, 1));
    }

    private void is_triggered()
    {
        Collider2D[] touched = Physics2D.OverlapBoxAll(gameObject.transform.position, new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y), 0, playerlayer);
        foreach (Collider2D col in touched)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Trigger set to true");
                triggered = true;
            }
        }
    }

    public void FollowObject(Transform Object)
    {
        Debug.Log("MOVING");
        // Calculate the difference, to get the distance
        Vector3 distance = new Vector3(0f, (Object.position.y - obj_to_move.transform.position.y), 0f);

        // Normalize the distance vector
        distance.Normalize();

        // Calculate the new position based on the speed and the distance vector
        newPosition = obj_to_move.transform.position + distance * speed * Time.deltaTime;

        // Move the object towards the new position
        if (Mathf.Abs(Object.position.y - obj_to_move.transform.position.y) > 0.1f)
        {
            obj_to_move.transform.position = Vector3.MoveTowards(obj_to_move.transform.position, newPosition, 2.5f);

        }
    }

}