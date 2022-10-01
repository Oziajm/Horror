using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    [Space(10)]
    [Header("Lights")]
    [Space(10)]
    [SerializeField] private Light spotlight;
    [SerializeField] private Renderer[] eyes;
    [SerializeField] private Light[] lights;

    [Space(10)]
    [Header("Mechanics")]
    [Space(10)]
    [SerializeField] private TextMeshProUGUI night;
    [SerializeField] private Transform[] heads;
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
        StartCoroutine(Transition("Night1"));
    }

    public void LoadGame()
    {
        string nextNight = "Night" + nightNumber;
        StartCoroutine(Transition(nextNight));
    }

    IEnumerator Transition(string nextScene)
    {
        float time = 2f;
        bool eyesGlowing = false;
        while (time >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            spotlight.intensity -= 0.5f;
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
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene(nextScene);
    }
}
