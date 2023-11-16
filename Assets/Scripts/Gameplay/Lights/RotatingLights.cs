using System.Collections;
using UnityEngine;

public class RotatingLights : MonoBehaviour
{
    #region Variables

    #endregion

    #region Unity Methods

    private void Update()
    {
        transform.Rotate(new Vector3(2, 0, 0));
    }

    #endregion
}
