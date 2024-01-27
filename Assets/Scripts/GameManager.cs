using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static int lives = 3;
    private int pigsToKill;
    private int pigsKilled = 0;

    public string victorySceneName;
    public string loseSceneName;

    public float delayBeforeGoingToVictory = 0.2f;

    public TextMeshPro levelPassedText; // Reference to the UI Text for Level Passed
    public TextMeshPro livesText; // Reference to the UI Text for Lives

    // Start is called before the first frame update
    void Start()
    {
        // Find all the pigs in the scene
        List<GameObject> pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
        pigsToKill = pigs.Count;
        Debug.Log("Pigs to kill: " + pigsToKill);

        // Ensure the UI Text components are assigned
        if (levelPassedText == null)
        {
            Debug.LogError("Level Passed Text not assigned!");
        }

        if (livesText == null)
        {
            Debug.LogError("Lives Text not assigned!");
        }

        UpdateLivesText(); // Update lives text initially
    }

    void Update()
    {

    }

    public void KillPig()
    {
        pigsKilled++;
        Debug.Log("Pigs killed: " + pigsKilled);

        if (pigsKilled == pigsToKill)
        {
            // Display Level Passed text
            levelPassedText.text = "Level Passed!";
            StartCoroutine(DelayedGoToVictory());
        }
    }

    IEnumerator DelayedGoToVictory()
    {
        yield return new WaitForSeconds(delayBeforeGoingToVictory);
        SceneManager.LoadScene(victorySceneName);
    }

    public void KillBird()
    {
        if (pigsKilled < pigsToKill)
        {
            lives--;
            Debug.Log("Lives: " + lives);

            if (lives <= 0)
            {
                // Update Lives Text
                UpdateLivesText();

                // Display You Lose text
                levelPassedText.text = "You Lose!";
                StartCoroutine(DelayedGoToLose());
            }
            else
            {
                UpdateLivesText(); // Update Lives Text
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator DelayedGoToLose()
    {
        yield return new WaitForSeconds(delayBeforeGoingToVictory);
        SceneManager.LoadScene(loseSceneName);
    }

    void UpdateLivesText()
    {
        // Update Lives Text
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}
