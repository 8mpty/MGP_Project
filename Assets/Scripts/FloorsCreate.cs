using UnityEngine;
using System.Collections;

public class FloorsCreate : MonoBehaviour 
{

    public GameObject floorPrefab;
    public bool integerScale = true;

    [Header("Coordinates and Scale of Floors")]
    public float minCreateCoorX = -1.0f;
    public float maxCreateCoorX = 1.0f;
    public float offset = 1.0f;

    private Transform player;

    private float disc;
    public float ranged;

	// Use this for initialization
	void Start () 
    {
        Instantiate(floorPrefab, transform.position, Quaternion.identity);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        disc = Vector2.Distance(player.position, transform.position);
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
            transform.position = new Vector3(Random.Range(-3.3f, 2.5f), transform.position.y + 2.5f);
            Instantiate(floorPrefab, transform.position, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = new Vector3(Random.Range(-3.3f, 2.5f), transform.position.y + 2.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawLine(transform.position, player.transform.position, Color.red,0f);
    }
}
