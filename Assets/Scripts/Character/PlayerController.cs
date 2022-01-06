using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {



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

    private bool isStillBullet = false;
    private bool shoot = false;

    Rigidbody rb;

    public float bulletSpawnDist;
    void Start() {
        rb = GetComponent<Rigidbody>();
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
        rb.velocity = new Vector3(characterHorizontalSpeed * direction, 0, 0);
        RotateDeltaTime();
    }

    void Shoot() {
        if (!isStillBullet && shoot) {
            Bullet newBullet = Instantiate(Gino.instance.entitiesManager.bullet);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + bulletSpawnDist);
            isStillBullet = true;
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

    public void CanShoot() {
        isStillBullet = false;
    }
}
