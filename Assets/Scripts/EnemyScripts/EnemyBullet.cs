using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float die, damage;
    public Rigidbody2D rb;

    private float bulletSpeed = 5f;
    private float spriteWithDelta;
    private Transform player;
    private Transform self;
    private Vector3 playerPos;
    private Vector3 dirc;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());

        spriteWithDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerPos = player.position;
        self = GetComponent<Transform>();
        dirc = (playerPos - self.position).normalized;

    }

    private void Update()
    {
        self.position += dirc * bulletSpeed * Time.deltaTime;
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(die);
        Destroy(gameObject);
    }
}