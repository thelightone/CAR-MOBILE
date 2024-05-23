using UnityEngine;

public class Wheel : MonoBehaviour
{
    private float x;
    void Update()
    {
        x = PlayerController.instance._TurnInput;
        var rot = transform.rotation.eulerAngles;
       // if (rot.z >-30 && rot.z < 30)
        transform.rotation = Quaternion.Euler(rot.x, -90+x*55, rot.z);
        //else
           // transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void WheelTurn(float gradus)
    {
        x = -gradus;
    }
}