using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public InputDevice Device { get; set; }
    public float speed = 2f;
    public Vector3 movement;
    private Animator anim;
    public Vector3 lastMove;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Device.LeftStick.X, Device.LeftStick.Y);
        anim.SetFloat("MoveX", Device.LeftStick.X);
        anim.SetFloat("MoveY", Device.LeftStick.Y);
        if(movement != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
            lastMove = new Vector3(Device.LeftStick.X, Device.LeftStick.Y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        transform.position += movement * Time.deltaTime * speed;
        
        if (Device.Action1.WasPressed)
        {
            Debug.Log("Button 1 was pressed!");
        }
    }
}
