using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 3f;
    [SerializeField] Rigidbody2D rb;
    private float jumpForce = 6f;
    private bool faceRight = true;
    private Animator playerAnim;

    private bool isGround;
    public Transform groundObj;
    public LayerMask groundLayer;

    private int jumpCount = 2;

    public Transform bulletSpawnPoint;
    public GameObject bulletObj;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        
        playerAnim = GetComponent<Animator>();

        playerAnim.SetBool("isIdle", true);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerBoundary();

        if (isGround == true)
        {
            jumpCount = 2;
        }
    }

    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundObj.position, 0.2f, groundLayer);
    }

    private void PlayerMovement()
    {
        float xInput = JoyStickManager.instance.GetDirection().x;
        //float vInput = JoyStickManager.instance.GetDirection().y;

        //transform.position += new Vector2(xInput, 0f, vInput) * speed * Time.deltaTime;
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        if (xInput > 0 && !faceRight)
        {
            Flip();
            //faceRight = true;
            playerAnim.SetBool("isIdle", false);
        }
        else if (xInput < 0 && faceRight)
        {
            Flip();
            //faceRight = false;
            playerAnim.SetBool("isIdle", false);
        }
        else if (xInput == 0)
        {
            playerAnim.SetBool("isIdle", true);
        }
    }

    private void Flip()
    {
        /*float scalex = transform.localScale.x;
        scalex *= -1;
        transform.localScale = new Vector3(scalex, transform.localScale.y, transform.localScale.z);*/

        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void PlayerJump()
    {
        if(jumpCount <= 2 && jumpCount != 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            playerAnim.SetTrigger("triggJump");
            jumpCount--;
        }
        
    }

    public void PlayerShoot()
    {
        playerAnim.SetTrigger("triggShoot");

        Instantiate(bulletObj, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    public void PlayerCrouch()
    {
        playerAnim.SetBool("boolCrouch", true);

    }

    public void PlayerUnCrouch()
    {
        playerAnim.SetBool("boolCrouch", false);
    }

    private void PlayerBoundary()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f), transform.position.y, transform.position.x);
    }
}