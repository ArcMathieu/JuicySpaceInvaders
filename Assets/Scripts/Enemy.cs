using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum State {
        NORMAL = 0,
        BROKEN = 1,
        DESTROYED = 2
    }
    public State state = State.NORMAL;
    [SerializeField]
    private int direction;
    [SerializeField]
    private int speed = 0;
    Rigidbody rb;

    public float bulletSpawnDist;
    void Start()
    {
        direction = Gino.instance.entitiesManager.enemyDirection;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed * direction, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction != Gino.instance.entitiesManager.enemyDirection) {
            direction = Gino.instance.entitiesManager.enemyDirection;
            rb.velocity = new Vector3(speed * direction, 0, 0);
            NextLine();
        }

        if (Gino.instance.gameManager.leftBorder.transform.position.x >= transform.position.x + transform.localScale.x / 2 && Gino.instance.entitiesManager.enemyDirection == -1 ||
             (Gino.instance.gameManager.rightBorder.transform.position.x <= transform.position.x + transform.localScale.x / 2) && Gino.instance.entitiesManager.enemyDirection == 1) {
            Gino.instance.entitiesManager.newDirection = true;
        }
        if (transform.position.z < Gino.instance.entitiesManager.player.transform.position.z + Gino.instance.entitiesManager.player.transform.localScale.z / 2) {
            Gino.instance.uiManager.GameOver();
        }
    }

    void NextLine() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Gino.instance.entitiesManager.distBetweenLines);
    }

    public void Die() {
        Destroy(gameObject);
        Gino.instance.entitiesManager.enemies.Remove(this);
    }

    public void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward * -1, out hit, Mathf.Infinity)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.transform.GetComponent<Enemy>()) {
                hit.transform.GetComponent<Enemy>().Shoot();
            }
        } else {
            EnemyBullet bullet = Instantiate(Gino.instance.entitiesManager.enemybullet);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - bulletSpawnDist);
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3 (-90, 0, 0);
            bullet.transform.rotation = newRotation;
        }
    }

    public void NewState(bool nextState, State newState = State.DESTROYED) {
        if (nextState) {
            state++;
        } else {
            state = newState;
        }

        if (state == State.DESTROYED) {
            Die();
        }
    }
}
