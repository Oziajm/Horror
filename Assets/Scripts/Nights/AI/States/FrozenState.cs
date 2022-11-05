using System;

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

        if (!endo.IsVisible(endo.gameObject))
            return typeof(RoamingState);

        return null;
    }

    private void FreezeEndo()
    {
        endo.animator.enabled = !endo.IsVisible(endo.gameObject);
        endo.navMeshAgent.speed = !endo.IsVisible(endo.gameObject) ? 0.3f : 0f;
    }
}
