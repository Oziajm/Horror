using System.Collections;
using UnityEngine;

public class RotatingLights : MonoBehaviour
{
    #region Variables

    private readonly float ROTATION_SPEED = 100f;

    private Vector3 rotationVec;

    #endregion

    #region Unity Methods

    private void Start()
    {
        rotationVec = transform.forward * ROTATION_SPEED;
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(rotationVec * Time.deltaTime);
    }

    #endregion
}
