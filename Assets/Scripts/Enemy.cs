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
    [SerializeField]
    private float randomXValue = 0;
    [SerializeField]
    private float randomZValue = 0;
    [SerializeField]
    private float animationMinSeconds = 0;
    [SerializeField]
    private float animationMaxSeconds = 0;
    private float minAnimationFrame = 0;
    private float maxAnimationFrame = 0;
    Vector3 lastPosition;
    Vector3 originPosition;
    Vector3 addPosition;
    Vector3 nextPosition;
    float animationFrame;
    float percentOfEndAnimation;
    public bool die = false;

    public GameObject[] bodies = new GameObject[0];
    [SerializeField]
    GameObject body;
    BoxCollider bc;
    Rigidbody rb;

    public bool hit = false; 

    public float bulletSpawnDist;
    void Start()
    {
        minAnimationFrame = animationMinSeconds * 50f;
        maxAnimationFrame = animationMaxSeconds * 50f;
        nextPosition = Vector3.zero;
        addPosition = Vector3.zero;
        direction = Gino.instance.entitiesManager.enemyDirection;
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        rb.velocity = new Vector3(speed * direction, 0, 0);
        NewBody(bodies.Length - 1);
        NewAnimation(randomXValue, randomZValue, minAnimationFrame, maxAnimationFrame);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!die) {
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
            if (Gino.instance.juicyManager.isMovement) {
                 Animation();
            }
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
        Gino.instance.soundsManager.Play("Shoot Police");
    }

    public void NewState(bool nextState, State newState = State.DESTROYED) {
        if (nextState && state != State.DESTROYED) {
            state--;
        } else {
            state = newState;
        }

        if (state == State.DESTROYED) {
            StartCoroutine(Die());
        }

        NewBody((int)state);
    }

    public void Hit() {
        Gino.instance.cameraManager.NewCameraShake(Gino.instance.entitiesManager.enemyHitCameraShake.x, Gino.instance.entitiesManager.enemyHitCameraShake.y);
        if (!die) {
            NewState(true); 
        }
    }

    IEnumerator Die() {
        die = true;
        Gino.instance.cameraManager.NewCameraShake(1, 2);
        Gino.instance.entitiesManager.enemies.Remove(this);
       // GameObject particle = body.transform.GetChild(0).gameObject;
       // particle.transform.parent = null;
        rb.velocity = Vector3.zero;
        Vector3 force = new Vector3(0, 1, 0.5f);
        force = force * 700;
        rb.AddForce(force);
        rb.useGravity = true;
        bc.isTrigger = true;
        hit = false;
        yield return new WaitForSeconds(1f);
    //    Destroy(particle);
        while (hit != true) {
            yield return null;
        }
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        force = new Vector3(0, 0, 1);
        force = force * 2000;
        rb.AddForce(force);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    void NewBody(int newBodyRange) {
        GameObject newBody = Instantiate(bodies[newBodyRange]);
        newBody.transform.position = transform.position;
        newBody.transform.eulerAngles = transform.eulerAngles + newBody.transform.eulerAngles;
        newBody.transform.SetParent(transform);
        newBody.transform.localScale = new Vector3(newBody.transform.localScale.x * transform.localScale.x, newBody.transform.localScale.y * transform.localScale.y, newBody.transform.localScale.z * transform.localScale.z);
        if (body != null) {
            body.SetActive(false);
            Destroy(body);
        }
        body = newBody;
    }

    public void Animation() {
        percentOfEndAnimation += 1f / animationFrame;
        originPosition = transform.position - addPosition;
        addPosition = LerpPosition(lastPosition, nextPosition, percentOfEndAnimation);
        transform.position = originPosition + addPosition;
        if (percentOfEndAnimation >= 1) {
            NewAnimation(randomXValue, randomZValue, minAnimationFrame, maxAnimationFrame);
        }
    }

    Vector3 LerpPosition(Vector3 first, Vector3 second, float Between) {
        Vector3 final = Vector3.zero;
        final.x = Mathf.Lerp(first.x, second.x, Between);
        final.y = Mathf.Lerp(first.y, second.y, Between);
        final.z = Mathf.Lerp(first.z, second.z, Between);
        return final;
    }

    void NewAnimation(float randomXValue, float randomZValue, float minAnimationFrame, float maxAnimationFrame) {
        lastPosition = nextPosition;
        float randomX = Random.Range(-randomXValue, randomXValue);
        float randomZ = Random.Range(-randomZValue, randomZValue);
        animationFrame = Random.Range(minAnimationFrame, maxAnimationFrame);
        percentOfEndAnimation = 0;
        nextPosition = new Vector3(randomX, 0, randomZ);
    }


    private void OnTriggerEnter(Collider other) {
        if (die && other.gameObject.tag == "Ground") {
            hit = true;
        }
    }
}
