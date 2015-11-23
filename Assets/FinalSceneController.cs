using UnityEngine;
using System.Collections;

public class FinalSceneController : MonoBehaviour {
    private GameObject _winnerPlayer;

    public float rotationSpeed;
    void Awake(){
        //instantiate player
        Player winningPlayer = GameManager.Instance.GetWinningPlayer();
        GameObject p = (GameObject)Instantiate(GameManager.Instance.PlayerPrefab, Vector3.zero, Quaternion.identity);
        // set color
        p.GetComponent<Renderer>().material.color = winningPlayer.Color;
        p.name = winningPlayer.name;
        p.GetComponent<playerControllerV1>().enabled = false;
        p.GetComponent<Rigidbody>().isKinematic = true;
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
