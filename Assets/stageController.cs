using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class stageController : Singleton<stageController> {

    private GameObject instructionsPanel;
    private StageManager2 SM;
    private string stageStatus = "";

    void PauseGame() {
        Time.timeScale = 0;
        //GameManager.Instance.
        GameManager.Instance.stageController = this;
    }

    void UnPauseGame() {
        Time.timeScale = 1;
        Debug.Log("unpausing");
    }

	// Use this for initialization
	void Start () {
        StartStage();
        
	}

    void StartStage(){

        instructionsPanel = GUIManager.Instance.InstructionsPanel;
        SM = GetComponent<StageManager2>();
        StartGeneration();
        //GUIManager.Instance.StageSelector.gameObject.SetActive(true);
    
    }
    void StartGeneration() {

        SM.BuildStage();
        stageStatus = "showing_instructions";
        GUIManager.Instance.InstructionsPanel.SetActive(true);
        PauseGame();
        Debug.Log("setting stagestatus to - " + stageStatus);
        ShowInstructions();

    }
    void ShowInstructions() {

        instructionsPanel.transform.FindChild("StageName").GetComponent<Text>().text = GameManager.Instance.currentStage.name;
        instructionsPanel.transform.FindChild("InstructionText").GetComponent<Text>().text = GameManager.Instance.currentStage.description;
        //instructionsPanel.transform.FindChild("StartButton").GetComponent<Button>().onClick.AddListener(()=> PlayerClickedOnInstructionsButton());
        instructionsPanel.gameObject.SetActive(true);

    }

    void HideInstructions() {
        instructionsPanel.gameObject.SetActive(false);

    }
	// Update is called once per frame

  
	
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space)) {

            StageIsOver();
            //GUIContinue();
        }	
	}

    public void ClosedInstructionsPanel() {        
        UnPauseGame();
    }

    void StageIsOver() {

        PauseGame();
        GUIManager.Instance.EndOfRoundPanel.SetActive(true);

        //var S = GUIManager.Instance.EndOfRoundPanel.transform.Find("ScoreText").gameObject.GetComponent<Text>();
        //S.text = "";
        Text S = GUIManager.Instance.EndOfRoundScoreText;
        S.text = "";

        foreach (Player P in GameManager.Instance.Players)
        {

            int NewAccumulatedScore = P.AccumulatedScore + P.CurrentSceneScore;
            S.text += P.name + ":\t" + P.AccumulatedScore.ToString() + "\t+\t" + P.CurrentSceneScore.ToString() + "\t=\t" + NewAccumulatedScore.ToString();
            P.AccumulatedScore = NewAccumulatedScore;
            S.text += "\n";
        }
    
    }

    
    

}
