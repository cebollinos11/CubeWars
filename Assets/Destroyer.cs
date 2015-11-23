using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GUIManager.Instance.LiveScores.SetActive(false);

        foreach (Player p in GameManager.Instance.Players) {
            p.CurrentSceneScore = 0;
            
        }


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Start"))
        {

            Debug.Log("yeah");
            AudioManager.PlayBgSong(2);   
            Application.LoadLevel("TitleScreen");
        }
	
	}
}
