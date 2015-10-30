using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

    public bool GameIsPaused = false;
    public GameObject PauseMenu;
	// Use this for initialization
	void Start () {
        PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (check_for_pausekey())
        {

            TogglePause();      
                  

        }
	
	}

    void ShowPauseMenu() {
        PauseMenu.SetActive(true);   
    }

    public void HidePauseMenu() {
        PauseMenu.SetActive(false);
    }

    bool check_for_pausekey()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return true;
        }
        return false;
    }

    public void PauseGame() {
        GameIsPaused = true;
        Debug.Log("Game was Paused");
        Time.timeScale = 0;
        
    
    }

   public void ResumeGame()
    {
        GameIsPaused = false;
        Debug.Log("Game REsumed");
        Time.timeScale = 1;
        
    }
    public void TogglePause()
    {

        
        
        if (GameIsPaused)
        {
            ResumeGame();
            HidePauseMenu();
        }

        else
        {
            //if(GetComponent<StageManager>().CurrentStage.CanBePaused){
            //    PauseGame();
            //   ShowPauseMenu();
            //}            
        }
    }
}
