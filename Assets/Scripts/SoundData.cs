using System;
using UnityEngine;

[Serializable]
public class SoundData
{
    [SerializeField] private string name;
    [SerializeField] private SoundType type;
    [SerializeField] private AudioClip clip;
    [SerializeField ,Range(0,1)] private float volume;
    [SerializeField] private bool isLoop;

    public string Name => name;
    public SoundType Type => type;
    public AudioClip Clip => clip;
    public float Volume => volume;
    public bool IsLoop => isLoop;

}
