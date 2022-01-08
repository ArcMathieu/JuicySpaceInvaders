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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Enemy>()) {
            other.gameObject.GetComponent<Enemy>().Hit();
        }
        if (!other.gameObject.GetComponent<PlayerController>()) {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
