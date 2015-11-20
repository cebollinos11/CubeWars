using UnityEngine;
using System.Collections;

public class rotateCPlane : MonoBehaviour {

    [SerializeField]
    float RotSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, Time.deltaTime * RotSpeed);
	}
}
