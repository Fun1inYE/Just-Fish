using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// ��������������
/// </summary>
public class AudioSettingManager : MonoBehaviour
{
    //��Ƶ������
    public AudioMixer mixer;

    public void SetMainVolume(float value)
    {
        mixer.SetFloat("Master", value);
    }

    /// <summary>
    /// ͨ��Slider����BGM����
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }

    /// <summary>
    /// ͨ��Slider����SFX����
    /// </summary>
    /// <param name="value"></param>
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }

    /// <summary>
    /// ��������������ֵ
    /// </summary>
    /// <returns></returns>
    public float GetMainVolume()
    {
        float masterAudioValue;
        mixer.GetFloat("Master", out masterAudioValue);
        return masterAudioValue;
    }

    /// <summary>
    /// ���ر������ֵ���ֵ
    /// </summary>
    /// <returns></returns>
    public float GetBGMVolume()
    {
        float bgmAudioValue;
        mixer.GetFloat("BGM", out bgmAudioValue);
        return bgmAudioValue;
    }

    /// <summary>
    /// ������Ч����ֵ
    /// </summary>
    /// <returns></returns>
    public float GetSFXVolume()
    {
        float sfxVolumeValue;
        mixer.GetFloat("SFX", out sfxVolumeValue);
        return sfxVolumeValue;
    }


    /// <summary>
    /// ������������
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

        //ͨ��PlayerPrefs��������
        PlayerPrefs.Save();
        
    }

    /// <summary>
    /// �����û���������
    /// </summary>
    public void LoadAudioSetting()
    {
        //���ز���������ֵ
        if (PlayerPrefs.HasKey("MasterVolume"))
            mixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        if (PlayerPrefs.HasKey("BGMVolume"))
            mixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGMVolume"));
        if (PlayerPrefs.HasKey("SFXVolume"))
            mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
    }
}
