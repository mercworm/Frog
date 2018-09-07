using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float playerOneScore;
    public float playerTwoScore;
    public int winScore;

    public GameObject pauseMenu;
    public GameObject winScreen;

    public bool winMenuActive;
    public bool pauseMenuActive;

    public GameObject crownHolderRed;
    public GameObject crownHolderBlue;

    public KeyCode exit, keepPlaying;

    public AudioSource flySource;

    public float crownSpawnTime;

    private void Update()
    {
        //if a player presses esc, bring up pause menu.
        //If a player presses it twice, go bak to main menu.
        if (Input.GetKeyDown(exit))
        {
            if (pauseMenuActive) UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            else
            {
                pauseMenuActive = true;
                pauseMenu.SetActive(true);
            }
        }

        //if a menu is active, restart the scene completely, or close the menu. 
        if (Input.GetKeyDown(keepPlaying))
        {
            if (winMenuActive)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
            }

            if (pauseMenuActive)
            {
                pauseMenu.SetActive(false);
                pauseMenuActive = false;
            }
        }
    }

    //Check who won a round.
    private void OnEnable()
    {
        EventManager.StartListening("PlayerOneWin", PlayerOneWin);
        EventManager.StartListening("PlayerTwoWin", PlayerTwoWin);
    }

    private void OnDisable()
    {
        EventManager.StopListening("PlayerOneWin", PlayerOneWin);
        EventManager.StopListening("PlayerTwoWin", PlayerTwoWin);
    }

    //If player one won a round, add points or declare winner.
    //Otherwise reset game for next round.
    public void PlayerOneWin ()
    {
        playerOneScore++;

        if (playerOneScore == winScore)
        {
            winScreen.SetActive(true);
            winScreen.transform.GetChild(0).gameObject.SetActive(true);
            winMenuActive = true;
            EventManager.TriggerEvent("Win");
            flySource.enabled = flySource;
            return;
        }
        else
        {
            EventManager.TriggerEvent("RoundComplete");
            Debug.Log("RoundComplete!");
            StartCoroutine(AddCrownBlue());
        }
    }

    public IEnumerator AddCrownBlue ()
    {
        yield return new WaitForSeconds(crownSpawnTime);
        for (int i = 0; i < crownHolderBlue.transform.childCount; i++)
        {
            var child = crownHolderBlue.transform.GetChild(i).gameObject;
            if (!child.activeInHierarchy)
            {
                child.SetActive(true);
                break;
            }
        }
    }

    //If player two won a round, add points or declare winner.
    //Otherwise reset game for next round.
    public void PlayerTwoWin ()
    {
        playerTwoScore++;

        if (playerTwoScore == winScore)
        {
            winScreen.transform.GetChild(1).gameObject.SetActive(true);
            winScreen.SetActive(true);
            winMenuActive = true;
            EventManager.TriggerEvent("Win");
            flySource.enabled = false;
            return;
        }
        else
        {
            EventManager.TriggerEvent("RoundComplete");
            Debug.Log("RoundComplete!");
            StartCoroutine(AddCrownRed());
        }
    }

    public IEnumerator AddCrownRed()
    {
        yield return new WaitForSeconds(crownSpawnTime);
        for (int i = 0; i < crownHolderRed.transform.childCount; i++)
        {
            var child = crownHolderRed.transform.GetChild(i).gameObject;
            if (!child.activeInHierarchy)
            {
                child.SetActive(true);
                break;
            }
        }
    }
}
