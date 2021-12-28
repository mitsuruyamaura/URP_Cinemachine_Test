using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ChangeCameraBox : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private AreaCameraManager areaCameraManager;


    private void OnTriggerEnter(Collider other) {

        areaCameraManager.ChangeCurrentCamera(virtualCamera);
    }
}
