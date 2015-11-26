using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    public float RotationPower;
    public bool randomizeMovement;
    public bool clockwise;
	// Use this for initialization
    private float max=5f;
    private float timer;
	void Start () {
        gameObject.GetComponent<Rigidbody>().maxAngularVelocity = RotationPower * 1000f;
        timer = Random.Range(0, max);
	}
	
	// Update is called once per frame
	void Update () {
        if (randomizeMovement) { 
        timer -= Time.deltaTime;
        if(timer<=0)
        {

            timer = Random.Range(0, max);
            clockwise = !clockwise;
        }
        }
        if (clockwise)
        gameObject.transform.Rotate(new Vector3(0,RotationPower*Time.deltaTime,0));
        else
            gameObject.transform.Rotate(new Vector3(0, -RotationPower * Time.deltaTime, 0));
    }
}
