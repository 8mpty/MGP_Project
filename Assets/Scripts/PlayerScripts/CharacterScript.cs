using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Transform feet;
    public float feetRadius;
    public LayerMask layerMask;

    private Rigidbody2D rb;
    private bool faceRight;
    private bool onGround;
    private Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        faceRight = true;
        onGround = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Keyboard Input
        //float inputH = Input.GetAxisRaw("Horizontal");

        float inputH = JoyStickManager.instance.GetDirection().x;
        float inputV = JoyStickManager.instance.GetDirection().y;
        // Animations
        animator.SetFloat("hVelocity", Mathf.Abs(inputH));

        rb.velocity = new Vector2(inputH * speed, rb.velocity.y);

        if(inputH > 0 && faceRight == false)
        {
            Flip();
            faceRight = true;
        }
        else if (inputH < 0 && faceRight == true)
        {
            Flip();
            faceRight = false;
        }

        // Check if OnGround
        if (Physics2D.OverlapCircle(feet.position, feetRadius, layerMask) && onGround == false)
        {
            onGround = true;
        }

        // Jumping Keyboard
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += Vector2.up * jumpForce;
            onGround = false;
        }

        // Jumping JoyStick
        if(inputV > 0.75)
        {
            PlayerJump();
        }
    }

    private void Flip()
    {
        float scalex = transform.localScale.x;
        scalex *= -1;
        transform.localScale = new Vector3(scalex, transform.localScale.y, transform.localScale.z);
    }

    public void PlayerJump()
    {
        if(onGround == true)
        {
            //rb.velocity += Vector2.up * jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onGround = false;
        }
    }
}
