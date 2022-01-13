using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Enemy>()) {
            other.gameObject.GetComponent<Enemy>().Hit();
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
