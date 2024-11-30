using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����ҳ����
/// </summary>
public class SettingPanel : BasePanel
{
    /// <summary>
    /// �������Ļ���
    /// </summary>
    public Slider mainVolumeSlider;
    /// <summary>
    /// �������ֵĻ���
    /// </summary>
    public Slider bgmVolumeSlider;
    /// <summary>
    /// ��Ч�Ļ���
    /// </summary>
    public Slider sfxVolumeSlider;

    /// <summary>
    /// ��д���캯��
    /// </summary>
    public SettingPanel() : base("SettingPanel") { }

    public override void OnEnter()
    {
        //���봰��ʱ�򣬶�ȡ�û���������
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();

        //�󶨻�������
        mainVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "MainAudioSlider").GetComponent<Slider>();
        mainVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetMainVolume);
        mainVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetMainVolume();

        bgmVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "BGMAudioSlider").GetComponent<Slider>();
        bgmVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetBGMVolume);
        bgmVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetBGMVolume();

        sfxVolumeSlider = SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SFXAudioSlider").GetComponent<Slider>();
        sfxVolumeSlider.onValueChanged.AddListener(AudioManager.Instance.audioSettingManager.SetSFXVolume);
        sfxVolumeSlider.value = AudioManager.Instance.audioSettingManager.GetSFXVolume();

        //�󶨰�ť�ķ���
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndCancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //���浱ǰ�û�����
            AudioManager.Instance.audioSettingManager.SaveAudioSetting();
            //������ǰ����
            panelManager.Pop();
        });

        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            //������ǰ����
            panelManager.Pop();
        });
    }

    public override void OnExit()
    {
        //�˳��ô���ʱ����ȡ�û�����
        AudioManager.Instance.audioSettingManager.LoadAudioSetting();

        //���������
        mainVolumeSlider.onValueChanged.RemoveAllListeners();
        bgmVolumeSlider.onValueChanged.RemoveAllListeners();
        sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        //���ť�ļ�����
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "SaveAndCancelButton").GetComponent<Button>().onClick.RemoveAllListeners();
        SetGameObjectToParent.FindChildRecursive(activePanel.transform, "CancelButton").GetComponent<Button>().onClick.RemoveAllListeners();

        base.OnExit();
    }
}
