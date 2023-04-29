using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Chest : MonoBehaviour
{
    public Transform actTrans;
    public float transX;
    public float transY;
    public float actrange_x;
    public float actrange_y;
    public float spawn_offset_x;
    public float spawn_offset_y;
    public float spawn_vel_offset_x;
    public float spawn_vel_offset_y;
    public GameObject item;

    private LayerMask playerlayer;
    private SpriteRenderer sprite_renderer;
    private Animator anim;
    private new Rigidbody2D rigidbody;
    private bool has_spawned = false;
    private bool flipped;

    void Start()
    {
        playerlayer = LayerMask.GetMask("PlayerLayer");

        anim = GetComponent<Animator>();
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        flipped = sprite_renderer.flipX;
        attackbox_flip();

    }

    // Update is called once per frame
    void Update()
    {
        if (is_triggered())
        {
            // spawn_item() Self Triggered as keyframe event for the open animation
            anim.SetInteger("state", 1);
        }

    }


    private void attackbox_flip()
    {
        switch (flipped)
        {
            case true:
                actTrans.transform.localPosition = new Vector3(transX, transY, 0);

                break;
            case false:
                actTrans.transform.localPosition = new Vector3(-transX, transY, 0);

                break;
            default:
        }
    }

    private bool is_triggered()
    {
        Collider2D[] touched = Physics2D.OverlapBoxAll(actTrans.position, new Vector2(actrange_x, actrange_y), 0, playerlayer);
        foreach (Collider2D col in touched)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(actTrans.position, new Vector3(actrange_x, actrange_y, 1));

    }
    private void spawn_item() //Self Triggered as keyframe event for the open animation
    {
        if (!has_spawned)
        {
            has_spawned = true;
            GameObject spawned_item = Instantiate(item);
            switch (flipped)
            {
                case true:
                    spawned_item.transform.position = 
                        new Vector2(
                            gameObject.transform.position.x + spawn_offset_x, 
                            gameObject.transform.position.y + spawn_offset_y
                        );

                    spawned_item.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(
                            spawned_item.GetComponent<Rigidbody2D>().velocity.x + spawn_vel_offset_x, 
                            spawned_item.GetComponent<Rigidbody2D>().velocity.y +spawn_vel_offset_y
                        );

                    break;
                case false:
                    spawned_item.transform.position = 
                        new Vector2(
                            gameObject.transform.position.x - spawn_offset_x, 
                            gameObject.transform.position.y + spawn_offset_y
                        );

                    spawned_item.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(
                            spawned_item.GetComponent<Rigidbody2D>().velocity.x - spawn_vel_offset_x, 
                            spawned_item.GetComponent<Rigidbody2D>().velocity.y - spawn_vel_offset_y
                        );

                    break;
                default:
            }
        }

    }
}
