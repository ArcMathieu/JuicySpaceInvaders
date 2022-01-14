using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightJuicy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gino.instance.juicyManager.lights.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gino.instance.juicyManager.isLight == false) {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        Gino.instance.juicyManager.lights.Remove(this);
    }
}
