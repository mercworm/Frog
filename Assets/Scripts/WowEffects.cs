using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowEffects : MonoBehaviour {

    public GameObject wowSprite;
    public bool isSprite = false;

    public float waitTime;

    private void OnEnable()
    {
        if (isSprite) return;
        EventManager.StartListening("RoundComplete", RoundComplete);
        EventManager.StartListening("Win", Win);
    }

    private void OnDisable()
    {
        EventManager.StopListening("RoundComplete", RoundComplete);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(BeforeMovementActive());
	}

    public void TurnOffObject ()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator BeforeMovementActive()
    {
        yield return new WaitForSeconds(waitTime);
        EventManager.TriggerEvent("PlayersMovementToggleOn");
    }

    public void Win ()
    {
        Debug.Log("Someone won!!");
        //whatever we might want to show with the winscreen, if we're not already doing that
        EventManager.TriggerEvent("PlayersMovementToggleOff");
    }

    public void RoundComplete()
    {
        StartCoroutine(RoundCompleteActions());
    }

    public IEnumerator RoundCompleteActions ()
    {
        //here goes all the things that we want to show between rounds.
        wowSprite.SetActive(true);
        yield return new WaitForEndOfFrame();
    }
}
