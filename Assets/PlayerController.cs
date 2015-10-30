using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float MoveSpeed;
    
    public float JumpStrenght;
    private Rigidbody R;
	// Use this for initialization
	void Start () {
        R = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Jump() {
        //R.AddForce(new Vector3(0f, JumpStrenght, 0f));
        R.velocity = new Vector3(R.velocity.x, JumpStrenght, R.velocity.z);
    }
	void Update () {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            MovePlayer(moveHorizontal, moveVertical);
        }

        float jump = Input.GetAxis("Jump");
        if (jump != 0 && Mathf.Abs( R.velocity.y)<0.1 ) {
            Jump();
        }

             	
	}


    void MovePlayer(float moveHorizontal, float moveVertical) {


        Vector3 movement = new Vector3(moveHorizontal, R.velocity.y, moveVertical);
        //R.velocity = movement * MoveSpeed;
        float xSpeed = moveHorizontal * MoveSpeed;
        float zSpeed = moveVertical * MoveSpeed;


        R.velocity = new Vector3(xSpeed, R.velocity.y, zSpeed);



    }

    void FixedUpdate() {
       
       
    }
}
