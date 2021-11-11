using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            if (instance == null)
            {
                Debug.LogErrorFormat("Singleton<{0}> instance has been not found.", typeof(T));
            }
            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            if (instance != this)
            {
                DestroySelf();
            }
        }
    }

    protected void OnValidate()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            if (instance != this)
            {
                 Debug.LogErrorFormat("Singleton<{0}> already has an instance on scene", this.GetType());

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.delayCall -= DestroySelf;
                UnityEditor.EditorApplication.delayCall += DestroySelf;
            #endif
            }
        }
    }

    private void DestroySelf()
    {
        if (!Application.isPlaying)
        {
            DestroyImmediate(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
