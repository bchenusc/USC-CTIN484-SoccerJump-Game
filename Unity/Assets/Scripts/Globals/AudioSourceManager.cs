using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceManager : MonoBehaviour {

	Queue<AudioSource> playQueue;
	
	bool playing = false;
	
	void Update()
	{
		if (playQueue == null) playQueue = new Queue<AudioSource>();
		else if (playQueue.Count > 0)
		{
			AudioSource source = playQueue.Peek();
			Debug.Log (source.isPlaying);
			if (! source.isPlaying)
			{
				if (! playing)
				{
					source.Play();
					playing = true;
				}
				else
				{
					if (playQueue.Count > 0) Destroy(playQueue.Dequeue());
					playing = false;
				}
			}
		}
	}
	
	public void add(AudioClip clip, bool loop, float vol = 1f)
	{
		if (playQueue == null) playQueue = new Queue<AudioSource>();
		AudioSource audioSource = null;
		gameObject.AddComponent<AudioSource>();
		AudioSource[] sources = gameObject.GetComponents<AudioSource>();
		foreach (AudioSource source in sources)
		{
			if (source.clip == null) { audioSource = source; break; }
		}
		if (audioSource != null)
		{
			audioSource.playOnAwake = false;
			audioSource.clip = clip;
			audioSource.loop = loop;
			audioSource.volume = vol;
			playQueue.Enqueue(audioSource);
		}
	}
	
	public bool isActive()
	{
		if (playQueue == null) playQueue = new Queue<AudioSource>();
		return (playQueue.Count != 0);
	}
	public bool isPlaying(AudioClip clip)
	{
		if (! isActive()) return false;
		else return (playQueue.Peek().clip == clip);
	}
	public bool isLooping()
	{
		if (! isActive()) return false;
		else return playQueue.Peek().loop;
	}
	public int getCount()
	{
		if (playQueue == null) playQueue = new Queue<AudioSource>();
		return playQueue.Count;
	}
}