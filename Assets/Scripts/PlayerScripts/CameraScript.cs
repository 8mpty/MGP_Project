using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffest;
    [SerializeField] private Vector2 pos;


    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;

    // Start is called before the first frame update
    void Start()
    {
        
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
