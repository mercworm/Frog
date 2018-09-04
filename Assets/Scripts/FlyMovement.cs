using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {

    public float moveSpeed = 5;

    private Rigidbody2D rb2d;

    public bool left = false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (left) rb2d.velocity = Vector3.left * moveSpeed;
        else rb2d.velocity = Vector3.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("FlyDie"))
        {
            Destroy(gameObject);
        }
    }
}
