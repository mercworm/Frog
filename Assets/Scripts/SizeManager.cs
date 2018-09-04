using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour {

    //The different sizes the frogs can be.
    public Sprite[] playerSizes;
    //The amount of flies they have eaten.
    public int flyCount = 0;
    public int currentSize = 0;

    Rigidbody2D rb2d;
    SpriteRenderer frogBody;

    public float massIncrease;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        frogBody = GetComponentInParent<SpriteRenderer>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            Debug.Log("Touched fly");
            //ChangeSize();
            Destroy(collision.gameObject); // this could be changed to do something else
        }
    }

    public void ChangeSize ()
    {
        //change the size of the frog
        //this should be configured depending on the flycount, so we can change how many flies a frog needs to eat to grow.
        //this will hold for now though.
        currentSize++;
        frogBody.sprite = playerSizes[currentSize];

        flyCount++;

        rb2d.mass += massIncrease;
        EventManager.TriggerEvent("IncreaseJumpForce");

        // if flycount is goal send win trigger
    }
}
