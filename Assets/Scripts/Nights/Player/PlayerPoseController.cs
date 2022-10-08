using UnityEngine;
using System.Collections;

public class PlayerPoseController : MonoBehaviour
{
    [SerializeField] protected CharacterController characterController;
    [SerializeField] private PlayerSettings playerSettings;

    public bool duringCrouchAnimation { get; private set; }
    public bool isCrouching { get; private set; }

    public void SetCrouch(bool val)
    {
        if (duringCrouchAnimation) StopCoroutine(DoCrouchStand());
        isCrouching = val;
        StartCoroutine(DoCrouchStand());
    }

    public IEnumerator DoCrouchStand()
    {
        duringCrouchAnimation = true;
        float time = 0f;
        float startHeight = characterController.height;
        float targetHeight = isCrouching ? playerSettings.standHeight - 1f : playerSettings.standHeight;
        float targetCenter = isCrouching ? 0.2f : 0f;

        while (time <= playerSettings.timeToCrouch)
        {
            characterController.height = Mathf.Lerp(startHeight, targetHeight, time / playerSettings.timeToCrouch);
            time += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = new Vector3(0f, targetCenter, 0f);

        duringCrouchAnimation = false;
    }
}
