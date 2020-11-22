using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public int numOfPositionalSources = 100;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	private Queue<AudioSource> positionalSources = new Queue<AudioSource>();

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			//DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
		Transform positionalSourceParent = new GameObject("Positional Audio Sources").transform;
        for (int i = 0; i < numOfPositionalSources; i++)
        {
			AudioSource newPositional = new GameObject().AddComponent<AudioSource>();
			newPositional.transform.parent = positionalSourceParent;
			newPositional.spatialBlend = 1;
			positionalSources.Enqueue(newPositional);
        }
	}

	public void StartPlaying(string soundName)
	{
		Sound sound = Array.Find(sounds, item => item.name == soundName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
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
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}
		
		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));
		sound.source.minDistance = sound.minimunDistance;

		AudioSource source = positionalSources.Dequeue();
		source.transform.position = position;
		source.clip = sound.clip;
		source.Play();
		positionalSources.Enqueue(source);
	}

	public void StopPlaying(string soundName)
	{
		Sound sound = Array.Find(sounds, item => item.name == soundName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + soundName + " not found!");
			return;
		}

		sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
		sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

		sound.source.Stop();
	}
}
