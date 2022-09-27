using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    enum SoundType
    {
        SE,
        ME,
        BGM,
    }

    GameObject _go = default;
    AudioSource _asBGM = default;
    AudioSource _defSE = default;
    AudioSource[] _asSE = default;
    AudioSource _asME = default;
    const int _seCh = 4;

    static SoundManager _instance = null;
    public static SoundManager Instance => _instance ??= new SoundManager();
    public SoundManager() => _asSE = new AudioSource[_seCh];

    class Data
    {
        public string _key;
        public string _name;
        public AudioClip _clip;

        public Data(string key, string res)
        {
            _key = key;
            _name = "Sounds/" + res;
            _clip = Resources.Load(_name) as AudioClip;
        }
    }

    static Dictionary<string, Data> bgm = new Dictionary<string, Data>();
    static Dictionary<string, Data> se = new Dictionary<string, Data>();
    static Dictionary<string, Data> me = new Dictionary<string, Data>();

    AudioSource GetAudioSource(SoundType type, int ch = -1)
    {
        if (_go)
        {
            _go = new GameObject();
            GameObject.DontDestroyOnLoad(_go);
            _asBGM = _go.AddComponent<AudioSource>();
            _defSE = _go.AddComponent<AudioSource>();
            for(int i = 0; i < _seCh; i++)
            {
                _asSE[i] = _go.AddComponent<AudioSource>();
            }
            _asME = _go.AddComponent<AudioSource>();
        }
        switch (type)
        {
            case SoundType.BGM:
                return _asBGM;
            case SoundType.SE:
                if (0 <= ch && ch < _seCh) return _asSE[ch];
                else return _defSE;
            default:
                return _asME;
        }
    }

    static public void LoadBGM(string key, string name) => Instance.LoadBgm(key, name);
    void LoadBgm(string key, string name)
    {
        if (bgm.ContainsKey(key)) return;
        bgm.Add(key, new Data(key, name));
    }
    static public void LoadSE(string key, string name) => Instance.LoadSe(key, name);
    void LoadSe(string key, string name)
    {
        if(se.ContainsKey(key)) return;
        se.Add(key, new Data(key, name));
    }
    static public void LoadME(string key, string name) => Instance.LoadMe(key, name);
    void LoadMe(string key, string name)
    {
        if (se.ContainsKey(key)) return;
        me.Add(key, new Data(key, name));
    }

    static public bool PlayBGM(string key) => Instance.PlayBgm(key);
    bool PlayBgm(string key)
    {
        if(!bgm.ContainsKey(key)) return false;

        StopBGM();
        var _data = bgm[key];
        var source = GetAudioSource(SoundType.BGM);
        source.loop = true;
        source.clip = _data._clip;
        source.Play();
        return true;
    }

    public static bool StopBGM() => Instance.StopBgm();
    bool StopBgm()
    {
        GetAudioSource(SoundType.BGM).Stop();
        return true;
    }

    static public bool PlaySE(string key, int ch = -1) => Instance.PlaySe(key,ch);
    bool PlaySe(string key, int ch = -1)
    {
        if (!se.ContainsKey(key))return false;

        var _data = se[key];

        if (0 <= ch && ch < _seCh)
        {
            var source = GetAudioSource(SoundType.SE, ch);
            source.clip = _data._clip;
            source.Play();
        }
        else
        {
            var source = GetAudioSource(SoundType.SE);
            source.PlayOneShot(_data._clip);
        }

        return true;
    }

    static public bool PlayME(string key) => Instance.PlayMe(key);
    bool PlayMe(string key)
    {
        if (!me.ContainsKey(key)) return false;

        var _data = me[key];
        var source = GetAudioSource(SoundType.ME);
        source.loop = false;
        source.clip = _data._clip;
        source.Play();
        return true;
    }
}
