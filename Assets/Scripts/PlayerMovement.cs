using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public KeyCode jump;
    public KeyCode tongue;

    public float jumpForce;
    public float lickWait;

    public float forceIncrease;

    private Rigidbody2D rb2d;
    private Animator frogAnim;

    public bool isLicking = false;
    public bool isGrounded = true;

    public Vector2 startPos;
    public Vector2 centerOfMass;

    public string jumpAnimName;
    public string tongueAnimName;

    private void OnEnable()
    {
        EventManager.StartListening("IncreaseJumpForce", ForceIncrease);
        EventManager.StartListening("ResetLevel", Resetting);
    }

    private void OnDisable()
    {
        EventManager.StopListening("IncreaseJumpForce", ForceIncrease);
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        frogAnim = GetComponent<Animator>();

        startPos = transform.position;
        rb2d.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update () {
        if (isGrounded)
        {
            if (Input.GetKeyDown(jump))
            {
                rb2d.velocity = Vector2.up * jumpForce;
                Debug.Log("is jumping");
                frogAnim.SetTrigger(jumpAnimName);
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
        frogAnim.SetTrigger(tongueAnimName);
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

    public void Resetting()
    {
        transform.position = startPos;
    }
}
