using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// ��Ƶ��ܹ�����
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// ÿһ����Ƶ����Ϣ
    /// </summary>
    [Serializable]
    public class Sound
    {
        /// <summary>
        /// ��ƵƬ��
        /// </summary>
        public AudioClip clip;
        /// <summary>
        /// ��Ƶ����
        /// </summary>
        public AudioMixerGroup outputGroup;
        /// <summary>
        /// ������С
        /// </summary>
        [Range(0, 1)]
        public float volume;
        /// <summary>
        /// ��Ƶ�Ƿ񿪾ֲ���
        /// </summary>
        public bool playOnAwake;
        /// <summary>
        /// ��Ƶ�Ƿ�ѭ��
        /// </summary>
        public bool loop;
    }

    /// <summary>
    /// ������Ƶ����Ϣ
    /// </summary>
    public List<Sound> sounds;
    /// <summary>
    /// ÿһ����ƵƬ�ζ�Ӧһ����Ƶ���
    /// </summary>
    public Dictionary<string, AudioSource> audioDic;

    /// <summary>
    /// ��������������
    /// </summary>
    public AudioSettingManager audioSettingManager;

    /// <summary>
    /// ��Ƶ�������ĵ���ģʽ
    /// </summary>
    public static AudioManager Instance;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        audioDic = new Dictionary<string, AudioSource>();

        //����ģʽ�ĳ�ʼ��
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        audioSettingManager = GetComponent<AudioSettingManager>();
    }

    public void Start()
    {
        foreach (var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            //����һ����ƵԴ
            AudioSource source = obj.AddComponent<AudioSource>();
            //����ƵԴ��ز�����ֵ
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            //���sound����Ϊ�����Ͳ���
            if (sound.playOnAwake)
                source.Play();

            //����Ƶ��Ϣ��ӵ��ֵ���
            audioDic.Add(sound.clip.name, source);
        }
    }

    /// <summary>
    /// ��������Ϊname����Ƶ
    /// </summary>
    /// <param name="name">��Ƶ������</param>
    /// <param name="isWait">�Ƿ�Ҫ�ȴ���Ƶ������</param>
    /// <param name="loop">�Ƿ�Ҫ��Ƶѭ������</param>
    public void PlayAudio(string name, bool isWait = false, bool loop = false)
    {
        if(!audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"����Ϊ{name}����Ƶ������");
            return;
        }

        //������Ƶ�Ƿ�ѭ������
        audioDic[name].loop = loop;

        //�ж���Ƶ�Ƿ���Ҫ�ȴ�������
        if (isWait)
        {
            //�ø���Ƶ��ȫ��������Ƶ
            if (audioDic[name].isPlaying)
            {
                audioDic[name].Play();
            }
            audioDic[name].Play();
        }
        else
        {
            //������Ƶ
            audioDic[name].Play();
           
        }
    }

    /// <summary>
    /// ֹͣ����Ϊname����Ƶ
    /// </summary>
    /// <param name="name">��Ƶ������</param>
    /// <param name="isWait">�Ƿ�Ҫ�ȴ���Ƶ������</param>
    public void StopAudio(string name, bool isWait = false)
    {
        if (!audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"����Ϊ{name}����Ƶ������");
            return;
        }

        //ֹͣ��Ƶ�Ĳ���
        audioDic[name].Stop();
    }
}
