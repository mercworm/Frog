using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    //Make sure there is an EventManager in the scene.
    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager on a GameObject in your scene.");
                }
                else
                {
                    //If there is an EventManager, initialize it.
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    //Make sure there is a dictionary.
    //Otherwise create one.
    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    //Add a gameObject that will listen to an event.
    //Find the event by name.
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    //Remove a gameObject so it does no longer listen to an event.
    public static void StopListening(string eventName, UnityAction listener)
    {
        //Find if it exits.
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove the listened.
            thisEvent.RemoveListener(listener);
        }
    }

    //Start the event!
    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
