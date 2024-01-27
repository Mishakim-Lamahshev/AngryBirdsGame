using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody2D rb;
    public int birdSpeed;
    public GameManager gameManager; // Reference to the GameManager

    public float delayBeforeKill = 3f; // Delay before calling KillBird

    private bool birdWasLaunched = false;

    public void Awake()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = initialPosition - transform.position;
        rb.gravityScale = 1;
        rb.AddForce(directionToInitialPosition * birdSpeed);
        birdWasLaunched = true;
    }

    public void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    private bool ShouldKillBird()
    {
        return (birdWasLaunched && rb.velocity.magnitude < 0.1f);
    }

    void Update()
    {
        // Check if the bird has finished flying (velocity is very small)
        if (birdWasLaunched)
        {
            StartCoroutine(DelayedKillBird());
        }
    }

    IEnumerator DelayedKillBird()
    {
        // Introduce a short delay before calling KillBird
        yield return new WaitForSeconds(delayBeforeKill); // Adjust the delay time as needed

        // Call KillBird function on the GameManager
        if (ShouldKillBird())
        {
            gameManager.KillBird();
        }
    }
}
