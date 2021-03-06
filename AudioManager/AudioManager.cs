using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	public bool SoundActive=true;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		if(SoundActive == false)
        {
			return;
        }
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void PauseSound(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Pause();
	}
	public void UnPauseSound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.UnPause();
	}
	public bool isSoundPlaying(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		return s.source.isPlaying;
	}
}
