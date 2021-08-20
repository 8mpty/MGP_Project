using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffest = 1f;
    [SerializeField] private Vector2 pos = new Vector2(0.5f,0.5f);


    private float leftLimit = 0f;
    private float rightLimit = 0f;
    [SerializeField] private float topLimit = 50f;
    [SerializeField] private float bottomLimit = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = transform.position;

        Vector3 targetPosition = player.transform.position;

        targetPosition.x += pos.x;
        targetPosition.y += pos.y;
        targetPosition.z = -10;

        transform.position = Vector3.Lerp(startPosition, targetPosition, timeOffest * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), 
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit), 
            transform.position.z);

    }
}
