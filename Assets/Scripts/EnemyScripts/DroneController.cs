using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DroneController : MonoBehaviour
{

    [SerializeField]private int enemyHealth = 40;
    public float shootSpeed = 2f;
    private float speed = 200f;
    private float distance;

    public Rigidbody2D rb;
    public LayerMask platformLayer;
    private Transform player;
    public float rangeToPlayer, btwShots;

    public Transform enemybulletSpawnPoint;
    public GameObject enemybulletObj;


    private bool patrolling;
    private bool turn;

    public Transform checkGround;

    private bool canShoot;

    public Transform self;

    public GameObject floatDamagePoint;
    public GameObject healthPrefab;

    [SerializeField] private Material flashMat;
    [SerializeField] private float flashDuration = 0.1f;

    private SpriteRenderer spriteChange;
    private Material oriMat;
    private Coroutine flashSprite;

    private int scoreInc = 0;


    [SerializeField] private bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        patrolling = true;
        speed = Random.Range(100f, 150f);

        player = GameObject.FindGameObjectWithTag("Player").transform;

        canShoot = true;

        spriteChange = GetComponent<SpriteRenderer>();
        oriMat = spriteChange.material;
        scoreInc = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
    }

    private void FixedUpdate()
    {
        if(patrolling)
        {
            turn = !Physics2D.OverlapCircle(checkGround.position, 0.1f, platformLayer);
        }
    }

    private void LookForPlayer()
    {
        if (patrolling)
        {
            Patrol();
        }

        distance = Vector2.Distance(self.transform.position, player.position);

        if (distance <= rangeToPlayer)
        {
            if (player.position.x > self.transform.position.x && self.transform.localScale.x < 0 || player.position.x < self.transform.position.x && self.transform.localScale.x > 0)
            {
                Flip();
            }

            patrolling = false;
            rb.velocity = Vector2.zero;

            if (canShoot)
            {
                StartCoroutine(Shooting());
            }
        }
        else
        {
            patrolling = true;
        }
    }
    
    private void Flip()
    {
        patrolling = false;
        transform.localScale = new Vector2(self.transform.localScale.x * -1, self.transform.localScale.y);
        speed *= -1;
        patrolling = true;
    }

    private void Patrol()
    {
        if(turn)
        {
            Flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    private IEnumerator Shooting()
    {
        canShoot = false;
        yield return new WaitForSeconds(btwShots);
        Instantiate(enemybulletObj, enemybulletSpawnPoint.position, Quaternion.identity);
        canShoot = true;
    }

    public void EnemyHealth(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            EnemyDead();
        }
    }
    public void Flash()
    {
        if(flashSprite != null)
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

    private void EnemyDead()
    {
        GameObject addScore = Instantiate(floatDamagePoint, new Vector3(transform.position.x + 0.2f, transform.position.y + 1f, 0f), Quaternion.identity) as GameObject;
        addScore.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.cyan;
        scoreInc = 10;
        addScore.transform.GetChild(0).GetComponent<TextMeshPro>().text = "+" + scoreInc + " Points";
        Destroy(gameObject);
        int spawnHealth = Random.Range(1, 3);

        if (spawnHealth == 1)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        if (isBoss == true)
        {
            GameManager.instance.GameOverStatus(true);
        }
        GameManager.instance.ScoreUpdater(scoreInc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            BulletScript playerBullet = collision.gameObject.GetComponent<BulletScript>();
            enemyHealth -= playerBullet.playerBulletDamage;
            AudioManager.instance.Play("EnemyDamaged");
            Flash();
            GameObject damage = Instantiate(floatDamagePoint, new Vector3(transform.position.x + 0.338f, transform.position.y + 0.189f, 0f), Quaternion.identity) as GameObject;
            damage.transform.GetChild(0).GetComponent<TextMeshPro>().color = Color.white;
            damage.transform.GetChild(0).GetComponent<TextMeshPro>().text = "-" + playerBullet.playerBulletDamage;

            if (enemyHealth <= 0)
            {
                EnemyDead();
            }
        }
    }
}
