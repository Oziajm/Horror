using System;
using UnityEngine;

public class FoxyFlashedState : BaseState
{
    private readonly Foxy foxy;

    public FoxyFlashedState(Foxy animatronic) : base(animatronic.gameObject)
    {
        this.foxy = animatronic;
    }

    public override Type Tick()
    {
        Debug.Log("foxyFlashedState");

        if (foxy.IsVisible(foxy.gameObject) && foxy.IsFlashlightOn && !foxy.isImmuneToFlashlight)
        {
            foxy.navMeshAgent.speed = 0f;
            foxy.isImmuneToFlashlight = true;
            ChangeFoxyEyesColor(Color.red);
            foxy.animator.SetBool("isFlashedOut", true);
        }

        if (foxy.IsVisible(foxy.gameObject) && foxy.IsFlashlightOn && foxy.isImmuneToFlashlight)
        {
            foxy.isTriggered = false;
            foxy.animator.SetBool("isFlashedOut", false);
            foxy.animator.SetBool("isFoxyTriggered", true);
        } else {
            foxy.animator.SetBool("isFoxyTriggered", false);
        }

        if (!foxy.isTriggered && !foxy.IsVisible(foxy.gameObject))
        {
            CalmDownFoxy();
        }
        return null;
    }

    private void ChangeFoxyEyesColor(Color color)
    {
        foreach (var eyeLight in foxy.eyesLights)
        {
            eyeLight.color = color;
        }
        foxy.eyes.color = color;
        foxy.eyes.SetColor("_EmissionColor", color * 1f);
    }

    private void CalmDownFoxy()
    {
        float time = 5f;
        time -= 1f * Time.deltaTime;
        if (time < 0)
        {
            ChangeFoxyEyesColor(Color.white);
        }
    }
}
