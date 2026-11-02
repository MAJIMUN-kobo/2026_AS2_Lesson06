using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector3.forward * 7.5f * Time.deltaTime);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector3.back * 7.5f * Time.deltaTime);
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Rotate(Vector3.up * -180f * Time.deltaTime);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.Rotate(Vector3.up * 180f * Time.deltaTime);
        }
    }
}
