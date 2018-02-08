using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Note: This approach loads the AudioClip using Resources.Load() dynamically.
// Once the resource is no longer needed (the audio clip has stopped playing), the
// resource is freed from memory via (Resources.UnloadAsset()), reducing runtime
// memory consumption

// this system can be extended to support multiple audio sources, and audio clips playing
// simultaneously

// it needs to be added to game object with at least 2 audio sources,
// one for the FX and one for the music although it is recommended to have at least 5 for the FX

/// <summary>
/// This class is in charge of the audio in a 2D game, 

/// </summary>
public class AudioSystem2D : MonoBehaviour
{
    // In the inspector drag each Audios ource to these fields
    [SerializeField]
    AudioSource[] fXSources;
    [SerializeField]
    AudioSource musicSource;

    // All audios must be on the Resources folder and if there's any sub folder, the path must be declared in the next fields
    // eg: Audio/FX/   this is the path for the audio clips in the folder "FX" inside the "Audio" folder which is inside the "Resources" folder
    [SerializeField]
    string fxFolder;
    [SerializeField]
    string musicFolder;

    // Mute properties, turn this to true to Mute the audiosources
    public bool muteFX;
    public bool muteMusic;

    // Array of Audio clips equal to the quantity of FX Audio sources
    AudioClip[] loadedResources = new AudioClip[20];


    // Singleton
    private static AudioSystem2D instance = null;

    public static AudioSystem2D Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioSystem2D>();

                if (instance == null)
                {
                    GameObject go = Instantiate(Resources.Load("AudioSystem2D")) as GameObject;
                    instance = go.GetComponent<AudioSystem2D>();
                    DontDestroyOnLoad(go);
                }
            }

            return instance;
        }
    }

    // Just one instance is allowed
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            loadedResources = new AudioClip[fXSources.Length];
    }

    // Check if the user has already muted the program
    // to mute the music or fx change the "Music" or "FX" prefs anywhere on the project
    private void Start()
    {
        if (PlayerPrefs.GetInt("Music") != 1)
            muteMusic = false;
        else
            muteMusic = true;
        if (PlayerPrefs.GetInt("FX") != 1)
            muteFX = false;
        else
            muteFX = true;
    }

    void Update()
    {
        musicSource.mute = muteMusic;
        // If any FX audio source stops playing, this will unload the asset 
        // from memory and will set the source to be used again
        for (int i = 0; i < fXSources.Length; i++)
        {
            if (!fXSources[i].isPlaying && loadedResources[i] != null)
            {
                Resources.UnloadAsset(loadedResources[i]);
                loadedResources[i] = null;
                fXSources[i].clip = null;
            }
        }
    }

    // number to use a different FX audio source every time
    int x = 0;

    /// <summary>
    /// Loads and plays a sound from the resources folder
    /// </summary>
    /// <param name="resourceName">
    /// Name of the clip that is going to be played
    /// </param>
    public void PlaySound(string resourceName)
    {
        if (!muteFX)
        {
            // if the fx aren't muted it will create a new resource name with the name of the clip passed as argument and the root folder for fx
            resourceName = fxFolder + resourceName;
            // it will look for an unused source
            while (fXSources[x].isPlaying)
            {
                x++;
                if (x >= fXSources.Length)
                    x = 0;
            }
            // Loads the asset and and sets it as a clip of the corresponding source, then play's it and adds 1 unito to the x so next time it uses a different source
            loadedResources[x] = Resources.Load(resourceName) as AudioClip;
            fXSources[x].clip = loadedResources[x];
            fXSources[x].Play();
            x++;
            // I x reaches the top of the list then it starts again
            if (x >= fXSources.Length)
                x = 0;
        }
    }

    public void PlayMusic(string songName)
    {
        // If the music source is not muted it will create a new resource name with the name of the clip passed as argument and the root folder for music
        if (!muteMusic)
        {
            songName = musicFolder + songName;
            // If there's another clip on the source it will unload it from memory an then load the new asset as the clip to the source and then play's it
            if (musicSource.clip != null)
            {
                musicSource.Stop();
                Resources.UnloadAsset(musicSource.clip);
            }
            musicSource.clip = Resources.Load(songName) as AudioClip;
            musicSource.Play();
        }
    }
}