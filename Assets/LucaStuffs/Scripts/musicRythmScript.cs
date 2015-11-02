using UnityEngine;
using System.Collections;

public class musicRythmScript : MonoBehaviour {

    int qSamples = 1024;
    float refValue = 0.1f;
    float rmsValue;
    float dbValue;
    float volume = 2;

    public float treshold;

    private float[] samples;
    private GameObject target;
	// Use this for initialization
	void Start () {
        samples = new float[qSamples];
        target = GameObject.FindGameObjectWithTag("GamePlane");
	}
	
    void getVolume()
    {
        GetComponent<AudioSource>().GetOutputData(samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        if (dbValue < -160) dbValue = -160; // clamp it to -160dB min
    }
	// Update is called once per frame
	void Update () {
        getVolume();
        Debug.Log(volume * rmsValue);
        if (volume * rmsValue > treshold)
        {
            target.GetComponent<MeshRenderer>().enabled = false;
            target.GetComponent<Collider>().enabled = false;
        }

        else
        {
            target.GetComponent<MeshRenderer>().enabled = true;
            target.GetComponent<Collider>().enabled = true;
        }
           
        //target.localScale.Set(target.localScale.x,volume * rmsValue,target.localScale.z);
    }
}


