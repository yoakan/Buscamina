using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScripteableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    throw new System.Exception("SingletonScriptableObject: No instances of " + typeof(T).ToString() + " were found.");
                }
                if (results.Length > 1)
                {
                    throw new System.Exception("SingletonScriptableObject: Multiple instances of " + typeof(T).ToString() + " were found.");
                }
                _instance = results[0];
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return _instance;
        }
    }

}
