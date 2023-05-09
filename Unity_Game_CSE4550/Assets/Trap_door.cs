using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_door : MonoBehaviour
{
    public Transform actTrans;
    public float transX;
    public float transY;
    public float actrange_x;
    public float actrange_y;

    private bool triggered = false;

    private Animator anim;
    private Collider2D coll;
    private LayerMask playerlayer;
    [SerializeField] private AudioSource sound;

    void Start()
    {
        anim = GetComponent<Animator>();
        coll = gameObject.GetComponent<Collider2D>();
        playerlayer = LayerMask.GetMask("PlayerLayer");
    }

    void Update()
    {
        transparent(0f);
        is_triggered();

        if (triggered)
        {
            anim.SetInteger("state", 1);
            transparent(1f);
            coll.isTrigger = false;

        }
    }

    public void transparent(float alpha)
    {
        Color oldColor = gameObject.GetComponent<Renderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(actTrans.position, new Vector3(actrange_x, actrange_y, 1));
    }

    private void is_triggered()
    {
        Collider2D[] touched = Physics2D.OverlapBoxAll(actTrans.position, new Vector2(actrange_x, actrange_y), 0, playerlayer);
        foreach (Collider2D col in touched)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Trigger set to true");
                triggered = true;
            }
        }
        // return false;
    }

    public void play_sound()
    {
        sound.Play();
    }

}

