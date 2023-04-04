using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    SoundData[] _soundsData;

    GameObject _playingBGM;

    public void CallSound(string soundName)
    {
        //指定された名前の音声データをデータ配列からインデックスを取得、無い場合はそのまま終了
        int index = Array.IndexOf(_soundsData, soundName);
        if (index < 0)
        {
            Debug.LogError("サウンドがありません");
            return;
        }

        switch (_soundsData[index].type)
        {
            case SoundType.BGM:
                if (_playingBGM)
                {
                    Destroy(_playingBGM);
                }
                SoundPlay(_soundsData[index]);
                break;
            case SoundType.ME:
            case SoundType.SE:
                SoundPlay(_soundsData[index]);
                break;
        }
    }

    void SoundPlay(SoundData soundData)
    {

        //オブジェクト生成＆データ初期化
        var soundPlayer = new GameObject(soundData.clip.name);
        AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();

        audioSource.clip = soundData.clip;
        audioSource.volume = soundData.volume;
        audioSource.loop = soundData.isLoop;
        audioSource.Play();
        _playingBGM = soundPlayer;

        //ループしない場合、音声終了後に削除
        if (!soundData.isLoop)
        {
            Destroy(audioSource, soundData.clip.length);
        }
    }
}
