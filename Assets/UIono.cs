using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIono : MonoBehaviour
{
    public bool canFade;
    public Color alphaColor;
    public float timeToFade = 1.0f;

    public void Start()
    {
        canFade = false;
        alphaColor = GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }
    public void Update()
    {
        if (canFade)
        {
            //GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
            var material = GetComponent<Renderer>().material;
            var color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (timeToFade * Time.deltaTime));
        }

    }
}
