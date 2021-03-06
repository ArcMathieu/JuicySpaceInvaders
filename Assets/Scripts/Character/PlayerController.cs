using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 shootShakeValue;
    public Vector2 hitShakeValue;
    public enum State
    {
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
    [SerializeField]
    private float shootTime = 1;

    private float shootTimer = 0;

    private bool shoot = false;
    public GameObject[] bodies = new GameObject[0];
    [SerializeField]
    GameObject body;
    Rigidbody rb;
    BoxCollider bc;

    public float bulletSpawnDist;
    public bool canMove;
    bool alreadySpawn;
    public Animator playerAnim;
    public Animator BangAnim;
    bool canBeHit;

    void Start()
    {
        NewBody(bodies.Length - 1);
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
        canMove = true;
        canBeHit = true;
    }
    // Update is called once per frame
    void Update()
    {
        #region Input
        direction = Input.GetAxis("Horizontal");
        if (Gino.instance.juicyManager.isAnimation)
        {
            if (direction >= -0.1f && direction <= 0.1f)
                playerAnim.SetBool("Turn", false);
            else
                playerAnim.SetBool("Turn", true);
        }

        if (Input.GetKeyDown("space"))
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
        #endregion
        Shoot();
        if (canMove)
            Movement();
    }

    void Movement()
    {
        transform.position = new Vector3(transform.position.x, 0, 0);
        if (Gino.instance.gameManager.leftBorder.transform.position.x >= transform.position.x + bc.size.x / 2 && direction <= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(Gino.instance.gameManager.leftBorder.transform.position.x - bc.size.x / 2, transform.position.y, transform.position.z);
        }
        else if (Gino.instance.gameManager.rightBorder.transform.position.x <= transform.position.x + bc.size.x / 2 && direction >= 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(Gino.instance.gameManager.rightBorder.transform.position.x - bc.size.x / 2, transform.position.y, transform.position.z);
        }
        else
        {
            rb.velocity = new Vector3(characterHorizontalSpeed * direction, 0, 0);
        }
        if (Gino.instance.juicyManager.isMovement)
        {
            RotateDeltaTime();
        }
    }

    void Shoot()
    {
        if (shoot && shootTimer <= 0)
        {
            if (Gino.instance.juicyManager.isUI)
                BangAnim.SetTrigger("Bang");

            if (Gino.instance.juicyManager.isAnimation)
            {
                //transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Shoot");
                playerAnim.SetTrigger("Shoot");

            }
            Gino.instance.soundsManager.Play("Shoot Player");
            Gino.instance.cameraManager.NewCameraShake(shootShakeValue.x, shootShakeValue.y);
            Bullet newBullet = Instantiate(Gino.instance.entitiesManager.bullet);
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + bulletSpawnDist);
            shootTimer = shootTime;
            if (Gino.instance.juicyManager.isMovement)
            {
                StartCoroutine(Propulsion(propulsionFrame, propulsionDist));
            }
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }

    IEnumerator Propulsion(float frame, int jumpSize)
    {
        float jump = 0;
        Vector3 position = transform.position;
        int time = 0;
        jump = -jumpSize / (frame * 2);
        while (time < frame / 2)
        {
            transform.position += new Vector3(0, 0, jump);
            time++;
            yield return null;
        }
        jump = jumpSize / (frame * 2);
        while (time < frame)
        {
            transform.position += new Vector3(0, 0, jump);
            time++;
            yield return null;
        }
    }


    private void RotateDeltaTime()
    {
        float newAngle = 0.5f;
        float lastAngle = Mathf.InverseLerp(180 - maxAngleRotation, 180 + maxAngleRotation, transform.localEulerAngles.y);
        if (direction == 0)
        {
            if (lastAngle != 0.5f)
            {
                newAngle = Mathf.Clamp(Time.deltaTime * -Mathf.Sign(lastAngle - 0.5f) * characterAngularSpeedComeback / 2 + lastAngle, 0, 1);
                if (Mathf.Lerp(180 - maxAngleRotation, 180 + maxAngleRotation, newAngle) > 180 - minAngleRotation && Mathf.Lerp(180 - maxAngleRotation, 180 + maxAngleRotation, newAngle) < 180 + minAngleRotation || Mathf.Sign(newAngle - 0.5f) != Mathf.Sign(lastAngle - 0.5f))
                {
                    newAngle = 0.5f;
                }
            }

        }
        else
        {
            newAngle = Time.deltaTime * -direction * characterAngularSpeed / 2 + Mathf.InverseLerp(180 - maxAngleRotation, 180 + maxAngleRotation, transform.localEulerAngles.y);
        }
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 180 - maxAngleRotation, 0), Quaternion.Euler(0, 180 + maxAngleRotation, 0), newAngle);
    }

    public void ChangeLifePoint(int changeLifePoint)
    {
        lifePoint += changeLifePoint;
        if(lifePoint < 1) 
        {
            GameOvered();
        }
    }

    public void NewState(bool nextState, State newState = State.HIT3)
    {
        if (nextState)
        {
            Gino.instance.cameraManager.NewCameraShake(hitShakeValue.x, hitShakeValue.y);
            state--;
            Gino.instance.uiManager.LoseLife((int)state);
        }
        else
        {
            state = newState;
        }

        NewBody((int)state);
    }

    public void Hit()
    {
        if (canBeHit)
        {
            canMove = true;
            StartCoroutine(hitted());
            ChangeLifePoint(-1);
            Gino.instance.soundsManager.Play("Hit");
            //Gino.instance.cameraManager.ZoomEffect();

            if (state != State.HIT3)
                NewState(true);
        }
    }

    public void GameOvered()
    {
        canMove = false;
        GameManager.instance.LaunchGameOver();
    }
    IEnumerator hitted()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1);
        canBeHit = true;
    }
    void NewBody(int newBodyRange)
    {
        GameObject newBody = Instantiate(bodies[newBodyRange]);
        newBody.transform.position = transform.position + newBody.transform.localPosition;
        newBody.transform.eulerAngles = transform.eulerAngles + newBody.transform.localEulerAngles;
        newBody.transform.SetParent(transform);
        newBody.transform.localScale = new Vector3(newBody.transform.localScale.x * transform.localScale.x, newBody.transform.localScale.y * transform.localScale.y, newBody.transform.localScale.z * transform.localScale.z);
        if (body != null)
        {
            body.SetActive(false);
            Destroy(body);
        }
        body = newBody;
    }
}
