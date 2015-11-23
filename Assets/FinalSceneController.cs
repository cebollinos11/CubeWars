using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalSceneController : MonoBehaviour {
    private GameObject _winnerPlayer;
    private Player _winningPlayer;

    public GameObject PlayerPrefab;
    public GameObject Counter;
    public float rotationSpeed;

    void Awake(){

        //instantiate player
        //_winningPlayer = GameManager.Instance.GetWinningPlayer();
        _winningPlayer = new Player(1);
        _winningPlayer.name = "Luca";
        _winningPlayer.Color = Color.red;
        _winnerPlayer = (GameObject)Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        // set color
        _winnerPlayer.GetComponent<Renderer>().material.color = _winningPlayer.Color;
        _winnerPlayer.name = _winningPlayer.name;
        _winnerPlayer.GetComponent<playerControllerV1>().enabled = false;
        _winnerPlayer.GetComponent<Rigidbody>().isKinematic = true;
        _winnerPlayer.GetComponent<Rigidbody>().isKinematic = true;
        

    }
    
    // Use this for initialization
	void Start () {
        int i = 0;
            Color Temp = _winningPlayer.Color;
            Temp.a = 1f;
            Counter.GetComponent<Text>().color = Temp;
            i++;
        

        Counter.GetComponent<Text>().text = "The winning player is "+_winningPlayer.name+" with "+_winningPlayer.CurrentSceneScore.ToString()+" points. "+ "Press X to return to the Title Screen!";
    }
	
	// Update is called once per frame
	void Update () {
        _winnerPlayer.transform.Rotate(new Vector3(0, Time.deltaTime*rotationSpeed, 0));
	}
}
