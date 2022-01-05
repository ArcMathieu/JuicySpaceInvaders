using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;

    Transform camTransform;
    Vector3 originalPos;

    [SerializeField]
    float shakeDurationTimer = 0;

    void Awake() {
        camTransform = gameObject.transform;
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        Shake(false, 0);
    }

    void Shake(bool resetTimer, float newShakeAMount, float newDuration = 0) {

        if (newDuration > 0) {
            shakeDuration = newDuration;
        }

        if (resetTimer) {
            shakeDurationTimer = shakeDuration;
        }

        if (newShakeAMount > 0) {
            shakeAmount = newShakeAMount;
        }

        if (shakeDurationTimer > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            if (!resetTimer) {
                shakeDurationTimer -= Time.deltaTime;
            }
        } else {
            shakeDurationTimer = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}