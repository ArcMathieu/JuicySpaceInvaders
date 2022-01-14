using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds = new Sound[0];
    public Sound[] music = new Sound[0];
    public Sound currentMusic;

    [SerializeField] Slider VolumeMusicSlider;
    [SerializeField] Slider VolumeSFXSlider;
    [SerializeField] Slider ValueShakeSlider;

    private static SoundManager instance;
    public static SoundManager i
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = VolumeSFXSlider.value;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound m in music) {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = VolumeMusicSlider.value;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }
        currentMusic = music[0];
        currentMusic.source.Play();
    }

    void Update() {
        if (!currentMusic.source.isPlaying) {
            if (currentMusic.leadTo) {
                currentMusic = Array.Find(music, sound => sound.name == music[0].nextSound);
                currentMusic.source.Play();
            } else if(currentMusic.loop){
                currentMusic.source.Play();
            }
        }
    }

    public void Play(string name)
    {
        Sound s;
        s = name == "Music" ? music[0] : Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("pas trouvé : " + name);
            return;
        }
        s.source.Play();
    }

    public void ChangeVolumeMusic()
    {
        foreach (Sound m in music)
        {
            m.source.volume = VolumeMusicSlider.value;
        }
    }
    public void ChangeVolumeSFX()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = VolumeSFXSlider.value;
        }
    }
    public float ShakeMultiplier;
    public void ChangeValueShake()
    {
        ShakeMultiplier = ValueShakeSlider.value;
    }
}
