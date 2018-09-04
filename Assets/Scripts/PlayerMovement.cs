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

    //All the events this script listens to.
    private void OnEnable()
    {
        EventManager.StartListening("IncreaseJumpForce", ForceIncrease);
        EventManager.StartListening("ResetLevel", Resetting);
    }

    private void OnDisable()
    {
        EventManager.StopListening("IncreaseJumpForce", ForceIncrease);
        EventManager.StopListening("IncreaseJumpForce", ForceIncrease);
    }

    //Get animator and rigidbody.
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        frogAnim = GetComponent<Animator>();

        //Save startpos, so the game can be reset without restarting the scene.
        startPos = transform.position;
        //Center of mass, to change the feel of the frogs.
        rb2d.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update () {
        //Only jump if the frog is touching the ground.
        if (isGrounded)
        {
            if (Input.GetKeyDown(jump))
            {
                frogAnim.SetTrigger(jumpAnimName);
                rb2d.velocity = Vector2.up * jumpForce;
                Debug.Log("is jumping");
            }
        }
        //Don't start a lick, if a lick is already in progress.
        if (!isLicking)
        {
            if (Input.GetKeyDown(tongue))
            {
                isLicking = true;
                StartCoroutine(LickPause());
            }
        }
	}

    //Make sure the animation is finished before the next lick starts.
    public IEnumerator LickPause ()
    {
        frogAnim.SetTrigger(tongueAnimName);
        yield return new WaitForSeconds(lickWait);
        isLicking = false;
    }

    //Check when the frog touches the ground.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }
    //Check when the frog leaves the ground.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }

    //Increase the jump-force when the size and mass of the frog has increased.
    public void ForceIncrease ()
    {
        jumpForce += forceIncrease;
    }

    //Use this to reset the frog's position when resetting the game. 
    public void Resetting()
    {
        transform.position = startPos;
    }
}
