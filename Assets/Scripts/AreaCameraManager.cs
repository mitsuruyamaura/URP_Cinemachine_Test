using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class AreaCameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] areaCameras;

    /// <summary>
    /// VirtualCamera‚Ì—Dæ‡ˆÊ‚ğØ‚è‘Ö‚¦‚é
    /// </summary>
    /// <param name="cameraNo"></param>
    public void ChengeVirtualCamera(CinemachineVirtualCamera newCamera) {

        foreach (CinemachineVirtualCamera camera in areaCameras) {
            if (camera == newCamera) {
                camera.Priority = 10;
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
