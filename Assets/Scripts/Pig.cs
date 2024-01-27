using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bird" || col.contacts[0].normal.y < 0.5)
        {
            gameManager.KillPig();
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
