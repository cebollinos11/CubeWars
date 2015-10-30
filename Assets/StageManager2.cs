using UnityEngine;
using System.Collections;

public class StageManager2 : MonoBehaviour {

    public Transform[] SpawnList;    

	// Use this for initialization
	void Start () {       
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void BuildStage() {

        Debug.Log("Spawning chars");
        SpawnCharacters();
        
    }


    public void SpawnCharacters()
    {
        
        var i = 0;
        foreach (Transform SpawnPoint in SpawnList)
        {
            Debug.Log("Spawning char");
            if (GameManager.Instance.Players[i].Active)
            {
                //instantiate player
                GameObject p = (GameObject)Instantiate(GameManager.Instance.PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
                // set color
                p.GetComponent<Renderer>().material.color = GameManager.Instance.Players[i].Color;
                p.name = GameManager.Instance.Players[i].name;
            }
            else { Debug.Log("not active"); }

            i++;


        }

    }
}
