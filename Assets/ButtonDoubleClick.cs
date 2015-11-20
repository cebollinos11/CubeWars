using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonDoubleClick : MonoBehaviour {




    
    Button buttonMe;
    // Use this for initialization
    void Start()
    {
        buttonMe = GetComponent<Button>();
    }

    void Update()
    {
        if (Input.GetButton("Start"))
        {
            buttonMe.onClick.Invoke();
        }


    }
}
