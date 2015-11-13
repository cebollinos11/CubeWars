using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    public GameObject BodyToFollow;
    public float RotationSpeed;
    public bool lowGravity = true;
    private Vector3 OriginalDistanceVector;
    private Rigidbody rigidBodyMaster;
    private int points = 1;

    private float superCoinProbability = 20;
	// Use this for initialization
	void Start () {

        AudioManager.PlayClip(AudioClipsType.RespawnCoin);
        OriginalDistanceVector = BodyToFollow.transform.position - transform.position;
        rigidBodyMaster = BodyToFollow.GetComponent<Rigidbody>();

        if (Random.Range(0, 100)<superCoinProbability) {

            transform.parent.localScale *= 3;
            transform.parent.GetChild(0).localScale *= 3;
            points += 4;
        
        }
	}
	
	// Update is called once per frame
	void Update () {
       // transform.position = new Vector3( BodyToFollow.transform.position.x,BodyToFollow.transform.position.y+1,BodyToFollow.transform.position.z);
        transform.position = BodyToFollow.transform.position - OriginalDistanceVector;
        transform.Rotate(0,  Time.deltaTime*RotationSpeed,0);

        if (lowGravity) {
            rigidBodyMaster.velocity = new Vector3(rigidBodyMaster.velocity.x, rigidBodyMaster.velocity.y + 0.8f, rigidBodyMaster.velocity.z);
            
        }
	
	}

    void OnTriggerEnter(Collider Other) {


        if (Other.gameObject.tag == "Player") {

            AudioManager.PlayClip(AudioClipsType.GetCoin);

            Other.gameObject.GetComponent<PointsManager>().GivePoints(points);
            //GUIManager.Instance.LiveScores.GetComponent<LiveScores>().UpdateCounters();
            
            GUIManager.Instance.UpdateLiveScores();
            Destroy(transform.parent.gameObject);
        }
    
    }
}
