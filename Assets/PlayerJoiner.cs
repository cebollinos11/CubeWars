using UnityEngine;
using System.Collections;

public class PlayerJoiner : MonoBehaviour {

    public int PlayerID;
    public GameObject particle;
    bool begin;
    public Material playerSelectedMaterial;

    Vector3 originalPos;
	// Use this for initialization
	void Start () {

        Invoke("Begin", 0.1f);
        originalPos = transform.localPosition;
	
	}

    void Begin() {

        if (GameManager.Instance.Players[PlayerID - 1].Active)
        {
            GameManager.Instance.Players[PlayerID - 1].Active = false;
            Join();
        }

        begin = true;
    
    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (begin == false) return;

        if (Input.GetButtonDown("JumpP" + PlayerID.ToString()))
        {
            Join();
        }


        transform.localPosition = originalPos;
        if (Input.GetButton("JumpP" + PlayerID.ToString()))
        {
            transform.localPosition = originalPos + Vector3.up;
        }

	}

    void Join() {
        if (GameManager.Instance.Players[PlayerID - 1].Active) {
            AudioManager.PlayClip(AudioClipsType.Clash);
            return;
        }
        Debug.Log(PlayerID + " joins!");
        Color color = GameManager.Instance.PlayerColors[PlayerID - 1];
        GetComponent<Renderer>().material = playerSelectedMaterial;
        GetComponent<Renderer>().material.color = color;

        AudioManager.PlayClip(AudioClipsType.Clash);
        GameManager.Instance.Players[PlayerID - 1].Active = true;
        Instantiate(particle, transform.position+Vector3.up, transform.rotation);

    }
}
