using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Animatronic Settings", fileName = "Animatronic Settings")]
public class AnimatronicSettings : ScriptableObject
{
    public float MovementSpeed = 1.5f;
    public float FootStepDelay = 1f;
    public float StunDuration = 3f;
    public float AngerDuration = 10f;
    public float AngerMultiplier = 2f;
    public float RunningMultiplier = 2.5f;
    public float IdleDuration = 14f;
    public float StartUpDuration = 1f;
}
