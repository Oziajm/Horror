using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    #region Variables

    private const float OPENING_DURATION = 0.1f;

    [Header("Door Rotations")]
    [Space(10)]
    [SerializeField]
    private Quaternion openedRotationNorth;
    [SerializeField]
    private Quaternion openedRotationSouth;

    private Quaternion closedRotation;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        closedRotation = this.transform.localRotation;
    }

    #endregion

    #region Public Methods

    public async void OpenDoors(bool isNorth)
    {
        if (isNorth)
        {
            await RotateDoors(openedRotationNorth);
        }
        else
        {
            await RotateDoors(openedRotationSouth);
        }
    }

    public async void CloseDoors()
    {
        await RotateDoors(closedRotation);
    }

    #endregion

    #region Private Methods

    private async Task RotateDoors(Quaternion targetRotation)
    {
        float end = Time.time + OPENING_DURATION;

        while (Time.time < end)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * 2f / OPENING_DURATION);
            await Task.Yield();
        }
    }

    #endregion
}
