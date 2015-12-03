using UnityEngine;
using System.Collections;

public class DisappearingManager : MonoBehaviour {
    private GameObject[] _planesToDisappear;
    private MeshRenderer[] _planesRenderers;
    private float _planeTimer;
    private int index;
    private bool[] _visibilitySelector;
    public float _startingResetBlinkingTimer=0.5f;
    public float _resetDisappearIntervalTimer = 2f;
    private float _disappearIntervalTimer;
    public int _blinkingIterations = 3;
    private int _remainingBlinks;
    private float _blinkingTimer;
    private float _lerpBlinkingRatio = 0;
    public float _resetDisappearTimer=5.0f;
    private bool _startBlinking=false;
	// Use this for initialization
	void Awake () {
        _planesToDisappear = GameObject.FindGameObjectsWithTag("Tower");
        _visibilitySelector = new bool[_planesToDisappear.Length];
        _planesRenderers=new MeshRenderer[_planesToDisappear.Length];
        _planeTimer = _resetDisappearTimer;
        _disappearIntervalTimer = _resetDisappearIntervalTimer;
        for (int i = 0; i < _visibilitySelector.Length; i++)
        {
            _visibilitySelector[i] = true;
            _planesRenderers[i] = _planesToDisappear[i].GetComponent<MeshRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("BlinkingTimer:" + _blinkingTimer + " RemainingIterations:" + _remainingBlinks + " BlinkingRatio:" + _lerpBlinkingRatio);
        

               

                
        bool allVisible = true;
       
        for (int i = 0; i < _visibilitySelector.Length; i++)
            if (!_visibilitySelector[i])
                allVisible = false;
        if (allVisible)
        {
            _disappearIntervalTimer -= Time.deltaTime;
            if (_disappearIntervalTimer <= 0)
            {
                index = Random.Range(0, _visibilitySelector.Length - 1);
                _visibilitySelector[index] = false;
                _startBlinking = true;
            }
            else
                _startBlinking = false;
        }
        if(_startBlinking)
        {
            //If the plane is active, reduce the blinkingTimer
            if (_planesToDisappear[index].active)
                if (_blinkingTimer > 0)
                {
                    _blinkingTimer -= Time.deltaTime;
                    _planesRenderers[index].enabled = true;
                }
                else
                {
                    _blinkingTimer = Mathf.Lerp(_startingResetBlinkingTimer, 0, _lerpBlinkingRatio);
                    _planesRenderers[index].enabled = false;
                }
            //If the blinkingTimer is minus or equal 0, there are no other blinks to execute then disable the plane
            if(_lerpBlinkingRatio>=1&&_blinkingTimer<=0&&_remainingBlinks==0)
            {
                _planesToDisappear[index].active = false;
                _planeTimer -= Time.deltaTime;
                if(_planeTimer<=0)
                {
                    _lerpBlinkingRatio = 0;
                    _planesToDisappear[index].active = true;
                    _planesRenderers[index].enabled = true;
                    _visibilitySelector[index] = true;
                    _planeTimer = _resetDisappearTimer;
                    _disappearIntervalTimer = _resetDisappearIntervalTimer;
                }
            }else
                if(_blinkingTimer<=0)
                if (_remainingBlinks > 0)
                    _remainingBlinks--;
            else
                {
                    _remainingBlinks = _blinkingIterations;
                    _lerpBlinkingRatio += 0.1f;
                }
                
        }
    }
}
