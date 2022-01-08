using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum State {
        NORMAL = 2,
        BURNING = 1,
        DESTROYED = 0
    }
    public State state = State.NORMAL;
    [SerializeField]
    private int direction;
    [SerializeField]
    private int speed = 0;

    public GameObject[] bodies = new GameObject[0];
    [SerializeField]
    GameObject body;
    BoxCollider bc;
    Rigidbody rb;

    public float bulletSpawnDist;
    void Start()
    {
        direction = Gino.instance.entitiesManager.enemyDirection;
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        rb.velocity = new Vector3(speed * direction, 0, 0);
        NewBody(bodies.Length - 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(speed * direction, 0, 0);
        if (direction != Gino.instance.entitiesManager.enemyDirection) {
            direction = Gino.instance.entitiesManager.enemyDirection;
            NextLine();
        }

        if (Gino.instance.gameManager.leftBorder.transform.position.x >= transform.position.x +bc.size.x / 2 && Gino.instance.entitiesManager.enemyDirection == -1 ||
             (Gino.instance.gameManager.rightBorder.transform.position.x <= transform.position.x +bc.size.x / 2) && Gino.instance.entitiesManager.enemyDirection == 1) {
            Gino.instance.entitiesManager.newDirection = true;
        }
        if (transform.position.z < Gino.instance.entitiesManager.player.transform.position.z + Gino.instance.entitiesManager.player.transform.localScale.z / 2) {
            Gino.instance.uiManager.GameOver();
        }
    }

    void NextLine() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Gino.instance.entitiesManager.distJump);
    }

    public void Shoot() {
        RaycastHit hit;
        Vector3 positionShoot = new Vector3(transform.position.x, transform.position.y, transform.position.z -bc.size.z);
        if (Physics.Raycast(positionShoot, Vector3.forward * -1, out hit, Mathf.Infinity)) {
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
        Debug.Log("shoot");
    }

    public void NewState(bool nextState, State newState = State.DESTROYED) {
        if (nextState) {
            state--;
        } else {
            state = newState;
        }

        if (state == State.DESTROYED) {
            Die();
        }

        NewBody((int)state);
    }

    public void Hit() {
        NewState(true);
    }

    public void Die() {
        Destroy(gameObject);
        Gino.instance.entitiesManager.enemies.Remove(this);
    }
    void NewBody(int newBodyRange) {
        GameObject newBody = Instantiate(bodies[newBodyRange]);
        newBody.transform.position = transform.position;
        newBody.transform.eulerAngles = transform.eulerAngles + newBody.transform.eulerAngles;
        newBody.transform.SetParent(transform);
        if (body != null) {
            body.SetActive(false);
            Destroy(body);
        }
        body = newBody;
    }
}
