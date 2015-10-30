using UnityEngine;
using System.Collections;

public class Mill : MonoBehaviour {

    public float RotSpeed;
    public float ChangeBehaviourTime;

	// Use this for initialization
	void Start () {
        ChangeBehaviour();
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);	
	}

    void ChangeBehaviour() {
        RotSpeed = -RotSpeed;
        Invoke("ChangeBehaviour", ChangeBehaviourTime); 
    }
}
