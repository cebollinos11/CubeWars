using UnityEngine;
using System.Collections;

public class FinalSceneController : MonoBehaviour {
    private GameObject _winnerPlayer;

    public float rotationSpeed;
    void Awake(){
        _winnerPlayer = GameObject.FindGameObjectWithTag("Player");
        _winnerPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _winnerPlayer.transform.Rotate(new Vector3(0, Time.deltaTime*rotationSpeed, 0));
	}
}
