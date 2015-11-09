using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {
    public GameObject dashEffect;
    public GameObject jumpEffect;
    public GameObject clashEffect;
    public GameObject clashDashEffect;

    private GameObject _dashEffectInstance;
    private GameObject _jumpEffectInstance;
    private GameObject _clashEffectInstance;

    // Use this for initialization
    void Awake () {
        float d=0.5f;

        _dashEffectInstance = (GameObject)GameObject.Instantiate(dashEffect, transform.position+new Vector3(0.0f, 0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        _dashEffectInstance.GetComponent<Transform>().parent = GetComponent<Transform>();
        _dashEffectInstance.SetActive(false);

        _jumpEffectInstance = (GameObject)GameObject.Instantiate(jumpEffect, gameObject.GetComponent<Transform>().position+ new Vector3(0,-d, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        _jumpEffectInstance.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void playDashParticle(Vector3 dashDirection)
    {
            _dashEffectInstance.GetComponent<Transform>().LookAt(-dashDirection);
            _dashEffectInstance.SetActive(true);
        
    }

    public void stopDashParticle()
    {
        Invoke("HideDashParticle", 0.3f);
        
    }

    void HideDashParticle()
    {
            _dashEffectInstance.SetActive(false);
    }

    public void playJumpParticle()
    {
        Vector3 position;
        position =transform.position;
        position.y -=0.4f;
        _jumpEffectInstance.GetComponent<Transform>().position = position;
        _jumpEffectInstance.SetActive(true);
    }

    public void stopJumpParticle()
    {
        _jumpEffectInstance.SetActive(false);
    }

    public void playClashParticle(Vector3 position)
    {
        _clashEffectInstance = (GameObject)GameObject.Instantiate(clashEffect, position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        Invoke("destroyClashParticle", 0.5f);
    }

    public void playClashDashParticle(Vector3 position)
    {
        _clashEffectInstance = (GameObject)GameObject.Instantiate(clashDashEffect, position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        Invoke("destroyClashParticle", 0.5f);
    }

    void destroyClashParticle()
    {
        Destroy(_clashEffectInstance);
    }
}
