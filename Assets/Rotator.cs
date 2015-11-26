using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public float RotationPower;
    public bool clockwise;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Rigidbody>().maxAngularVelocity = RotationPower * 1000f;
	}
	
	// Update is called once per frame
	void Update () {
        if(clockwise)
        gameObject.transform.Rotate(new Vector3(0,RotationPower*Time.deltaTime,0));
        else
            gameObject.transform.Rotate(new Vector3(0, -RotationPower * Time.deltaTime, 0));
    }
}
