using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour {

    public float minTime;
    public float maxTime;

    private float spawnPointX, spawnPointY;

    public GameObject[] leftFlies;
    public GameObject[] rightFlies;

    public bool spawning = true;

    private void OnEnable()
    {
        EventManager.StartListening("Win", StopSpawning);
    }

    void Start ()
    {
        //Start with spawning a fly, so the loop can begin.
        Invoke("SpawnFlyRight", Random.Range(minTime, maxTime));
        Invoke("SpawnFlyLeft", Random.Range(minTime, maxTime));
    }

    public void SpawnFlyRight ()
    {
        if (spawning)
        {
            spawnPointX = Random.Range(9, 11);
            spawnPointY = Random.Range(4, -2);
            Vector3 spawn = new Vector3(spawnPointX, spawnPointY, 0);

            Instantiate(rightFlies[Random.Range(0,2)], spawn, Quaternion.identity);

            Invoke("SpawnFlyRight", Random.Range(minTime, maxTime));
        }
    }

    public void SpawnFlyLeft ()
    {
        if (spawning)
        {
            spawnPointX = Random.Range(-9f, -11f);
            spawnPointY = Random.Range(4f, -2f);
            Vector3 spawn = new Vector3(spawnPointX, spawnPointY, 0);

            Instantiate(leftFlies[Random.Range(0,2)], spawn, Quaternion.identity);

            Invoke("SpawnFlyLeft", Random.Range(minTime, maxTime));
        }
    }

    public void StopSpawning ()
    {
        spawning = false;
    }
}
