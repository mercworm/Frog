using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStraightening : MonoBehaviour {

    public float selfRigtingTol = 0.5f;
    public float torque = 1;

    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (Vector2.Dot(transform.up, Vector2.up) < selfRigtingTol)
        {
            rb2d.AddTorque(torque * transform.up.x);
        }
	}
}
