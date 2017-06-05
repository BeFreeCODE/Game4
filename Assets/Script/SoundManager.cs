using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource myAudio;
    [SerializeField]
    private AudioSource effectAudio;

    private bool invertSound = false;

    [SerializeField]
    private AudioClip[] bgm = new AudioClip[6];
    [SerializeField]
    private AudioClip[] effect = new AudioClip[7];

    private int muteState = 0;
    public GameObject muteButton;
    public GameObject muteButton2;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        myAudio = this.GetComponent<AudioSource>();

        CheckMute();
    }

    private void Update()
    {
        if (invertSound)
        {
            myAudio.pitch -= Time.deltaTime * 2f;

            if (myAudio.pitch <= 0f)
            {
                invertSound = false;
            }
        }
    }

    //효과음 볼륨을 BGM과 달리하기 위해 다른 Audio를 이용.
    public void PlayEffectSound(int _num)
    {
        effectAudio.PlayOneShot(effect[_num]);
    }

    public void ChangeBGM(int COLORNUM)
    {
        myAudio.clip = SetBGMClip(COLORNUM);
    }

    public void PlayBGM()
    {
        myAudio.Play();
    }
    public void PauseBGM()
    {
        myAudio.Pause();
    }
    public void StopBGM()
    {
        myAudio.Stop();
    }

    public void SlowBGM()
    {
        invertSound = true;

    }
    public void ModestBGM()
    {
        myAudio.pitch = 1f;
    }

    private AudioClip SetBGMClip(int _num)
    {
        switch (_num)
        {
            case 0:
                return bgm[0];
                break;
            case 1:
                return bgm[0];
                break;
            case 2:
                return bgm[1];
                break;
            case 3:
                return bgm[2];
                break;
            case 4:
                return bgm[3];
                break;
            case 5:
                return bgm[4];
                break;
            case 6:
                return bgm[5];
                break;
        }
        return null;
    }

    private void CheckMute()
    {
        muteState = PlayerPrefs.GetInt("MUTE");

        if(muteState == 0)
        {
            ActiveSound();
        }
        else
        {
            MuteSound();
        }
    }

    public void MuteSound()
    {
        myAudio.mute = true;
        effectAudio.mute = true;
        muteState = 1;

        PlayerPrefs.SetInt("MUTE", muteState);

        muteButton.SetActive(true);
        muteButton2.SetActive(false);
    }

    public void ActiveSound()
    {
        myAudio.mute = false;
        effectAudio.mute = false;
        muteState = 0;

        PlayerPrefs.SetInt("MUTE", muteState);

        muteButton.SetActive(false);
        muteButton2.SetActive(true);
    }

}
