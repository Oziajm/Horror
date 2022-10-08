using UnityEngine;
using System.Collections;

public class PlayerPoseController : StaminaController
{
    [SerializeField] protected CharacterController characterController;

    public void StartCrouching()
    {
        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.LeftShift))
        {
            if (duringCrouchAnimation) StopCoroutine(DoCrouchStand());
            isCrouching = !isCrouching;
            StartCoroutine(DoCrouchStand());
        }
    }

    public IEnumerator DoCrouchStand()
    {
        duringCrouchAnimation = true;
        float time = 0f;
        float startHeight = characterController.height;
        float targetHeight = isCrouching ? standHeight - 1f : standHeight;
        float targetCenter = isCrouching ? 0.2f : 0f;

        while (time <= timeToCrouch)
        {
            characterController.height = Mathf.Lerp(startHeight, targetHeight, time / timeToCrouch);
            time += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = new(0f, targetCenter, 0f);

        duringCrouchAnimation = false;
    }
}
