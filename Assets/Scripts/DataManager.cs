using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour 
{
    // Singleton Pattern
    private static DataManager instance = null;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(DataManager).ToString());
                    instance = go.AddComponent<DataManager>();
                }
                instance.Load();
            }

            return instance;
        }
    }

    // SAME variables as the UserData class to handle them while the program is executed
    public bool booleanValue = false;
    public int intValue = 5;
    public int[] highScores = new int[10];

    /// <summary>
    /// Resets the variables values to a default and then saves the data
    /// </summary>
    public void ResetData()
    {
        booleanValue = false;
        intValue = 5;
        highScores = new int[10];
        Save();
    }

    /// <summary>
    /// Creates a binary file named userData.dat in a specific location , with the binary serialization of a UserData instance
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/userData.dat");
        // After the file is created a new instance of UserData is created and the values of the variable in this class are assigned to the variables in the instance
        UserData data = new UserData();
        data.booleanValue = booleanValue;
        data.intValue = intValue;
        data.highScores = highScores;
        // The instance is then serialized in the file created
        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Loads the data inside a file named userData.dat and assigns it to this class variables
    /// </summary>
    public void Load()
    {
        // If the file userDta.dat exists it is opened an deserialized in a new instance of UserData class
        if (File.Exists(Application.persistentDataPath + "/userData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/userData.dat", FileMode.Open);
            UserData data = (UserData)bf.Deserialize(file);
            // The file needs to be closed
            file.Close();
            // The variable values of the class are assigned to this manager variables
            booleanValue = data.booleanValue;
            intValue = data.intValue;
            highScores = data.highScores;
            // If there's an array of values inside the file, is possible at the first time the program is executed that this array has a null value
            // So a new array needs to be instanced with the lenght needed
            if (highScores == null)
                highScores = new int[10];
        }
        else
        {
            // If the file hasnt yet been created, a call is made to the Save function to create the file
            Save();
        }
    }
}
