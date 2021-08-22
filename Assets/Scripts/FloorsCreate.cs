using UnityEngine;
using System.Collections;

public class FloorsCreate : MonoBehaviour 
{

    [Header("Coordinates and Scale of Floors")]
    [SerializeField] private float minX = -1.0f;
    [SerializeField] private float maxX = 1.0f;
    [SerializeField] float offset = 1.0f;

    public GameObject floorPrefab;
    public GameObject enemyPrefab;
    private Transform player;
    private Transform self;

    private float disc;
    public float ranged;
    private int enemySpawn;
    private float maxLimit = 80f;
    // Use this for initialization
    void Start () 
    {
        Instantiate(floorPrefab, transform.position, Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disc = Vector2.Distance(player.position, transform.position);
        self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Spawning();
        disc = Vector2.Distance(player.position, transform.position);
    }

    private void Spawning()
    {
        if (disc <= ranged)
        {
            if (transform.position.y != maxLimit)
            {
                Vector3 newPos = transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y + offset);
                enemySpawn = Random.Range(1, 3);

                if (newPos.x > 0f)
                {
                    Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);
                    Instantiate(floorPrefab, transform.position, rotation);
                    if (enemySpawn == 1)
                    {
                        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(floorPrefab, transform.position, Quaternion.identity);
                    if (enemySpawn == 2)
                    {
                        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                    }
                }
            }
        }

        //Debugging Only//
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (transform.position.y != maxLimit)
            {
                Vector3 newPos = transform.position = new Vector3(Random.Range(minX, maxX), transform.position.y + offset);
                enemySpawn = Random.Range(1, 3);

                if (newPos.x > 0f)
                {
                    Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);
                    Instantiate(floorPrefab, transform.position, rotation);
                    if (enemySpawn == 1)
                    {
                        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(floorPrefab, transform.position, Quaternion.identity);
                    if (enemySpawn == 2)
                    {
                        Instantiate(enemyPrefab, new Vector3(transform.position.x, transform.position.y + 1f), Quaternion.identity);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Debug.DrawLine(transform.position, player.transform.position, Color.red, 0f);
    }
}
