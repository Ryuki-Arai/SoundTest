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
        //�w�肳�ꂽ���O�̉����f�[�^���f�[�^�z�񂩂�C���f�b�N�X���擾�A�����ꍇ�͂��̂܂܏I��
        var soundData = GetSoundData(soundName);
        if (soundData == null)
        {
            Debug.LogError("�T�E���h������܂���");
            return;
        }

        switch (soundData.type)
        {
            case SoundType.BGM:
                if (_playingBGM)
                {
                    Destroy(_playingBGM);
                }
                SoundPlay(soundData);
                break;
            case SoundType.ME:
            case SoundType.SE:
                SoundPlay(soundData);
                break;
        }
    }

    SoundData GetSoundData(string soundName)
    {
        for(int i = 0; i < _soundsData.Length; i++)
        {
            if (_soundsData[i].name == soundName)
            {
                return _soundsData[i];
            }
        }
        return null;
    }

    void SoundPlay(SoundData soundData)
    {

        //�I�u�W�F�N�g�������f�[�^������
        var soundPlayer = new GameObject(soundData.clip.name);
        AudioSource audioSource = soundPlayer.AddComponent<AudioSource>();

        audioSource.clip = soundData.clip;
        audioSource.volume = soundData.volume;
        audioSource.loop = soundData.isLoop;
        audioSource.Play();
        _playingBGM = soundPlayer;

        //���[�v���Ȃ��ꍇ�A�����I����ɍ폜
        if (!soundData.isLoop)
        {
            Destroy(audioSource, soundData.clip.length);
        }
    }
}
