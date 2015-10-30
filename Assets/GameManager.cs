using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player {

    public bool Active = true;
    public int AccumulatedScore = 0;
    public int CurrentSceneScore = 0;
    public string name = "";
    public Color Color;
    public int id;
    
    

    public Player(int setId) {
        id = setId;
        name = "Player " + id.ToString();
        Debug.Log("created "+name);        
    }

    

    public void AddPoints(int d) {
        Debug.Log("Added " + d.ToString() + " points to " + name);
        CurrentSceneScore += d;
    }

    public void ToggleActivate() {
        
        this.Active = true;
    }
}

public class GameManager : Singleton<GameManager> {

    public GameObject PlayerPrefab;
    public Player[] Players = new Player[4];    
    public Color[] PlayerColors = new Color[4];
    private GameObject PlayerTogglers;
    public stageController stageController;
    
    UnityEngine.UI.Text TestString;

    public List<StageClass> _StagesDB;
    public StageClass currentStage;

	// Use this for initialization
	void Start () {

        Debug.Log("Initializing players array");

        //Init Players array
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player(i+1);
            Players[i].Color = PlayerColors[i];
           
        }	

        //Find TestPlayersPreview

        //TestString = GameObject.Find("TestPreview").GetComponent<Text>();

        Debug.Log(Application.loadedLevelName);
                
        

	}
	
	// Update is called once per frame
	void Update () {


      
       
        
	}

    public void checkActivatedPlayers() { 
                
    }

    void EnablePlayer(int n) {
        Players[n].ToggleActivate();
    }

    public void EnablePlayerUI(int n) {
        
        EnablePlayer(n);
       
    }

    public void StartNewTournament(){
        DontDestroyOnLoad(gameObject);
        Application.LoadLevel(1);
    }

    public void LoadNewStage() {

        int stageIndex = GUIManager.Instance.StageSelector.value;

        currentStage = _StagesDB[stageIndex];
        string lvlname = _StagesDB[stageIndex].levelname;
        DontDestroyOnLoad(gameObject);
        Debug.Log("trying to load " + lvlname);
        Application.LoadLevel(lvlname);
        GUIManager.Instance.MainMenu.SetActive(false);
        //GUIManager.Instance.InstructionsPanel.SetActive(true);
    }
}
