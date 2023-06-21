using UnityEngine;

public class Wheel : MonoBehaviour
{
    private float x;
    void Update()
    {
        if (x != 0)
            transform.Rotate(x, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void WheelTurn(float gradus)
    {
        x = gradus;
    }
}