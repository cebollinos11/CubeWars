using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    Vector3 originalPosition;
    Vector3 targetPosition;
    public float yDamp;
    public float speed;
	// Use this for initialization
	void Start () {

        originalPosition = transform.position;
        targetPosition = transform.position + new Vector3(0, -yDamp, 0);
       
        
	
	}
	
	// Update is called once per frame
	void Update () {

      //transform.position = Vector3.Lerp();
	}
}
