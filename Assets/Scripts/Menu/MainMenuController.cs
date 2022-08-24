using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private TextMeshProUGUI night;
    [SerializeField] private Renderer[] eyes = new Renderer[3];
    [SerializeField] private Light[] lights = new Light[9];
    [SerializeField] private Transform[] heads ;
    [SerializeField] private Transform cameraTransform;
    private int nightNumber = 1;

    private void Start()
    {
        night.text = "Night " + nightNumber;
        foreach (Renderer obj in eyes)
        {
            obj.material.SetColor("_EmissionColor", obj.material.color * 0.0f);
        }
    }

    public void NewGame()
    {
        nightNumber = 1;
        StartCoroutine(transition("Night1"));
    }

    public void LoadGame()
    {
        string nextNight = "Night" + nightNumber;
        SceneManager.LoadScene(nextNight);
    }

    IEnumerator transition(string nextScene)
    {
        float time = 2f;
        bool eyesGlowing = false;
        while (time >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            light.intensity -= 0.3f;
            time -= 0.1f;

            if (time <= 0.5f && !eyesGlowing)
            {
                foreach (Renderer obj in eyes)
                {
                    obj.material.SetColor("_EmissionColor", obj.material.color * 2.0f);
                }
                foreach(Light light in lights)
                {
                    light.enabled = true;
                }
                foreach (Transform head in heads)
                {
                    head.LookAt(cameraTransform);
                }
            }
        }
        SceneManager.LoadScene(nextScene);
    }
}
