using UnityEngine;

/// <summary>
/// Interactable that triggers an animation once activated
/// </summary>
public class AnimatedInteractable : Interactable
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (startsActive) Activate();
    }

    public override void Activate()
    {
        if (IsActive) return;

        _animator.SetTrigger("Interacted");

        IsActive = true;
    }
}
