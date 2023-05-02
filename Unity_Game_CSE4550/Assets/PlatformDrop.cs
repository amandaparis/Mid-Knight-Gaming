using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDrop : MonoBehaviour
{
    public float time;
    private Rigidbody2D rb;
    private Collider2D coll;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            StartCoroutine(wait());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator wait() {
        yield return new WaitForSeconds(time);
        rb.isKinematic = false;
        coll.isTrigger = true;
    }
    
}
