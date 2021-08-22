using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private float bulletSpeed = 10f;
    public int playerBulletDamage = 10;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 1f);

    }
}
