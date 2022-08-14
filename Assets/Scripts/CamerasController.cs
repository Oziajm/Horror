using UnityEngine;
using TMPro;

public class CamerasController : MonoBehaviour
{
    public int cameraNumber;

    [SerializeField] private TextMeshPro cameraNumberAtScreen;
    [SerializeField] private MeshRenderer TVScreen;
    [SerializeField] private Material[] camerasMaterials;
    private void Start()
    {
        ChangeCameraAtScreen();
    }

    public void ChangeCameraAtScreen()
    {
        cameraNumberAtScreen.text = cameraNumber.ToString();
        switch (cameraNumber)
        {
            case 0:
                TVScreen.material = camerasMaterials[0];
                break;
            case 1:
                TVScreen.material = camerasMaterials[1];
                break;
            case 2:
                TVScreen.material = camerasMaterials[2];
                break;
            case 3:
                TVScreen.material = camerasMaterials[3];
                break;
            case 4:
                TVScreen.material = camerasMaterials[4];
                break;
            case 5:
                TVScreen.material = camerasMaterials[5];
                break;
            case 6:
                TVScreen.material = camerasMaterials[6];
                break;
            case 7:
                TVScreen.material = camerasMaterials[7];
                break;
            case 8:
                TVScreen.material = camerasMaterials[8];
                break;
            case 9:
                TVScreen.material = camerasMaterials[9];
                break;
        }
    }
}
