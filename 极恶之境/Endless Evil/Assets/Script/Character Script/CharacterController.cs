using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // get the animator and rigid body 2d
    private Animator animator;
    private Rigidbody2D body2d;



    // charactor data
    private bool running = false;
    public bool _locked;
    // charactor spped 
    public float CharactorSpeed = 5;

    public Vector2 movement;
    private float timeSinceAttack = 0.0f;
    private float delayToIdle = 0.0f;
    public int facingDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        animator.SetBool("Ready", true);
    }

    // Update is called once per frame
    void Update()
    {
        // if dead, close controller
        _locked =  animator.GetInteger("Dead") > 0;
        if (_locked) return;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        // if dead, close controller
        _locked = animator.GetInteger("Dead") > 0;
        if (_locked) return;

        if (movement.x != 0 || movement.y != 0)
        { 
            body2d.MovePosition(body2d.position + movement * CharactorSpeed * Time.fixedDeltaTime);
            animator.SetBool("Run", true);
            running = true;
        }
        var scale = transform.localScale;


        if(movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Ready", true);
            running = false;
        }
        if (movement.x > 0 & facingDirection == -1)
        {
            facingDirection = 1;
            scale.x *= -1;
            transform.localScale = scale;
        }

        else if (movement.x < 0 && facingDirection == 1)
        {
            facingDirection = -1;
            scale.x *= -1;
            transform.localScale = scale;
        }

    }
}
