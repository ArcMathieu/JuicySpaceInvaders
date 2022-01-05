using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float direction = 0;
    public float characterSpeed = 0;

    bool shoot = false;

    Rigidbody rb;

    public GameObject bullet;
    public float bulletSpawnDist;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Input
        direction = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown("space")) {
            shoot = true;
        }
        #endregion
        Movement();
        Shoot();
    }

    void Movement() {
        rb.velocity = new Vector3(characterSpeed * direction, 0, 0);
    }

    void Shoot() {
        if (shoot) {
            GameObject newBullet =  Instantiate(bullet);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + bulletSpawnDist);
            shoot = false;
        }
    }
}
