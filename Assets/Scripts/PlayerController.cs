
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

    public float _speed = 0;
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

    void Start()
    {
        instance = this;
        startZ = transform.position.z;
        _r = GetComponent<Rigidbody>();
        _rotation = transform.rotation;
    }

    private void OnDisable()

    {
        _speed = 0;
    }

    void Update()
    {
        if (!dead)
        {
            var newPos = transform.position;
            transform.position = new Vector3(newPos.x, newPos.y, startZ);

            // настройка центра тяжести для реалистичного поведения при поворотах
            _CoM.z = _TurnInput / 3;
            _CoM.y = 0;
            _CoM.x = 0;
            _r.centerOfMass = _CoM;
            _r.WakeUp();

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
            if (_speed > 0 && _ForwardInput == 0)
            {
                _speed -= 1f;
            }
            if (_speed < _maxSpeed && _ForwardInput == 1)
            {
                _speed += 1f;
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




            // transform.Translate(Vector3.right * Time.deltaTime * _speed);
            if (_TurnInput != 0)
            {
                _elapsedTime = 0;
                _rotation2 = transform.rotation;

                _r.AddForce(Vector3.right * 30 * _TurnInput);

                //transform.Translate(Vector3.back * Time.deltaTime * _speed*_TurnInput/2);

                var rot = transform.rotation.eulerAngles;
                Debug.Log(rot);
                if (rot.y > 240 && rot.y < 300)
                {
                    transform.Rotate(transform.eulerAngles * _TurnInput * Time.deltaTime / 300 * _speed);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
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
    private void Death()
    {
        _eff1.Stop();
        _eff2.Stop();

        _ForwardInput = 0;

        dead = true;
        _deathEff.Play();
        _speed = 0;

        _loseScreen.SetActive(true);
        
    }
}
