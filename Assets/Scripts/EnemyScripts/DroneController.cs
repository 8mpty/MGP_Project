using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float shootSpeed = 10f;
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

    private Vector3 direction;

    public Transform self;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        patrolling = true;
        speed = Random.Range(100f,150f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canShoot = true;

        direction = (player.position - self.transform.position).normalized;
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
}
