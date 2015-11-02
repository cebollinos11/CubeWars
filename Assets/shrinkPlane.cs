using UnityEngine;
using System.Collections;

public class shrinkPlane : MonoBehaviour {

    public float howOftenItShrinks;
    public float howMuchItShrinks;
    private float shrinkRatio;
    private float shrinkCountdown;

	// Use this for initialization
	void Start () {
        shrinkRatio = -(gameObject.transform.localScale.x * howMuchItShrinks);
        RestartCountDown();
	}

    void RestartCountDown() {
        shrinkCountdown = howOftenItShrinks;
    }
	// Update is called once per frame
	void Update () {

        shrinkCountdown -= Time.deltaTime;
        if (shrinkCountdown < 0) {
            ShrinkNow();
            RestartCountDown();
        }


        if (Input.GetKeyDown(KeyCode.E)) {
            ShrinkNow();
        }
	}

    void ShrinkNow() {

        if (gameObject.transform.localScale.x + shrinkRatio <= 0)
        {
            return;
        }

        gameObject.transform.localScale += new Vector3(shrinkRatio,0f,shrinkRatio);
    
    }
}
