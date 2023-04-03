using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public int airSpinsDone = 0;
    float punching;

    bool grounded;

    float speed = 0.5f;
    float maxSpeed = 8.0f;
    float jumpForce = 8.0f;
    float airControlForce = 10.0f;
    float airControlMax = 8f;

    Camera cam;
    Rigidbody2D rigidBody;
    Animator animator;
    BoxCollider2D hitBox;
    Vector2 boxExtents;

    public Vector2 inputVector;

    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitBox = GetComponent<BoxCollider2D>();
        boxExtents = hitBox.bounds.extents;
    }

    // Update is called once per frame
    private void Update()
    {
        if (view.IsMine)
        {
            inputVector = (Vector2)(Input.mousePosition - cam.WorldToScreenPoint(transform.position));
            //flip for direction
            if (inputVector.x * transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            //running animation
            float xSpeed = Mathf.Abs(rigidBody.velocity.x);
            animator.SetFloat("xSpeed", xSpeed);
            animator.SetFloat("ySpeed", rigidBody.velocity.y);

            //punch
            if (grounded)
            {
                //reset counter
                airSpinsDone = 0;
                //punch
                if (Input.GetMouseButtonDown(0))
                    punching = .5f;
                animator.SetBool("punching", punching > 0);
                if (punching > 0)
                    punching -= Time.deltaTime;
                else
                    punching = 0;
            }
            //air kick
            else
                animator.SetBool("punching", Input.GetMouseButtonDown(0) && airSpinsDone < 2);
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            inputVector = (Vector2)(Input.mousePosition - cam.WorldToScreenPoint(transform.position));

            Vector2 bottom = new Vector2(transform.position.x, transform.position.y - boxExtents.y);

            Vector2 hitBoxSize = new Vector2(boxExtents.x * 2.0f, 0.05f);

            RaycastHit2D result = Physics2D.BoxCast(bottom, hitBoxSize, 0.0f, new Vector3(0.0f, -1.0f), 0.0f, 1 << LayerMask.NameToLayer("Ground"));

            grounded = result.collider != null && result.normal.y > 0.9f;

            animator.SetBool("grounded", grounded);

            if (grounded)
            {
                if (animator.GetBool("punching")) { }
                else if (inputVector.y > Math.Abs(inputVector.x) && inputVector.magnitude > 80) //jump
                    rigidBody.AddForce(new Vector2(0.0f, jumpForce * 1.25f), ForceMode2D.Impulse);
                else
                {
                    float actualSpeed = speed * inputVector.x;
                    if (actualSpeed < 0)
                        actualSpeed = Mathf.Max(-maxSpeed, actualSpeed);
                    else
                        actualSpeed = Mathf.Min(maxSpeed, actualSpeed);
                    rigidBody.velocity = new Vector2(actualSpeed, rigidBody.velocity.y);
                }

            }
            else
            {


                // allow a small amount of movement in the air
                float force = inputVector.x;
                if (Mathf.Abs(force) < airControlMax)
                    rigidBody.AddForce(new Vector2(force * airControlForce, 0));
                else
                    rigidBody.AddForce(new Vector2(airControlMax * Math.Sign(force), 0));

                // allow vertical control
                if (inputVector.y < -80)
                    rigidBody.gravityScale = 3;
                else
                    rigidBody.gravityScale = 1.5f;

            }
        }

    }
}
