using System;
using UnityEngine;

public class FrozenState : BaseState
{
    private readonly Endo endo;
    public FrozenState(Endo animatronic) : base(animatronic.gameObject)
    {
        this.endo = animatronic;
    }

    public override Type Tick()
    {
        FreezeEndo();

        return null;
    }

    private void FreezeEndo()
    {
        endo.animator.enabled = !endo.IsVisible(endo.gameObject);
        endo.navMeshAgent.speed = !endo.IsVisible(endo.gameObject) ? 0f : 0.3f;
    }
}
