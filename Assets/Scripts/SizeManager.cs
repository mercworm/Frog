using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeManager : MonoBehaviour {

    //The different sizes the frogs can be.
    public Sprite[] playerSizes;
    //The amount of flies they have eaten.
    public int flyCount;

    Rigidbody2D rb2d;

    public float massIncrease;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fly"))
        {
            ChangeSize();
            Destroy(collision.gameObject); // this could be changed to do something else
        }
    }

    public void ChangeSize ()
    {
        flyCount++;
        rb2d.mass += massIncrease;
        EventManager.TriggerEvent("IncreaseJumpForce");
    }
}
