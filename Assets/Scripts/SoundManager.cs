using System.Collections;
using System.Collections.Generic;

public class SoundManager
{
    static SoundManager _instance = default;
    public static SoundManager Instance => _instance ??= new SoundManager();

    SoundData playngBGM = default;
    public void SetBGM(SoundData bgm) { _instance.playngBGM = bgm; }
}
