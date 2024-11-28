using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 声音混响设置类
/// </summary>
public class AudioSettingManager : MonoBehaviour
{
    //音频混响器
    public AudioMixer mixer;

    public void SetMainVolume(float value)
    {
        mixer.SetFloat("Master", value);
    }

    /// <summary>
    /// 通过Slider调整BGM声音
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }

    /// <summary>
    /// 通过Slider调整SFX声音
    /// </summary>
    /// <param name="value"></param>
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }

    /// <summary>
    /// 返回主音量的数值
    /// </summary>
    /// <returns></returns>
    public float GetMainVolume()
    {
        float masterAudioValue;
        mixer.GetFloat("Master", out masterAudioValue);
        return masterAudioValue;
    }

    /// <summary>
    /// 返回背景音乐的数值
    /// </summary>
    /// <returns></returns>
    public float GetBGMVolume()
    {
        float bgmAudioValue;
        mixer.GetFloat("BGM", out bgmAudioValue);
        return bgmAudioValue;
    }

    /// <summary>
    /// 返回音效的数值
    /// </summary>
    /// <returns></returns>
    public float GetSFXVolume()
    {
        float sfxVolumeValue;
        mixer.GetFloat("SFX", out sfxVolumeValue);
        return sfxVolumeValue;
    }


    /// <summary>
    /// 保存音量设置
    /// </summary>
    public void SaveAudioSetting()
    {
        float masterAudioValue;
        mixer.GetFloat("Master", out masterAudioValue);
        PlayerPrefs.SetFloat("MasterVolume", masterAudioValue);

        float bgmAudioValue;
        mixer.GetFloat("BGM", out bgmAudioValue);
        PlayerPrefs.SetFloat("BGMVolume", bgmAudioValue);

        float sfxVolume;
        mixer.GetFloat("SFX", out sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        //通过PlayerPrefs保存数据
        PlayerPrefs.Save();
        
    }

    /// <summary>
    /// 加载用户音量设置
    /// </summary>
    public void LoadAudioSetting()
    {
        //加载并设置音量值
        if (PlayerPrefs.HasKey("MasterVolume"))
            mixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        if (PlayerPrefs.HasKey("BGMVolume"))
            mixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
        if (PlayerPrefs.HasKey("SFXVolume"))
            mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
    }
}
