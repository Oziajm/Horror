using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI night;
    private int nightNumber;

    private void Start()
    {
        night.text = "Night " + nightNumber;
    }

    public void NewGame()
    {
        nightNumber = 1;
        SceneManager.LoadScene("Night1");
    }

    public void LoadGame()
    {
        string nextNight = "Night" + nightNumber;
        SceneManager.LoadScene(nextNight);
    }
}
