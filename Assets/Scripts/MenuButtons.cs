using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public GameObject creditsScreen;

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Frog");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Showing the credits when leftshift is held down
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            creditsScreen.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            creditsScreen.SetActive(false);
        }
	}
}
