using UnityEngine;

public class DoorTriggerReceiverDual : DualTriggerReceiver
{
    [SerializeField] protected Collider2D _doorCollider;
    [SerializeField] protected Animator _animController;
    [SerializeField] protected bool _reverseTrigger;
    [SerializeField] protected SpriteRenderer _spriteRender;

    protected override void ReceiveTrigger(bool triggered1, bool triggered2)
    {
        if (_doorCollider != null)
        {
            bool triggered = triggered1 && triggered2;
            _doorCollider.enabled = _reverseTrigger ? !triggered : triggered;
            _spriteRender.enabled = _reverseTrigger ? !triggered : triggered;
            if (_animController != null)
            {
                _animController.SetBool("isEnabled", _doorCollider.enabled);
            }
        }
    }
}
