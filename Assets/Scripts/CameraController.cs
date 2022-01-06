using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float shakeDuration = 0f;
    public float shakeAmount = 0.7f;

    Transform camTransform;
    Vector3 originalPos;

    [SerializeField]
    float shakeDurationTimer = 0;

    void Awake() {
        camTransform = gameObject.transform;
        originalPos = camTransform.localPosition;
    }


    void Update() {
        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        // Shake(false);
    }

    public void Shake(bool resetTimer, float newShakeAMount = 0, float newDuration = 0) {

        if (newDuration > 0) {
            shakeDuration = newDuration;
        }

        if (resetTimer) {
            shakeDurationTimer = shakeDuration;
            originalPos = camTransform.localPosition;
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