using UnityEngine;
using System.Collections;

public class DisappearingManagerV1 : MonoBehaviour {
    private GameObject[] _planesToDisappear;
    private MeshRenderer[] _planesRenderers;
    
    private int index;
    private bool[] _visibilitySelector;

    public float _resetPlaneTimer = 5f;
    private float _planeTimer;

    public float _blinkResetTimer = 0.2f;
    private float _blinkTimer;

    public float _resetPlaneDisappearInterval = 2f;
    private float _timerPlaneDisappearInterval;

    public float _totalBlinkResetTimer = 3f;
    private float _totalBlinkTimer;
    // Use this for initialization
    void Awake () {
        _planesToDisappear = GameObject.FindGameObjectsWithTag("Tower");
        _visibilitySelector = new bool[_planesToDisappear.Length];
        _planesRenderers = new MeshRenderer[_planesToDisappear.Length];
        _planeTimer = _resetPlaneTimer;
        _blinkTimer = _blinkResetTimer;
        _timerPlaneDisappearInterval = _resetPlaneDisappearInterval;
        _totalBlinkTimer = _totalBlinkResetTimer;
        for (int i = 0; i < _visibilitySelector.Length; i++)
        {
            _visibilitySelector[i] = true;
            _planesRenderers[i] = _planesToDisappear[i].GetComponent<MeshRenderer>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        bool allVisible = true;
        for (int i = 0; i < _visibilitySelector.Length; i++)
            if (!_visibilitySelector[i])
                allVisible = false;
        if (allVisible)
        {
            index = Random.Range(0, 1000)%_visibilitySelector.Length;

            _visibilitySelector[index] = false;
        }
        if (_timerPlaneDisappearInterval <= 0)
        {
           
            if(_totalBlinkTimer<=0)
            {
                _planesToDisappear[index].SetActive(false);
                _planeTimer -= Time.deltaTime;
                if(_planeTimer<=0)
                {
                    _planesToDisappear[index].SetActive(true);
                    _planesRenderers[index].enabled = true;
                    _totalBlinkTimer = _totalBlinkResetTimer;
                    _visibilitySelector[index] = true;
                    _planeTimer = _resetPlaneTimer;
                    _timerPlaneDisappearInterval = _resetPlaneDisappearInterval;
                }
            }
            else
            {
                Blink();
                _totalBlinkTimer -= Time.deltaTime;
            }
            
        }
        else
            _timerPlaneDisappearInterval -= Time.deltaTime;
    }

    private void Blink()
    {
        _blinkTimer -= Time.deltaTime;
        if(_blinkTimer<=0)
        {
            _planesRenderers[index].enabled = !_planesRenderers[index].enabled;
            _blinkTimer = _blinkResetTimer;
        }
    }
}
