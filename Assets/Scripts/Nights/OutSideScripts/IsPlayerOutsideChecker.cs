using UnityEngine;

public class IsPlayerOutsideChecker : MonoBehaviour
{
    private bool isOutside = false;

    public bool IsOutside => isOutside;

    private void OnTriggerEnter(Collider other)
    {
        isOutside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOutside = false;
    }
}
