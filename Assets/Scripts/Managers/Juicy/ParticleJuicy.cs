using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleJuicy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Gino.instance.juicyManager.particles.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gino.instance.juicyManager.isParticle == false) {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        Gino.instance.juicyManager.particles.Remove(this);
    }
}
