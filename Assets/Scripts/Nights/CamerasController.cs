using UnityEngine;
using UnityEngine.UI;

public class CamerasController : MonoBehaviour
{
    public int cameraNumber;
    public int rotation = 0;

    [SerializeField] private Image[] activeCamerasImage;
    [SerializeField] private MeshRenderer TVScreen;
    [SerializeField] private Material[] camerasMaterials;
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private Slider slider;

    private void Start()
    {
        ChangeCameraAtScreen();
    }

    public void ChangeCameraAtScreen()
    {
        TurnOffAllCameras();
        SetAllCamerasColorsToGray();

        activeCamerasImage[cameraNumber].color = Color.red;
        cameras[cameraNumber].SetActive(true);
        TVScreen.material = camerasMaterials[cameraNumber];
        SetNewCameraRotation();
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
