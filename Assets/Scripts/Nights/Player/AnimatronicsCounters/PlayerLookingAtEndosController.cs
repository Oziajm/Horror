using UnityEngine;
using UnityEngine.UI;

public class PlayerLookingAtEndosController : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();

    }

    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        foreach(var target in targets)
        {
            if (IsVisible(cam, target))
            {
                target.GetComponent<Animator>().enabled = false;
                target.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            }
            else
            {
                target.GetComponent<Animator>().enabled = true;
                target.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
        }
    }
}
