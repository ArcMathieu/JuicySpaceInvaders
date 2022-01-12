using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float speed = 0;
    Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed * -1);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerController>()) {
            other.gameObject.GetComponent<PlayerController>().Hit();
            Destroy(gameObject);

        }
    }
}

