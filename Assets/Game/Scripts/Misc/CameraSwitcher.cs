using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    static private List<CinemachineVirtualCamera> Cameras = new List<CinemachineVirtualCamera>();
    static private CinemachineVirtualCamera ActiveCam = null;


    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;

        foreach (CinemachineVirtualCamera cam in Cameras)
        {
            cam.Priority = cam != camera ? 0 : 10;
        }

        ActiveCam = camera;
    }

    public static void AddCamera(CinemachineVirtualCamera camera)
    {
        Cameras.Add(camera);
    }

    public static void RemoveCamera(CinemachineVirtualCamera camera)
    {
        Cameras.Remove(camera);
    }

    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return ActiveCam == camera ? true : false;
    }

    public static CinemachineVirtualCamera GetActiveCamera()
    {
        return ActiveCam;
    }

}