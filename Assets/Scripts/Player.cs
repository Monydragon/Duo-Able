using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public InputDevice Device { get; set; }
    public float speed = 2f;
    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Device.LeftStick.X, Device.LeftStick.Y);
        transform.position += movement * Time.deltaTime * speed;
        
        if (Device.Action1.WasPressed)
        {
            Debug.Log("Button 1 was pressed!");
        }
    }
}
