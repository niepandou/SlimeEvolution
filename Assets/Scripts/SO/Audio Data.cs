using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    // BGM
    public AudioClip bgmMainMenu;
    //不同场景根据id设置对应的bgm
    public AudioClip[] bgmGameplay;
    
    // 音效（可扩展）
    public AudioClip sfxClick;
    public AudioClip sfxJump;
    public AudioClip sfxShoot;
    public AudioClip sfxExplosion;
    public AudioClip sfxCast;
    public AudioClip down;
}
