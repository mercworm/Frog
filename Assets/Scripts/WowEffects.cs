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
        EventManager.StartListening("RoundComplete", TriggerWow);
        EventManager.StartListening("Win", Win);
    }

    private void OnDisable()
    {
        EventManager.StopListening("RoundComplete", TriggerWow);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(BeforeMovementActive());
	}

    public void TriggerWow()
    {
        wowSprite.SetActive(true);
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
        //whatever we might want to show with the winscreen, if we're not already doing that
        EventManager.TriggerEvent("PlayersMovementToggleOff");
    }
}
