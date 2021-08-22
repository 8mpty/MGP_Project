using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D rb;
    private float jumpForce = 7f;
    private bool faceRight = true;
    private Animator playerAnim;

    private bool isGround;
    private Transform groundObj;
    private LayerMask groundLayer;

    private int jumpCount = 2;

    private Transform bulletSpawnPoint;
    public GameObject bulletObj;

    private int playerHealth = 100;
    private int currentHealth;
    public GameObject floatPoint;
    private int heartAmount = 5;

    [SerializeField] private Material flashMat;
    [SerializeField] private float flashDuration = 0.15f;

    private SpriteRenderer spriteChange;
    private Material oriMat;
    private Coroutine flashSprite;

    public HealthBar bar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundObj = transform.GetChild(0).transform;
        groundLayer = LayerMask.GetMask("Platform");
        bulletSpawnPoint = transform.GetChild(1).transform;

        playerAnim = GetComponent<Animator>();

        playerAnim.SetBool("isIdle", true);
        GameManager.instance.HealthUpdater(playerHealth);

        spriteChange = GetComponent<SpriteRenderer>();
        oriMat = spriteChange.material;

        currentHealth = playerHealth;
        bar.SetHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameWin)
        {
            return;
        }

        PlayerMovement();
        PlayerBoundary();

        if (isGround == true)
        {
            jumpCount = 2;
        }
        
        //Debugging
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerHealth -= 1;
            GameManager.instance.HealthUpdater(playerHealth);

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.instance.GameOverStatus(false);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.instance.GameOverStatus(true);
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


        if (xInput > 0f && !faceRight)
        {
            Flip();
            playerAnim.SetBool("isIdle", false);
        }
        else if (xInput < 0f && faceRight)
        {
            Flip();
            playerAnim.SetBool("isIdle", false);
        }
        else if (xInput == 0f)
        {
            playerAnim.SetBool("isIdle", true);
        }
    }

    private void PlayerDead()
    {
        //Destroy(gameObject);
        GameManager.instance.GameOverStatus(false);
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void PlayerJump()
    {
        if(jumpCount <= 2 && jumpCount != 1)
        {
            rb.velocity = Vector2.up * jumpForce;

            playerAnim.SetTrigger("triggJump");
            jumpCount--;
            AudioManager.instance.Play("PlayerJump");
        }
    }

    public void PlayerShoot()
    {
        playerAnim.SetTrigger("triggShoot");
        Instantiate(bulletObj, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        AudioManager.instance.Play("PlayerShoot");
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

    public void Flash()
    {
        if (flashSprite != null)
        {
            StopCoroutine(flashSprite);
        }

        flashSprite = StartCoroutine(FlashSprite());
    }

    private IEnumerator FlashSprite()
    {
        spriteChange.material = flashMat;

        yield return new WaitForSeconds(flashDuration);

        spriteChange.material = oriMat;

        flashSprite = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyOrb"))
        {
            AudioManager.instance.Play("PlayerDamaged");
            Flash();
            playerAnim.SetTrigger("triggHurt");
            EnemyBullet bullet = collision.gameObject.GetComponent<EnemyBullet>();
            currentHealth -= bullet.enemyDamage;
            GameManager.instance.HealthUpdater(currentHealth);
            bar.SetHealth(currentHealth);

            GameObject decHealth = Instantiate(floatPoint, new Vector3(transform.position.x + 0.338f, transform.position.y + 0.189f, 0f), Quaternion.identity) as GameObject;
            decHealth.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.red;
            decHealth.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + bullet.enemyDamage;

            if (currentHealth <= 0)
            {
                GameManager.instance.GameOverStatus(false);
            }
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Heart"))
        {
            AudioManager.instance.Play("HealthPickUp");
            currentHealth += heartAmount;
            GameManager.instance.HealthUpdater(currentHealth);
            bar.SetHealth(currentHealth);


            GameObject incHealth = Instantiate(floatPoint, new Vector3(transform.position.x + 0.338f, transform.position.y + 0.189f, 0f), Quaternion.identity) as GameObject;
            incHealth.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.green;
            incHealth.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + heartAmount;

            if(currentHealth >= 100)
            {
                currentHealth = 100;
            }

            Destroy(collision.gameObject);
        }
    }
}