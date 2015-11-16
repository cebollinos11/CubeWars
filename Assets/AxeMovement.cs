using UnityEngine;
using System.Collections;

public class AxeMovement : MonoBehaviour {
    public enum OscillationType
    {
        LeftToRight,
        RightToLeft
    }
    public OscillationType oscillationType;
    private float _forceApplied=0;
	
	// Update is called once per frame
	void FixedUpdate () {
        _forceApplied += Time.fixedDeltaTime;
        if(oscillationType==OscillationType.RightToLeft)
        transform.Rotate(new Vector3(0,0,Mathf.Sin(_forceApplied)));
        else
            if (oscillationType == OscillationType.LeftToRight)
            transform.Rotate(new Vector3(0, 0, -Mathf.Sin(_forceApplied)));
    }
}
