using UnityEngine;

public class FoxyParameters : MonoBehaviour
{
    [field:SerializeField]
    public float StunDuration { get; private set; }
    [field: SerializeField]
    public float AngerDuration { get; private set; }
    [field: SerializeField]
    public float AngerMultiplier { get; private set; }
}
