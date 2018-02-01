using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour 
{
    private static Singleton instance = null;

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Singleton>();

                if (instance == null)
                {
                    // ------- If the script DOESN'T need to be tweaked in the inspector uncomment the next 2 lines
                    //GameObject go = new GameObject(typeof(Singleton).ToString());
                    //instance = go.AddComponent<Singleton>();
                    // ------- If the script DOES need to be tweaked in the inspector, create a prefab of it
                    // ------- uncomment the next 3 lines and repalce the string on th Load function with the name of the prefab
                    //GameObject go = Instantiate(Resources.Load("NAME")) as GameObject;
                    //instance = go.GetComponent<AudioSystem>();
                    //DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
}
