using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Camera Settings", fileName = "Camera Settings")]
public class CameraSettings : ScriptableObject
{
    public float magnitude = 0.05f;
    public float sprintingFrequency = 20f;
    public float walkingFrequency = 10f;
    public float crouchingFrequency = 5f;
    public float mouseSensitivity = 200f;
}
