using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] private Slider time;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            time.value -= 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            time.value += 1;
        }
        Time.timeScale = time.value;
    }
}
