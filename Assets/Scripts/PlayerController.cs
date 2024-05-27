
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _eff1;
    [SerializeField]
    private ParticleSystem _eff2;
    private float _speed;
    public float speed { get { return _speed; }  set { if (value > 0) _speed = value; else _speed = 0; } }
    public float _TurnInput;
    private float _ForwardInput;
    private Vector3 _CoM;

    private float _maxSpeed = 120f;
    private Quaternion _rotation;
    private Quaternion _rotation2;

    private float _elapsedTime = 0;
    private float startZ;

    protected Rigidbody _r;

    public static PlayerController instance;

    private int health = 5;
    [SerializeField] private Slider hpslider;

    [SerializeField]
    private ParticleSystem _dam;
    [SerializeField]
    private ParticleSystem _deathEff;
    private bool dead;

    [SerializeField]
    private GameObject _loseScreen;

    private Vector3 startPos;
    private Quaternion startRot;

    //TURBO
    private bool turboUse;
    [SerializeField] private Slider turboSlider;
    [SerializeField] private ParticleSystem turboPs;
    private float turboCharge=0;
    private float acc = 1;

    private bool damaged;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;

        instance = this;
        startZ = transform.position.z;
        _r = GetComponent<Rigidbody>();
        _rotation = transform.rotation;
        acc = 1;
    }

    private void OnDisable()

    {
        speed = 0;
        _CoM.z = _TurnInput / 3;
        _CoM.y = 0;
        _CoM.x = 0;
        _r.centerOfMass = _CoM;
        _r.WakeUp();
    }

    void Update()
    {
        if (!dead)
        {
            var newPos = transform.position;
            transform.position = new Vector3(newPos.x, newPos.y, startZ);

            // настройка центра тяжести для реалистичного поведения при поворотах

            if (transform.position.y > 10)
                return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                _ForwardInput = 1;
                AudioManager.Instance.OnStart();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                _ForwardInput = 0;
                AudioManager.Instance.OnStop();
            }
            if (speed > 0 && _ForwardInput == 0)
            {
                speed -= 2f;
            }
            if (speed < _maxSpeed && _ForwardInput == 1)
            {
                speed += acc;
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                _TurnInput = 0;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _TurnInput = -1;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _TurnInput = 1;

            }

            if (Input.GetKeyDown(KeyCode.Space) && !turboUse)
            {
                acc = 2;
                _maxSpeed = 170;
                turboUse = true;
                turboPs.Play();
            }

            if (Input.GetKeyUp(KeyCode.Space) || (turboUse && turboCharge<1))
            {
                acc = 1;
                _maxSpeed = 120;
                turboUse = false;
                turboPs.Stop();
            }

            if (!turboUse)
            {
                turboCharge += 0.5f;
                if (speed>120)
                {
                    _speed -= 3;
                }
            }
            else if (turboUse)
            {
                turboCharge -= 2;
            }

            turboSlider.value = turboCharge;

            // transform.Translate(Vector3.right * Time.deltaTime * _speed);
            if (_TurnInput != 0 && speed>0)
            {
                speed -= acc;
                _elapsedTime = 0;
                _rotation2 = transform.rotation;

                _r.AddForce(Vector3.right * 30 * _TurnInput, ForceMode.Acceleration);

                //transform.Translate(Vector3.back * Time.deltaTime * _speed*_TurnInput/2);

                var rot = transform.rotation.eulerAngles;
              
                if (rot.y > 240 && rot.y < 300)
                {
                    transform.Rotate(transform.eulerAngles * _TurnInput * Time.deltaTime / 300 * speed);
                }
            }
            else
            {
                _elapsedTime += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(_rotation2, _rotation, _elapsedTime / 0.25f);
            }

            Effects();
        }
    }
    public void GasMobile(float gas)
    {
        _ForwardInput = gas;
    }

    public void TurnMobile(float turn)
    {
        if (_ForwardInput != 0)
            _TurnInput = turn;
    }

    private void Effects()
    {
        if (_ForwardInput > 0)
        {
            _eff1.Play();
            _eff2.Play();
        }
        else
        {
            _eff1.Stop() ;
            _eff2.Stop() ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !damaged)
        {
            damaged = true;
            speed -= 50;
            StartCoroutine(Damaged());
            health--;
            hpslider.value = health;
            _dam.Play();
            AudioManager.Instance.OnHit();
            if(health==0)
            {
                Death();
            }
        }
    }

    private IEnumerator Damaged()
    {
        yield return new WaitForSeconds(1);
        damaged = false;
    }

    public void Death()
    {
        _eff1.Stop();
        _eff2.Stop();

        _TurnInput = 0;
        _ForwardInput = 0;

        _r.velocity = Vector3.zero;
        _r.angularVelocity = Vector3.zero;

        dead = true;
        _deathEff.Play();
        speed = 0;
        AudioManager.Instance.OnStop();
        _loseScreen.SetActive(true);
        
        Environmentcontroller.instance.Finish();
    }

    public void Revive()
    {
        _loseScreen.SetActive(false);
        damaged = true;
        SpawnFlow.Instance.Clear();
        transform.position = startPos;
        transform.rotation = startRot;
        health = 3;
        dead = false;
        StartCoroutine(Damaged());


    }
}
