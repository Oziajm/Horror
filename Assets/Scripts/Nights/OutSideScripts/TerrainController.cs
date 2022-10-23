using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    [SerializeField] private IsPlayerOutsideChecker isPlayerOutsideChecker;

    [Space(10)]
    [SerializeField] private GameObject terrain;

    private void Update()
    {
        if(!isPlayerOutsideChecker.IsOutside)
            terrain.SetActive(doors[0].IsOpen || doors[1].IsOpen);
    }
}
