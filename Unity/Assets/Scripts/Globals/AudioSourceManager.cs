using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceManager : MonoBehaviour {

	struct SoundClip
	{
		public AudioClip clip;
		public bool loop;
		public float vol;
	}

	Queue<SoundClip> playQueue;
	
	public void add(AudioClip clip, bool loop, float vol = 1f)
	{
		if (playQueue == null) playQueue = new Queue<SoundClip>();
		SoundClip sClip = new SoundClip();
		sClip.clip = clip;
		sClip.loop = loop;
		sClip.vol = vol;
		playQueue.Enqueue(sClip);
		if (playQueue.Count == 1)
		{
			StartCoroutine(playSound(sClip));
		}
	}
	
	public void playMusic(AudioClip clip, float vol = 1f)
	{
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			gameObject.AddComponent<AudioSource>();
			audioSource = gameObject.GetComponent<AudioSource>();
		}
		else 
		{
			audioSource.Stop();
		}
	    audioSource.playOnAwake = false;
	    audioSource.clip = clip;
	    audioSource.loop = true;
	    audioSource.volume = vol;
	    audioSource.Play();
	}
	
	IEnumerator playSound(SoundClip clip)
	{
		float time = clip.clip.length;
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			gameObject.AddComponent<AudioSource>();
			audioSource = gameObject.GetComponent<AudioSource>();
		}
		audioSource.playOnAwake = false;
		audioSource.clip = clip.clip;
		audioSource.loop = clip.loop;
		audioSource.volume = clip.vol;
		audioSource.Play();
		if (! clip.loop)
		{
			yield return new WaitForSeconds(time);
			if (playQueue.Count == 1)
			{
				Destroy(audioSource);
				playQueue.Dequeue();
			}
			else
			{
				playQueue.Dequeue();
				playSound(playQueue.Peek());
			}
		}
		
	}
	
	public bool isActive()
	{
		if (playQueue == null) playQueue = new Queue<SoundClip>();
		return (playQueue.Count != 0);
	}
	public bool isPlaying(AudioClip clip)
	{
		if (playQueue == null) playQueue = new Queue<SoundClip>();
		if (! isActive()) return false;
		else return (playQueue.Peek().clip == clip);
	}
	public bool isLooping()
	{
		if (playQueue == null) playQueue = new Queue<SoundClip>();
		if (! isActive()) return false;
		else return playQueue.Peek().loop;
	}
	public int getCount()
	{
		if (playQueue == null) playQueue = new Queue<SoundClip>();
		return playQueue.Count;
	}
}