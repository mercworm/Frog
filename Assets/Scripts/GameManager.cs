using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float playerOneScore;
    public float playerTwoScore;

    public GameObject pauseMenu;
    public GameObject winScreen;

    public bool winMenuActive;
    public bool pauseMenuActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuActive) UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            else
            {
                pauseMenuActive = true;
                pauseMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M))
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

    public void PlayerOneWin ()
    {
        playerOneScore++;
        if (playerOneScore == 3)
        {
            //Change which image to be activated depending on who won.
            winScreen.SetActive(true);
            winMenuActive = true;
        }
        else
        {
            EventManager.TriggerEvent("RoundComplete");
        }
    }

    public void PlayerTwoWin ()
    {
        playerTwoScore++;
        if (playerTwoScore == 3)
        {
            //Change which image to be activated depending on who won.
            winScreen.SetActive(true);
            winMenuActive = true;
        }
        else
        {
            EventManager.TriggerEvent("RoundComplete");
        }
    }

}
