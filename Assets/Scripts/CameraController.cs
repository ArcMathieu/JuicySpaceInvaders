using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    public Transform camTransform;
    private Vector3 originalPos = Vector3.zero;
    public AnimationCurve curve;

    public class Shake {
        public float shakeAmount = 0;
        public float currentShakeAmount = 0;
        public float shakeDuration = 0;
        public float currentShakeDuration = 0;
    }
    List<Shake> shakes = new List<Shake>();
    void Start() {
        camTransform = gameObject.transform;
        originalPos = camTransform.localPosition;
        NewCameraShake(5, 3);
    }

    void Update() {
        CameraShake();
    }

    public void CameraShake() {
        float mainAmount = 0;
        List<Shake> toRemove = new List<Shake>();
        for (int i = 0; i < shakes.Count; i++) {
            if (shakes[i].currentShakeAmount > 0 && shakes[i].currentShakeDuration > 0) {
                shakes[i].currentShakeDuration -= Time.deltaTime;
                if (shakes[i].currentShakeDuration < 0) {
                    shakes[i].currentShakeDuration = 0;
                }
                float durationRemaining = Mathf.Clamp01(shakes[i].currentShakeDuration);
                float shakeValue = Mathf.Lerp(shakes[i].shakeAmount, 0, durationRemaining);
                shakes[i].currentShakeAmount = curve.Evaluate(shakeValue);
                if (shakes[i].currentShakeAmount > mainAmount) {
                    mainAmount = shakes[i].currentShakeAmount;
                }
            } else if (shakes[i].currentShakeDuration != 0 || shakes[i].currentShakeAmount != 0) {
                toRemove.Add(shakes[i]);
            }
        }
        for (int i = 0; i < toRemove.Count; i++) {
            shakes.Remove(toRemove[i]);
        }
        camTransform.localPosition = originalPos + Random.insideUnitSphere * mainAmount;
    }

    public void NewCameraShake(float newDuration, float newAmount) {
        Shake newShake = new Shake();
        newShake.currentShakeAmount = newAmount;
        newShake.shakeAmount = newAmount;
        newShake.currentShakeDuration = newDuration;
        newShake.shakeDuration = newDuration;
        shakes.Add(newShake);
    }
}