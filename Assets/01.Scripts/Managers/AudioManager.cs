using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{

    [Header("BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume = 1.0f;
    private AudioSource bgmPlayer;
    private int bgmClipIndex = 0;  // ���� ��� ���� Ŭ�� �ε���
    [SerializeField] private AudioMixerGroup bgmAudioMixerGroup;

    [Header("SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    [SerializeField] private AudioMixerGroup sfxAudioMixerGroup;

    public enum Sfx
    {
        Sensor,
        Coin,
        Dead,
        Lose,
        Win
    }

    private void Awake()
    {
        Init();
    }
    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.outputAudioMixerGroup = bgmAudioMixerGroup;

        //ȿ���� �÷��̾� �ʱ�ȭ
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].volume = sfxVolume;
            sfxPlayers[index].outputAudioMixerGroup = sfxAudioMixerGroup; // SFX ����� �ͼ� �׷� ����
        }

    }

    public void PlaySfx(Sfx sfx)
    {

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClip[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }


    }
    public void StopSfx(Sfx sfx)
    {
        foreach (AudioSource sfxPlayer in sfxPlayers)
        {

            if (sfxPlayer.isPlaying && sfxPlayer.clip == sfxClip[(int)sfx])
            {
                sfxPlayer.Stop();
                break;
            }

        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay && bgmClip.Length > 0)
        {
            bgmClipIndex = 0; // ù ��° Ŭ������ ����
            PlayCurrentBgm();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }
    private void PlayCurrentBgm()
    {
        if (bgmClip.Length == 0) return;

        bgmPlayer.clip = bgmClip[bgmClipIndex];
        bgmPlayer.Play();

        // ���� Ŭ���� ������ ���� Ŭ�� ���
        Invoke(nameof(PlayNextBgm), bgmPlayer.clip.length);
    }

    private void PlayNextBgm()
    {
        bgmClipIndex = (bgmClipIndex + 1) % bgmClip.Length; // ���� Ŭ�� �ε��� ���
        PlayCurrentBgm();
    }
    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = bgmVolume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        foreach (AudioSource sfxPlayer in sfxPlayers)
        {
            sfxPlayer.volume = sfxVolume;
        }
    }
}
