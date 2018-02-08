using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  NOTE: Copy and paste from Line 8 to line 38 in the class you need the singleton
//  and replace all the words "Singleton" with the name of the class
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
                    GameObject go = null;
                    // ------- If the script DOESN'T need to be tweaked in the inspector uncomment the next 2 lines

                    //GameObject go = new GameObject(typeof(Singleton).ToString());
                    //instance = go.AddComponent<Singleton>();

                    // ------- If the script DOES need to be tweaked in the inspector, 
                    // ------- create a prefab of it and locate it on the "Resources" folder,
                    // ------- uncomment the next 2 lines OF CODE and repalce the string on th Load function with the name of the prefab
                    // ------- add the name of the folder and '/' at the beginning if it's located in a subfolder "Prefabs/NAME"

                    //GameObject go = Instantiate(Resources.Load("NAME")) as GameObject;
                    //instance = go.GetComponent<Singleton>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
}
