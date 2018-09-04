using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour {

    public float minTime;
    public float maxTime;

    public Vector2 areaPoint1, areaPoint2;

    public GameObject flyPrefab;

	void Start ()
    {
        //Start with spawning a fly, so the loop can begin.
        Invoke("SpawnFly", Random.Range(minTime, maxTime));
	}

    public void SpawnFly ()
    {
        //spawn the fly here, at random location.
        //Instantiate(flyPrefab, Vector2(Random.Range(min,max), Random.Range(min,max));
        Invoke("SpawnFly", Random.Range(minTime, maxTime));
    }
}
