using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : SceneBasedMonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera gameplayCam;
    [SerializeField] private CinemachineVirtualCamera finalCam;
    [SerializeField] private CinemachineVirtualCamera activeCam => CameraSwitcher.GetActiveCamera();
    private void OnEnable()
    {
        Debug.Log(activeCam);
        CameraSwitcher.AddCamera(gameplayCam);
        CameraSwitcher.AddCamera(finalCam);
    }

    public void SwitchCam(string cameraName)
    {
        //TODO ENUM SWITCH
        if (cameraName.Equals("GameplayCam") && !CameraSwitcher.IsActiveCamera(gameplayCam))
        {
            CameraSwitcher.SwitchCamera(gameplayCam);
        }
        else if (cameraName.Equals("FinalCam") && !CameraSwitcher.IsActiveCamera(finalCam))
        {
            CameraSwitcher.SwitchCamera(finalCam);
        }
    }
}
