using UnityEngine;
using System.Collections;

public class FanRotatorManager : MonoBehaviour {
    Transform t;
    public float rotationSpeed;
	// Use this for initialization
	void Awake () {
        t = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        t.Rotate(0, rotationSpeed*Time.deltaTime, 0);
	}
}
