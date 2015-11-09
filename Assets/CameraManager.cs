using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    Vector3 originalPosition;
    Vector3 targetPosition;
    
    
    Vector3 FocusPoint;
    public float y;
    //public Vector3 target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {

        originalPosition = transform.position;     
        FocusPoint = Vector3.zero;
        
        //target= originalPosition + new Vector3(0, 0, y);
	
	}
	
	// Update is called once per frame
	void Update () {

        //transform.LookAt(FocusPoint);

      //transform.position = Vector3.Lerp();

        //transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        //transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition.z);
        transform.position = new Vector3(originalPosition.x + Mathf.Sin(Time.time * smoothTime/2) * y*2, transform.position.y, transform.position.z);

       
	}
}
