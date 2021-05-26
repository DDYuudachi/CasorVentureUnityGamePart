using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    //https://www.youtube.com/watch?v=yQgVKR6PMqo FMOD audio setting menu
    FMOD.Studio.Bus master;
    FMOD.Studio.Bus music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus ambience;
    float masterVolume = 1.0f;
    float musicVolume = 1.0f;
    float SFXVolume = 1.0f;
    float ambienceVolume = 1.0f;

    private void Awake()
    {
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        ambience = FMODUnity.RuntimeManager.GetBus("bus:/Master/Ambience");
    }

    private void Update()
    {
        master.setVolume(masterVolume);
        music.setVolume(musicVolume);
        SFX.setVolume(SFXVolume);
        ambience.setVolume(ambienceVolume);
    }

    public void masterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
    }

    public void musicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }

    public void ambienceVolumeLevel(float newAmbienceVolume)
    {
        ambienceVolume = newAmbienceVolume;
    }
}
