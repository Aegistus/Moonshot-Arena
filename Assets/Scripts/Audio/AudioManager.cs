using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

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

	public void StartPlaying(string soundName)
	{
		Sound sound = Array.Find(sounds, item => item.name == soundName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

		sound.source.Play();
	}

	public void StartPlayingAtPosition(string soundName, Vector3 position)
    {
		Sound sound = Array.Find(sounds, item => item.name == soundName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));
		sound.source.minDistance = sound.minimunDistance;

		AudioSource.PlayClipAtPoint(sound.source.clip, position);
	}

	public void StopPlaying(string soundName)
	{
		Sound sound = Array.Find(sounds, item => item.name == soundName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

		sound.source.Stop();
	}
}
