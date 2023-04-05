using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    SoundData[] _soundsData;

    GameObject _playingBGM;

    public void CallSound(string soundName)
    {
        //指定された名前の音声データをデータ配列から取得、無い場合はそのまま終了
        var soundData = GetSoundData(soundName);
        if (soundData == null)
        {
            Debug.LogError("サウンドがありません");
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

        //オブジェクト生成＆データ初期化
        var soundObj = new GameObject(soundData.name);
        AudioSource audioSource = soundObj.AddComponent<AudioSource>();

        audioSource.clip = soundData.clip;
        audioSource.volume = soundData.volume;
        audioSource.loop = soundData.isLoop;
        audioSource.Play();
        if(soundData.type == SoundType.BGM) _playingBGM = soundObj;

        //ループしない場合、音声終了後に削除
        if (!soundData.isLoop)
        {
            Destroy(soundObj, soundData.clip.length);
        }
    }
}
