using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public KeyCode jump;
    public KeyCode tongue;

    public float jumpForce;
    public float lickWait;

    public float forceIncrease;

    Rigidbody2D rb2d;

    public bool isLicking;
    public bool isGrounded;

    private void OnEnable()
    {
        EventManager.StartListening("IncreaseJumpForce", ForceIncrease);
    }

    private void OnDisable()
    {
        EventManager.StopListening("IncreaseJumpForce", ForceIncrease);
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        if (!isGrounded)
        {
            if (Input.GetKeyDown(jump))
            {
                rb2d.AddForce(transform.up * jumpForce * Time.deltaTime);
                //trigger jump animation?
            }
        }

        if (!isLicking)
        {
            if (Input.GetKeyDown(tongue))
            {
                isLicking = true;
                StartCoroutine(LickPause());
            }
        }
	}

    public IEnumerator LickPause ()
    {
        // trigger the tongue animation, so it shoots out.
        yield return new WaitForSeconds(lickWait);
        isLicking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }

    public void ForceIncrease ()
    {
        jumpForce += forceIncrease;
    }
}
