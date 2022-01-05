using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private int speed = 0;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed * Gino.instance.entitiesManager.enemyDirection);
        rb.velocity = new Vector3(speed * Gino.instance.entitiesManager.enemyDirection, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Gino.instance.entitiesManager.enemyDirectionNumber != 0 || (Gino.instance.gameManager.leftBorder.transform.position.x >= transform.position.x + transform.localScale.x / 2) && Gino.instance.entitiesManager.enemyDirection == -1|| (Gino.instance.gameManager.rightBorder.transform.position.x <= transform.position.x + transform.localScale.x / 2) && Gino.instance.entitiesManager.enemyDirection == 1) {
            Gino.instance.entitiesManager.NewDirection();
            NextLine();
        }
        if (transform.position.z < Gino.instance.entitiesManager.player.transform.position.z + Gino.instance.entitiesManager.player.transform.localScale.z / 2) {
            Gino.instance.uiManager.GameOver();
        }
    }

    void NextLine() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Gino.instance.entitiesManager.distBetweenLines);
        Gino.instance.entitiesManager.enemyDirectionNumber--;
        rb.velocity = new Vector3(speed * Gino.instance.entitiesManager.enemyDirection, 0, 0);
    }

    public void Die() {
        Destroy(gameObject);
        Gino.instance.entitiesManager.enemyNumber--;
    }
}
