using UnityEngine;

public class DoorTriggerReceiver : TriggerReceiver
{
    [SerializeField] protected Collider2D _doorCollider;
    [SerializeField] protected Animator _animController;
    [SerializeField] protected bool _reverseTrigger;
    [SerializeField] protected SpriteRenderer _spriteRender;

    protected override void ReceiveTrigger(bool triggered)
    {
        if (_doorCollider != null)
        {
            _doorCollider.enabled = _reverseTrigger ? !triggered : triggered;
            _spriteRender.enabled = _reverseTrigger ? !triggered : triggered;
            if (_animController != null)
            {
                _animController.SetBool("isEnabled", _doorCollider.enabled);
            }
        }
    }
}
