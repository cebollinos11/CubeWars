using UnityEngine;
using System.Collections;

public class cameraShaker : MonoBehaviour {

    Transform originalPos;
    void Start() {
        originalPos = transform;
    }

    public void Shake() {

        Transform goTo;
        goTo = originalPos;
        goTo.Translate(0f,0f,-1f);
        //transform.position = goTo.position;
    }

    void Update() {

        //transform.position = Vector3.Lerp(transform.position, originalPos.position, Time.deltaTime * 0.8f);
       
    }
}