using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPawn : MonoBehaviour
{
    public Transform PlayerCar;

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Mathf.Clamp(-PlayerCar.position.x, PlayerCar.position.x - 5, PlayerCar.position.x + 5), transform.position.y, transform.position.z);
        transform.position = PlayerCar.position;
    }
}
