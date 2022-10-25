using UnityEngine;
using UnityEngine.AI;

public class PlayerLookingAtEndosController : PlayerLookingAtAnimatronics
{
    [SerializeField] private GameObject[] endos;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        foreach(var endo in endos)
        {
            if (IsVisible(cam, endo))
            {
                endo.GetComponent<Animator>().enabled = false;
                endo.GetComponent<NavMeshAgent>().speed = 0f;
                endo.GetComponent<StateMachine>().enabled = false;
            }
            else
            {
                endo.GetComponent<Animator>().enabled = true;
                endo.GetComponent<NavMeshAgent>().speed = 0.3f;
                endo.GetComponent<StateMachine>().enabled = true;
            }
        }
    }
}
