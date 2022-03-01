using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBasedMonoSingleton<T> : MonoBehaviour where T : SceneBasedMonoSingleton<T>
{
    private volatile static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                //If it is not there 
                if (instance == null) instance = new GameObject(typeof(T).Name).AddComponent<T>();

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
}