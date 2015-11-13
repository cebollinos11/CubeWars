using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

    public Vector3 originalPos;
    public int pointsToGive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < -100) {
            Respawn();
        
        }
            
        
	
	}

    void Respawn() {

        GetComponent<playerControllerV1>().NullVelocity();
        transform.position = originalPos;
        //AudioManager.PlayClip(AudioClipsType.RespawnPlayer);

        //give points on kill
        foreach (Player p in GameManager.Instance.Players)
        {
            if(p.playerObject.name != transform.name)
                p.playerObject.GetComponent<PointsManager>().GivePoints(pointsToGive);
        }
        



        

    }
}
