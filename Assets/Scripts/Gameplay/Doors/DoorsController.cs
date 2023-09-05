using System.Collections;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private DoorController[] doorControllers; 

    #endregion

    #region Public Methods
    
    public void OpenDoors(bool isNorth)
    {
        for (int i = 0; i < doorControllers.Length; i++)
        {
            doorControllers[i].OpenDoors(isNorth);
        }
    }

    public void CloseDoors()
    {
        for (int i = 0; i < doorControllers.Length; i++)
        {
            doorControllers[i].CloseDoors();
        }
    }

    #endregion
}