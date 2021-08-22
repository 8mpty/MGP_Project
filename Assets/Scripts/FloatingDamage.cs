using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    public Transform enemy;
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
