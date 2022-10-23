using UnityEngine;

public class IsPlayerOutsideChecker : MonoBehaviour
{
    [SerializeField] private bool isOutside;

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
