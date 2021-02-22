using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
public class LeverController : TriggerTransmitter
{
    public Animator anim;
    private Player player;
    [SerializeField] AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.Device.Action1.WasPressed)
            {
                Debug.Log("Triggering Button");
                Trigger();
                AudioManager.instance.PlaySFX(sfx);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        Player player = collision.GetComponent<Player>();
    //        if (player.Device.Action1.WasPressed)
    //        {
    //            Debug.Log("Triggering button");
    //            Trigger();
    //        }
    //    }
    //}

    public override void Trigger()
    {
        base.Trigger();
        anim.SetBool("isEnabled", Triggered);
    }
}
