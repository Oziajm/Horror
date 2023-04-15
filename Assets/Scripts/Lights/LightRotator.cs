using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    [SerializeField]
    private Transform light;

    private void Update()
    {
        light.Rotate(Vector3.up * Time.deltaTime * 100f);
    }
}
