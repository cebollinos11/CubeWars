using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour {

    public float speed = 2.0f;
    Text text;

    void Start() {

        text = GetComponent<Text>();     
    }

    void  Update()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Round(Mathf.PingPong(Time.time * speed, 1.0f)));
        
    }
}
