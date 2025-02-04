using System.Collections;
using UnityEngine;

public class RotatingLights : MonoBehaviour
{
    #region Variables

    private readonly float ROTATION_SPEED = 200f;

    private Vector3 rotationVec;

    #endregion

    #region Unity Methods

    private void Start()
    {
        rotationVec = transform.up * ROTATION_SPEED;
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(rotationVec * Time.deltaTime);
    }

    #endregion
}
