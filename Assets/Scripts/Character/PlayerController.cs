using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum State {
        NORMAL = 3,
        HIT1 = 2,
        HIT2 = 1,
        HIT3 = 0
    }

    State state = State.NORMAL;

    [SerializeField]
    private int lifePoint;
    [SerializeField]
    private float direction = 0;
    [SerializeField]
    private float characterHorizontalSpeed = 0;
    [SerializeField]
    private float characterAngularSpeed = 0;
    [SerializeField]
    private float characterAngularSpeedComeback = 0;
    [SerializeField]
    private float maxAngleRotation = 0;
    [SerializeField]
    private float minAngleRotation = 0;
    [SerializeField]
    private int propulsionFrame = 0;
    [SerializeField]
    private int propulsionDist = 0;

    private float shootTimer = 0;
    private float shootTime = 1;

    private bool shoot = false;
    public GameObject[] bodies = new GameObject[0];
    [SerializeField]
    GameObject body;
    Rigidbody rb;
    BoxCollider bc;

    public float bulletSpawnDist;
    void Start() {
        NewBody(bodies.Length - 1);
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
        #region Input
        direction = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown("space")) {
            shoot = true;
        } else {
            shoot = false;
        }
        #endregion
        Shoot();
        Movement();   
    }

    void Movement() {
        if (Gino.instance.gameManager.leftBorder.transform.position.x >= transform.position.x + bc.size.x / 2 && direction <= 0){
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(Gino.instance.gameManager.leftBorder.transform.position.x - bc.size.x / 2, transform.position.y, transform.position.z);
        }else if(Gino.instance.gameManager.rightBorder.transform.position.x <= transform.position.x + bc.size.x / 2 && direction >= 0) {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(Gino.instance.gameManager.rightBorder.transform.position.x - bc.size.x / 2, transform.position.y, transform.position.z);
        } else {
            rb.velocity = new Vector3(characterHorizontalSpeed * direction, 0, 0);
        }
        RotateDeltaTime();
    }

    void Shoot() {
        if (shoot && shootTimer <= 0) {
            Bullet newBullet = Instantiate(Gino.instance.entitiesManager.bullet);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + bulletSpawnDist);
            shootTimer = shootTime;
            StartCoroutine(Propulsion(propulsionFrame, propulsionDist));
        } else {
            shootTimer -= Time.deltaTime;
        }
    }

    IEnumerator Propulsion(float frame, int jumpSize) {
        float jump = 0;
        Vector3 position = transform.position;
        int time = 0;
        jump = -jumpSize / (frame * 2);
        while (time < frame / 2) {
            transform.position += new Vector3 (0,0, jump);
            time++;
            yield return null;
            Debug.Log(jump);
        }
        jump = jumpSize / (frame * 2);
        while (time < frame) {
            transform.position += new Vector3(0, 0, jump);
            time++;
            yield return null;
        }
    }


    private void RotateDeltaTime() {
        float newAngle = 0.5f;
        float lastAngle = Mathf.InverseLerp(180 - maxAngleRotation, 180 + maxAngleRotation, transform.localEulerAngles.y);
        if (direction == 0) {
            if (lastAngle != 0.5f) {
                newAngle = Mathf.Clamp(Time.deltaTime * -Mathf.Sign(lastAngle - 0.5f) * characterAngularSpeedComeback / 2 + lastAngle, 0, 1);
                if (Mathf.Lerp(180 - maxAngleRotation, 180 + maxAngleRotation, newAngle) > 180 - minAngleRotation && Mathf.Lerp(180 - maxAngleRotation, 180 + maxAngleRotation, newAngle) < 180 + minAngleRotation || Mathf.Sign(newAngle - 0.5f) != Mathf.Sign(lastAngle - 0.5f)) {
                    newAngle = 0.5f;
                }
            }

        } else {
            newAngle = Time.deltaTime * -direction * characterAngularSpeed / 2  + Mathf.InverseLerp(180 - maxAngleRotation, 180 + maxAngleRotation, transform.localEulerAngles.y);
        }
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 180 - maxAngleRotation, 0) , Quaternion.Euler(0,180 + maxAngleRotation,0), newAngle);
    }

    public void ChangeLifePoint(int changeLifePoint) {
        lifePoint += changeLifePoint;
        if (lifePoint < 1) {
            Gino.instance.uiManager.GameOver();
        }
    }

    public void NewState(bool nextState, State newState = State.HIT3) {
        if (nextState) {
            state--;
        } else {
            state = newState;
        }

        if (state == State.HIT3) {
            Gino.instance.uiManager.GameOver();
        }
        NewBody((int)state);
    }

    public void Hit() {
        ChangeLifePoint(-1);
        NewState(true);
        Gino.instance.uiManager.LoseLife((int)state);
    }

    void NewBody(int newBodyRange) {
        GameObject newBody = Instantiate(bodies[newBodyRange]);
        newBody.transform.position = transform.position + newBody.transform.localPosition;
        newBody.transform.eulerAngles = transform.eulerAngles + newBody.transform.localEulerAngles;
        newBody.transform.SetParent(transform);
        if (body != null) {
            body.SetActive(false);
            Destroy(body);
        }
        body = newBody;
    }
}
