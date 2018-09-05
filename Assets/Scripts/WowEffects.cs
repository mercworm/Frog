using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WowEffects : MonoBehaviour {

    public GameObject wowSprite;
    public bool isSprite = false;

    public float waitTime;

    private void OnEnable()
    {
        EventManager.StartListening("RoundComplete", TriggerWow);
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
        if (isSprite) return;
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
}
