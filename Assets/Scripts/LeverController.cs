using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
public class LeverController : MonoBehaviour
{
    public Animator anim;
    public bool enabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player.Device.Action1.WasPressed)
            {
                enabled = !enabled;
                anim.SetBool("isEnabled", enabled);
            }
        }
    }
}
