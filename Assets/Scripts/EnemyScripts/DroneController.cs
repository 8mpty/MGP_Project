using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform edgeCheck;


    private bool faceRight = true;
    public float distance;

    private Rigidbody2D rb;
    public LayerMask platformLayer;
    public Transform player;
    public float rangeToPlayer;

    RaycastHit2D edgeInfo;

    public Transform enemybulletSpawnPoint;
    public GameObject enemybulletObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        edgeInfo = Physics2D.Raycast(edgeCheck.position, -transform.up, 1f, platformLayer);

        float discToPlayer = Vector2.Distance(transform.position, player.position);
        if (discToPlayer < rangeToPlayer)
        {
            FoundPlayer();
            Debug.Log("Found Player");
        }
        else
        {
            Patrol();
            Debug.Log("Patrolling");
        }

    }

    private void FixedUpdate()
    {
        
    }

    private void FoundPlayer()
    {
        Vector2 vector2 = new Vector2(0f, 0f);
        rb.velocity = vector2;
        float angle = 0;

        Vector3 relative = transform.InverseTransformPoint(player.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
        //Instantiate(enemybulletObj, enemybulletSpawnPoint.position, enemybulletSpawnPoint.rotation);
    }

    private void Patrol()
    {
        if (edgeInfo.collider != false)
        {
            if (faceRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
        else
        {
            faceRight = !faceRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
