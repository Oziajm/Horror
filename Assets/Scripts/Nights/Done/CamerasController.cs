using UnityEngine;
using UnityEngine.UI;

public class CamerasController : MonoBehaviour
{
    #region Variables
    [Space(10)]
    [Header("Public variables")]
    [Space(10)]
    public float rotation;

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

    private int camNumber;

    #endregion

    #region Unity Methods

    private void Start()
    {
        TurnTVOnOff(false);
    }

    #endregion

    #region Public Methods

    public void TurnTVOnOff(bool isPlayerNearKeyboard)
    {
        camerasHud.SetActive(isPlayerNearKeyboard);
        OnOffLight.material.SetColor("_EmissionColor", isPlayerNearKeyboard ? Color.green * 1f : Color.red * 1f);
        ChangeCameraAtScreen(isPlayerNearKeyboard ? 1 : 10);
    }

    public void ChangeCameraAtScreen(int cameraNumber)
    {
        TurnOffAllCameras();
        SetAllCamerasColorsToGray();

        if(cameraNumber == 10)
        {
            TVScreen.material = camerasMaterials[cameraNumber];
        } else
        {
            activeCamerasImage[cameraNumber].color = Color.red;
            cameras[cameraNumber].SetActive(true);
            TVScreen.material = camerasMaterials[cameraNumber];
            camNumber = cameraNumber;
        }
    }

    public void SetNewCameraRotation()
    {
        slider.value = rotation;

        cameras[camNumber].transform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    #endregion

    #region Private Methods

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

    #endregion
}
