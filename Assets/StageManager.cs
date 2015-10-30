using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class StageManager : MonoBehaviour {
    
    public GameObject EndOfGamePanel;
    private GameObject GMO;
    private GameManager GM;
    
    

	// Use this for initialization
	void Start () {
        GMO = GameObject.Find("GameManager");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        

      
        
        
        Debug.Log("calling pause game");
        GetComponent<PauseMenuManager>().PauseGame();

       
	}


	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F)) { FinishGame(); }
        if (Input.GetKeyDown(KeyCode.E)) { GiveRandomPoints(); }
        
	
	}

    void GiveRandomPoints() {
        foreach (Player P in GM.Players) {
            P.AddPoints(5);
            GameObject.Find("LiveScores").GetComponent<LiveScores>().UpdateCounters();
        }
    }

    public void AbandonTournament() {

        Application.LoadLevel("TitleScreen");
        
    }

    public void FinishGame() {

        Debug.Log("finishing game");
        GetComponent<PauseMenuManager>().PauseGame();
        GetComponent<PauseMenuManager>().enabled = false;
        
        EndOfGamePanel.SetActive(true);

        //set text of ScoreText

        
        var S = EndOfGamePanel.transform.Find("ScoreText").gameObject.GetComponent<Text>();
        S.text = "";

        foreach (Player P in GM.Players) {

            int NewAccumulatedScore = P.AccumulatedScore+P.CurrentSceneScore;
            S.text += P.name+":\t"+ P.AccumulatedScore.ToString()+"\t+\t" +P.CurrentSceneScore.ToString()+"\t=\t"+NewAccumulatedScore.ToString();
            P.AccumulatedScore = NewAccumulatedScore;
            S.text += "\n";        
        }
        
       
        
        
    
    }

    
}
