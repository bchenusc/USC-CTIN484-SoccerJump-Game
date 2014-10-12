using UnityEngine;
using System.Collections.Generic;

/* adapted from https://dl.dropboxusercontent.com/u/96068012/Tutorials/AudioManager/AudioManagerTutorial.pdf */

public class SoundManager : Singleton {

	private List<GameObject> sounds = new List<GameObject>();
	GameObject sound1;
	GameObject sound2;
	GameObject sound3;
	GameObject sound4;
	GameObject sound5;
	GameObject sound6;
	GameObject sound7;
	GameObject sound8;
	GameObject sound9;
	GameObject sound10;
	
	void Start()
	{
		sounds.Clear();
		sound1 = GameObject.Find("SoundSource1");
		sounds.Add (sound1);
		sound2 = GameObject.Find("SoundSource2");
		sounds.Add (sound2);
		sound3 = GameObject.Find("SoundSource3");
		sounds.Add (sound3);
		sound4 = GameObject.Find("SoundSource4");
		sounds.Add (sound4);
		sound5 = GameObject.Find("SoundSource5");
		sounds.Add (sound5);
		sound6 = GameObject.Find("SoundSource6");
		sounds.Add (sound6);
		sound7 = GameObject.Find("SoundSource7");
		sounds.Add (sound7);
		sound8 = GameObject.Find("SoundSource8");
		sounds.Add (sound8);
		sound9 = GameObject.Find("SoundSource9");
		sounds.Add (sound9);
		sound10 = GameObject.Find("SoundSource10");
		sounds.Add (sound10);
	}
	
	// temp hack - start
	public AudioClip goSound;
	public AudioClip countSound;
	public AudioClip scoreSound;
	public AudioClip jumpSound;
	public void play(string clip)
	{
		playSound(findClip(clip), false);
	}
	public void play(string clip, bool loop, float vol = 1)
	{
		playSound(findClip(clip), false, vol);
	}
	AudioClip findClip(string clip)
	{
		switch (clip)
		{
			case "count": return countSound;
			case "go": return goSound;
			case "score": return scoreSound;
			case "jump": return jumpSound;
		}
		return null;
	}
	// temp hack - end
	
	public void playSound(AudioClip clip)
	{
		playSound(clip, false);
	}
	public void playSound(AudioClip clip, bool loop, float vol = 1)
	{
		if (sounds.Count == 0 || sounds[1] == null) Start ();
		foreach(GameObject sound in sounds)
		{
			if (sound.GetComponent<AudioSource>() == null)
			{
				sound.AddComponent<AudioSource>();
				AudioSource audioSource = sound.GetComponent<AudioSource>();
				audioSource.playOnAwake = false;
				audioSource.rolloffMode = AudioRolloffMode.Linear; // not sure what this does
				audioSource.clip = clip;
				audioSource.loop = loop;
				audioSource.volume = vol;
				audioSource.Play();
				if (! loop) Destroy(audioSource, clip.length);
				break;
			}
		}
	}
	
	public void stop(AudioClip clip) {
		foreach(GameObject sound in sounds) {
			AudioSource source = sound.GetComponent<AudioSource>();
			if (source.clip.Equals(clip))
			{
				Destroy (source);
				break; // if we only have one looping clip of a kind at any given point
			}
		}
	}
	
	#region Inherited functions
	protected override void DestroyIfMoreThanOneOnObject(){
		if (transform.GetComponents<InputManager>().Length > 1){
			Debug.Log ("Destroying Extra " + this.GetType() + " Attachment");
			DestroyImmediate(this);
		}
	}
	#endregion
	
}