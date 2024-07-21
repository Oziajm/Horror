using System.Collections;
using UnityEngine;

public class RotatingLights : MonoBehaviour
{
    #region Variables

    #endregion

    #region Unity Methods

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(2f, 0, 0));
    }

    #endregion
}
