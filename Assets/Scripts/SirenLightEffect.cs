using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenLightEffect : MonoBehaviour
{
    [SerializeField] private Light redLight;
    [SerializeField] private Light blueLight;

    private Vector3 redT;
    private Vector3 blueT;

    [SerializeField] int speed;

    // Update is called once per frame
    void Update()
    {
        redT.y += speed * Time.deltaTime;
        blueT.y -= speed * Time.deltaTime;
        redT.x = 12;
        blueT.x = 12;

        redLight.transform.eulerAngles = redT;
        blueLight.transform.eulerAngles = blueT;
    }
}
