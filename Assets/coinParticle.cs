using UnityEngine;
using System.Collections;

public class coinParticle : MonoBehaviour {

    [SerializeField]
    float lifeTime = 1f;

	// Use this for initialization
	void Start () {

        Invoke("DestroySelf", lifeTime);
	
	}

    public void SetColor(Color color) {
        GetComponent<ParticleSystem>().startColor = new Color( color.r,color.g,color.b,1f);    
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DestroySelf() {

        GameObject.Destroy(this.gameObject);
    
    }
}
