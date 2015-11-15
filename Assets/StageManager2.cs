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

        AudioManager.PlayBgSong();
        Debug.Log("Spawning chars");
        SpawnCharacters();
        
    }


    public void SpawnCharacters()
    {
        
        var i = 0;

        Player winPlayer = GameManager.Instance.GetWinningPlayer();

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

                playerControllerV1 pc = p.GetComponent<playerControllerV1>();
                pc.horizontalAxisName = "HorizontalP" + (i + 1).ToString();
                pc.verticalAxisName = "VerticalP" + (i + 1).ToString();
                pc.jumpKey = "JumpP" + (i + 1).ToString();
                pc.dashKey = "DashP" + (i + 1).ToString();

                //asign to game manager
                GameManager.Instance.Players[i].playerObject = p;
                p.GetComponent<PointsManager>().i = i;

                //respawner
                p.GetComponent<Respawner>().originalPos = SpawnPoint.position;

                if(winPlayer!= null)                
                    if (winPlayer == GameManager.Instance.Players[i])
                    {
                        p.transform.FindChild("Crown").gameObject.SetActive(true);
                    }

                //set trail renderer color
                TrailRenderer tR = p.GetComponent<TrailRenderer>();
                tR.material.SetColor("_Color",GameManager.Instance.Players[i].Color);


                

                var so = new UnityEditor.SerializedObject(tR);

                Color colorToSet = GameManager.Instance.Players[i].Color;

                colorToSet.a = 1f;


                so.FindProperty("m_Colors.m_Color[0]").colorValue = colorToSet;
                so.FindProperty("m_Colors.m_Color[1]").colorValue = colorToSet;
                so.FindProperty("m_Colors.m_Color[2]").colorValue = colorToSet;
                so.FindProperty("m_Colors.m_Color[3]").colorValue = colorToSet;
                so.FindProperty("m_Colors.m_Color[4]").colorValue = colorToSet;

                so.ApplyModifiedProperties();

               
                
            }
            else { Debug.Log("not active"); }
            
            i++;


        }

    }
}
