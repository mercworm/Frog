using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour {

    //The different sizes the frogs can be.
    public Sprite[] playerSizes;
    //The amount of flies they have eaten.
    public int flyCount = 0;
    public int currentSize = 0;

    public AudioClip[] growthSounds;
    public AudioSource growthSource;

    public GameObject crownHolder;
    private Vector2 crownHolderStart;

    Rigidbody2D rb2d;
    public SpriteRenderer frogBody;

    public float massIncrease;

    public GameObject splatFly;

    private void OnEnable()
    {
        EventManager.StartListening("RoundComplete", Resetting);
    }

    private void OnDisable()
    {
        EventManager.StopListening("RoundComplete", Resetting);
    }

    // Use this for initialization
    void Start () {
        rb2d = GetComponentInParent<Rigidbody2D>();

        crownHolderStart = crownHolder.transform.localPosition;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            splatFly.SetActive(true);
            Debug.Log("Touched fly");
            flyCount++;

            if (flyCount >= 5)
            {
                EventManager.TriggerEvent(gameObject.tag + "Win");
                Debug.Log("Triggered Win");
                return;
            }
            else ChangeSize();

            Destroy(collision.gameObject); // this could be changed to do something else
        }
    }

    public void ChangeSize ()
    {
        //change the size of the frog
        //this should be configured depending on the flycount, so we can change how many flies a frog needs to eat to grow.
        //this will hold for now though.
        currentSize++;

        crownHolder.transform.position = new Vector2(crownHolder.transform.position.x, crownHolder.transform.position.y + 0.2f);

        if (playerSizes[currentSize] != null)
        {
            growthSource.clip = growthSounds[currentSize];
            growthSource.Play();

            frogBody.sprite = playerSizes[currentSize];
            rb2d.mass += massIncrease;
            EventManager.TriggerEvent("IncreaseJumpForce");
        }
    }

    public void Resetting ()
    {
        flyCount = 0;
        currentSize = 0;
        rb2d.mass = 1;
        frogBody.sprite = playerSizes[0];
        crownHolder.transform.localPosition = crownHolderStart;
    }
}
