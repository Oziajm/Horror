using UnityEngine;
using UnityEngine.UI;

public class CamerasController : MonoBehaviour
{
    [Space(10)]
    [Header("Camera Mechanics Values")]
    [Space(10)]
    public int cameraNumber = 10;
    public int rotation = 0;
    public bool isPlayerNearKeyboard;

    [Space(10)]
    [Header("Cameras Screen")]
    [Space(10)]
    [SerializeField] private Image[] activeCamerasImage;
    [SerializeField] private MeshRenderer TVScreen;
    [SerializeField] private Material[] camerasMaterials;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject camerasHud;
    [SerializeField] private Renderer OnOffLight;

    private void Start()
    {
        TurnTVOnOff();
        ChangeCameraAtScreen();
    }

    public void TurnTVOnOff()
    {
        camerasHud.SetActive(isPlayerNearKeyboard);
        OnOffLight.material.SetColor("_EmissionColor", isPlayerNearKeyboard ? Color.green * 1f : Color.red * 1f);
        ChangeCameraAtScreen();
    }

    public void ChangeCameraAtScreen()
    {
        TurnOffAllCameras();
        SetAllCamerasColorsToGray();

        if (isPlayerNearKeyboard)
        {
            activeCamerasImage[cameraNumber].color = Color.red;
            cameras[cameraNumber].SetActive(true);
            TVScreen.material = camerasMaterials[cameraNumber];
            SetNewCameraRotation();
        }
        else
        {
            TVScreen.material = camerasMaterials[10];
        }
    }

    public void SetNewCameraRotation()
    {
        slider.value = rotation;

        cameras[cameraNumber].transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private void SetAllCamerasColorsToGray()
    {
        Color newColor = new (0.2f, 0.2f, 0.2f);
        for (int i = 0; i < activeCamerasImage.Length; i++)
        {
            activeCamerasImage[i].color = newColor;
        }
    }

    private void TurnOffAllCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
    }
}
