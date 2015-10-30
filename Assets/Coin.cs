using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public GameObject BodyToFollow;
    public float RotationSpeed;
    private Vector3 OriginalDistanceVector;
	// Use this for initialization
	void Start () {
        OriginalDistanceVector = BodyToFollow.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
       // transform.position = new Vector3( BodyToFollow.transform.position.x,BodyToFollow.transform.position.y+1,BodyToFollow.transform.position.z);
        transform.position = BodyToFollow.transform.position - OriginalDistanceVector;
        transform.Rotate(0,  Time.deltaTime*RotationSpeed,0);
	
	}

    void OnTriggerEnter(Collider Other) {


        if (Other.gameObject.tag == "Player") {
            Destroy(transform.parent.gameObject);
        }
    
    }
}
