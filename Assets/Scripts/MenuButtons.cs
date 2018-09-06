using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public GameObject creditsScreen;

    public KeyCode startGame, endGame, credits;

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(startGame))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
        }

        if (Input.GetKeyDown(endGame))
        {
            Application.Quit();
        }

        //Showing the credits when leftshift is held down
        if(Input.GetKeyDown(credits))
        {
            creditsScreen.SetActive(true);
        }
        if(Input.GetKeyUp(credits))
        {
            creditsScreen.SetActive(false);
        }
	}
}
