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
    public int inSpeed = 0;
    [SerializeField] float lightRange;
    [SerializeField] float lightIntensity;

    float oldLightRange;
    float oldLightIntensity;

    float rdn;
    public float randomF;
    bool startLight;
    private void Start()
    {
        oldLightRange = lightRange;
        oldLightIntensity = lightIntensity;
        redLight.range = lightRange;
        blueLight.range = lightRange;
        redLight.intensity = lightIntensity;
        blueLight.intensity = lightIntensity;
        randomF = Random.Range(0, 300);
    }
    IEnumerator DecalStart()
    {
        yield return new WaitForSeconds(randomF/60);
        inSpeed = speed;
        startLight = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!startLight)
            StartCoroutine(DecalStart());

        redT.y += inSpeed * Time.deltaTime;
        blueT.y -= inSpeed * Time.deltaTime;
        redT.x = 12;
        blueT.x = 12;

        redLight.transform.eulerAngles = redT;
        blueLight.transform.eulerAngles = blueT;

        if (lightRange != oldLightRange || lightIntensity != oldLightIntensity)
            ReloadLight();
    }

    private void ReloadLight()
    {
        redLight.range = lightRange;
        blueLight.range = lightRange;
        redLight.intensity = lightIntensity;
        blueLight.intensity = lightIntensity;
        oldLightRange = lightRange;
        oldLightIntensity = lightIntensity;
    }
}
