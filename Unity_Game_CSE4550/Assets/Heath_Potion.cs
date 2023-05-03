using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heath_Potion : MonoBehaviour
{
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int current_health = other.gameObject.GetComponent<Player_Heath>().health;

            if (current_health < 6)
            {
                other.gameObject.GetComponent<Player_Heath>().health += 1;
                GameObject spawn_effect = Instantiate(effect);
                spawn_effect.transform.parent = other.gameObject.transform;//Move into the player
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
