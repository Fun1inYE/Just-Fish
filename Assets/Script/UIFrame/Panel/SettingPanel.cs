using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置页面类
/// </summary>
public class SettingPanel : BasePanel
{
    /// <summary>
    /// 主音量的滑块
    /// </summary>
    public Slider mainVolumeSlider;
    /// <summary>
    /// 背景音乐的滑块
    /// </summary>
    public Slider bgmVolumeSlider;
    /// <summary>
    /// 音效的滑块
    /// </summary>
    public Slider sfxVolumeSlider;

    /// <summary>
    /// 重写构造函数
    /// </summary>
    public SettingPanel() : base("SettingPanel") { }

    public override void OnEnter()
    {
        //进入窗口时候，读取用户设置数据
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();

        //绑定滑条方法
        mainVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "MainAudioSlider").GetComponent<Slider>();
        mainVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetMainVolume);
        mainVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetMainVolume();

        bgmVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "BGMAudioSlider").GetComponent<Slider>();
        bgmVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetBGMVolume);
        bgmVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetBGMVolume();

        sfxVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SFXAudioSlider").GetComponent<Slider>();
        sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetSFXVolume);
        sfxVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetSFXVolume();

        //绑定按钮的方法
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndCancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //保存当前用户设置
            AudioManager.Instance.audioSettingManager.SaveAudioSetting();
            //弹出当前窗口
            panelManager.Pop();
        });

        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //弹出当前窗口
            panelManager.Pop();
        });
    }

    public override void OnExit()
    {
        //退出该窗口时，读取用户设置
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();

        //解绑滑条方法
        mainVolumeSlider.onValueChanged.RemoveAllListeners();
        bgmVolumeSlider.onValueChanged.RemoveAllListeners();
        sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        //解绑按钮的监听器
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndCancelButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.RemoveAllListeners();

        base.OnExit();
    }
}
