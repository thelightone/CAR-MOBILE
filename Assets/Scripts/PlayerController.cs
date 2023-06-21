using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _eff1;
    [SerializeField]
    private GameObject _eff2;

    private float _speed = 10f;
    private float _TurnInput;
    private float _ForwardInput;
    private Vector3 _CoM;

    protected Rigidbody _r;

    void Start()
    {
        _r = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // настройка центра тяжести для реалистичного поведения при поворотах
        _CoM.z = _TurnInput / 3;
        _CoM.y = 0;
        _CoM.x = 0;
        _r.centerOfMass = _CoM;
        _r.WakeUp();

        if (transform.position.y > 10)
            return;

        transform.Translate(Vector3.right * Time.deltaTime * _speed * _ForwardInput);
        transform.Rotate(transform.eulerAngles * _TurnInput * Time.deltaTime / 5);

        Effects();
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
            _eff1.SetActive(true);
            _eff2.SetActive(true);
        }
        else
        {
            _eff1.SetActive(false);
            _eff2.SetActive(false);
        }
    }
}
