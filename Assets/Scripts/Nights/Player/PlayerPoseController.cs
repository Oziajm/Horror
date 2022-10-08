using UnityEngine;
using System.Collections;

public class PlayerPoseController : MonoBehaviour
{
    [SerializeField] protected CharacterController characterController;
    [SerializeField] private PlayerSettings playerSettings;

    public void StartStopCrouching()
    {
        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (playerSettings.duringCrouchAnimation) StopCoroutine(DoCrouchStand());
            playerSettings.isCrouching = !playerSettings.isCrouching;
            StartCoroutine(DoCrouchStand());
        }
    }

    public void StopCrouching()
    { 
    }

    public IEnumerator DoCrouchStand()
    {
        playerSettings.duringCrouchAnimation = true;
        float time = 0f;
        float startHeight = characterController.height;
        float targetHeight = playerSettings.isCrouching ? playerSettings.standHeight - 1f : playerSettings.standHeight;
        float targetCenter = playerSettings.isCrouching ? 0.2f : 0f;

        while (time <= playerSettings.timeToCrouch)
        {
            characterController.height = Mathf.Lerp(startHeight, targetHeight, time / playerSettings.timeToCrouch);
            time += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = new Vector3(0f, targetCenter, 0f);

        playerSettings.duringCrouchAnimation = false;
    }
}
