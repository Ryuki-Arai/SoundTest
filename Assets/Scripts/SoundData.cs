using System;
using UnityEngine;

[Serializable]
public class SoundData
{
    public string name;
    public SoundType type;
    public AudioClip clip;
    [Range(0,1)] public float volume;
    public bool isLoop;
}
