using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class AreaCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] areaCameras;

    private CinemachineVirtualCamera currentCamera;

    public CinemachineVirtualCamera CurrentCamera
    {
        get => currentCamera;
        set => currentCamera = value;
    }

    private void Start() {
        ChangeCurrentCamera(areaCameras[0]);
    }

    /// <summary>
    /// VirtualCamera‚Ì—Dæ‡ˆÊ‚ğØ‚è‘Ö‚¦‚é
    /// </summary>
    /// <param name="cameraNo"></param>
    public void ChangeCurrentCamera(CinemachineVirtualCamera newCamera) {

        foreach (CinemachineVirtualCamera camera in areaCameras) {
            if (camera == newCamera) {
                camera.Priority = 10;

                currentCamera = newCamera;
            } else {
                camera.Priority = 5;
            }
        }


        //for (int i = 0; i < areaCameras.Length; i++) {
        //    if (cameraNo == i) {
        //        areaCameras[i].Priority = 11;
        //    } else {
        //        areaCameras[i].Priority = 10;
        //    }
        //}
    }
}
