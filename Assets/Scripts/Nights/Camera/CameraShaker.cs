using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    #region Variables

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraSettings cameraSettings;

    private Vector3 defaultCameraLocalPos;
    private float cameraShakeTimer;
    private float GetCameraShakeFrequency => playerMovement.IsCrouching ? cameraSettings.crouchingFrequency : playerMovement.IsSprinting ? cameraSettings.sprintingFrequency : cameraSettings.walkingFrequency;

    #endregion

    #region Unity Methods

    private void Start()
    {
        defaultCameraLocalPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        CameraShake();
    }

    #endregion

    #region Private Methods

    private void CameraShake()
    {
        if (playerMovement.IsMoving)
        {
            Vector3 newPos = defaultCameraLocalPos;
            newPos.y += cameraSettings.magnitude * Mathf.Sin(cameraShakeTimer * GetCameraShakeFrequency);
            transform.localPosition = newPos;
            cameraShakeTimer += Time.deltaTime;
            return;
        }

        Vector3.MoveTowards(transform.localPosition, defaultCameraLocalPos, 1f);
        cameraShakeTimer = 0;
    }

    #endregion
}
