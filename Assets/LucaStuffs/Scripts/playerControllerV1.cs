using UnityEngine;
using System.Collections;


public class playerControllerV1 : MonoBehaviour
{

    public string verticalAxisName;
    public string horizontalAxisName;
    public string jumpKey;
    public string dashKey;
    public GameObject[] objBodies;
    public float _jumpPower = 6.0f;
    public float _dashResetTimer = 1.5f;
    public float _dashDurationResetTimer = 0.1f;
    public float dashPower = 100.0f;
    public float stunTimerReset = 0.2f;

    private float inversionFlag=1.0f;
    private bool firstTimeTouch = false;
    private ParticleManager _particleManager;
    private float hAxisDash, vAxisDash;
    private float _dashTimer;
    private float _dashDurationTimer;
    private bool _dash = false;
    private bool _isJumping;
    private bool _stunned = false;
    private float _stunTimer;
    private float power = 10.0f;
    private Rigidbody[] bodies;
    private Vector3 _forward;
    private Vector3 _right;
    private float _oldDistanceFromMidpoint;
    private bool _canMove = true;
    private bool dashJumpInverted=false;
    private bool _scale = false;
    private Vector3 _newScale;
    private float _scaleTimer=0f;
    [SerializeField]
    float max_speed;
    // Use this for initialization


    void Awake()
    {
        bodies = new Rigidbody[objBodies.Length];
        for (int i = 0; i < objBodies.Length; i++)
            bodies[i] = objBodies[i].GetComponent<Rigidbody>();
        _dashTimer = _dashResetTimer;
        _dashDurationTimer = _dashDurationResetTimer;
        _particleManager = GetComponent<ParticleManager>();
    }
    void Update()
    {
        if (_scale)
        {
            _scaleTimer += Time.deltaTime / 100;
            transform.localScale = Vector3.Lerp(transform.localScale, _newScale, _scaleTimer);
            if (_scaleTimer >= 1)
            {
                _scale = false;
                _scaleTimer = 0;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (_dashTimer > 0)
            _dashTimer -= Time.fixedDeltaTime;
        float vPower, hPower;
        bool jPower, dPower;
        vPower = Input.GetAxis(verticalAxisName);
        hPower = Input.GetAxis(horizontalAxisName);
        string jKey, dKey;
        if(!dashJumpInverted)
        {
            jKey = jumpKey;
            dKey = dashKey;
        }
        else
        {
            jKey = dashKey;
            dKey = jumpKey;
        }
        jPower = Input.GetButton(jKey);
        dPower = Input.GetButton(dKey);
        if (firstTimeTouch)
        {
            if (dPower && _dashTimer <= 0 && (hPower != 0 || vPower != 0))
            {
                _dash = true;
                hAxisDash = hPower;
                vAxisDash = vPower;
            }

            if (_dash)
            {
                Dash();
                _dashDurationTimer -= Time.fixedDeltaTime;
                if (_dashDurationTimer <= 0)
                {
                    _dash = false;
                    _dashDurationTimer = _dashDurationResetTimer;
                    _dashTimer = _dashResetTimer;
                    _particleManager.stopDashParticle();
                }
            }
            else
            {
                    if (!_stunned)
                    {
                        if (hPower != 0 || vPower != 0 && !_isJumping)
                            MovePlayer(hPower, vPower);
                        if (jPower && !_isJumping)
                            Jump();
                    }
                    else
                    {
                        _stunTimer -= Time.fixedDeltaTime;
                        if (_stunTimer <= 0)
                            _stunned = false;
                    }

            }
        }




        for (int i = 0; i < bodies.Length; i++)
        {
            if (bodies[i].velocity.magnitude > max_speed)
            {
                bodies[i].velocity = bodies[i].velocity.normalized * max_speed;
            }
        }

        if (_stunned)
        {

            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].velocity /= 1.02f;
            }


        }

    }


    void MovePlayer(float hAxis, float vAxis)
    {
        for (int i = 0; i < bodies.Length; i++)
        {
                bodies[i].velocity = new Vector3(hAxis * -power*inversionFlag, bodies[i].velocity.y, vAxis * -power*inversionFlag);
        }

    }
    public void NullVelocity()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            if (!_isJumping)
                bodies[i].velocity = Vector3.zero;
        }
    }


    void Dash()
    {
        _particleManager.playDashParticle(new Vector3(hAxisDash * -dashPower, 0.0f, vAxisDash * -dashPower));
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].velocity = new Vector3(hAxisDash * -dashPower*inversionFlag, 0.0f, vAxisDash * -dashPower*inversionFlag);
        }

    }




    void Jump()
    {
        AudioManager.PlayClip(AudioClipsType.Jump);
        _particleManager.playJumpParticle();
        for (int i = 0; i < bodies.Length; i++)
            bodies[i].velocity = new Vector3(bodies[i].velocity.x, _jumpPower, bodies[i].velocity.z);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "GamePlane")
        {
            if (!firstTimeTouch)
                firstTimeTouch = true;
            if (firstTimeTouch)
            {
                _particleManager.stopJumpParticle();
                _isJumping = false;
            }
        }
        else
            if (col.gameObject.tag == "Player")
        {
            if (firstTimeTouch)
            {
                if (!isDashing() && !col.gameObject.GetComponent<playerControllerV1>().isDashing())
                    _particleManager.playClashParticle(col.contacts[0].point);
                else
                    _particleManager.playClashDashParticle(col.contacts[0].point);
                if (isDashing() && !col.gameObject.GetComponent<playerControllerV1>().isDashing())
                {
                    Vector3 impulse = (col.gameObject.GetComponent<Transform>().position - GetComponent<Transform>().position) * dashPower * 0.0000001f;
                    col.gameObject.GetComponent<playerControllerV1>().ApplyForce(impulse);
                    col.gameObject.GetComponent<playerControllerV1>().StunByDash();
                }
            }
        }

    }
    public bool isDashing()
    {
        return _dash;
    }

    public void StunByDash()
    {
        _stunned = true;
        _stunTimer = stunTimerReset;
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (firstTimeTouch)
            {
                if (isDashing() && !col.gameObject.GetComponent<playerControllerV1>().isDashing())
                {
                    Vector3 impulse = (col.gameObject.GetComponent<Transform>().position - GetComponent<Transform>().position) * dashPower * 0.0000001f;
                    col.gameObject.GetComponent<playerControllerV1>().ApplyForce(impulse);
                    col.gameObject.GetComponent<playerControllerV1>().StunByDash();
                }
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (firstTimeTouch)
        {
            if (col.gameObject.tag == "GamePlane" )
            {

                _isJumping = true;
            }




            //Debug.Log("ASDASDSA is jumping: " + _isJumping + " /// _canmove: " + _canMove);

        }

    }
    public void ApplyForce(Vector3 force)
    {
        for (int i = 0; i < bodies.Length; i++)
            bodies[i].velocity += force;
    }

    public void InvertControls()
    {
        inversionFlag *= -1;
    }

    public void InvertDashJump()
    {
        dashJumpInverted = !dashJumpInverted;
    }
    
    public void ScaleIt(float scale)
    {
        _scale = true;
        _newScale = new Vector3(scale, scale, scale);
        GetComponent<Transform>().localScale =new Vector3(scale,scale, scale);
    }
}