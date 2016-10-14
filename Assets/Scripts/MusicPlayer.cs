using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	public AudioClip menu;
	public AudioClip game;
	public AudioClip end;

	private AudioSource music;

	void Awake () {
		Debug.Log ("Mus player Awake " + GetInstanceID());
		if (instance != null) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = menu;
			music.loop = true;
			music.Play();
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Mus player Start " + GetInstanceID());

	}
	
	void OnLevelWasLoaded (int level) {
		Debug.Log("Music player loaded level "+ level);
		music.Stop ();
		if (level == 0) music.clip = menu;
		if (level == 1) music.clip = game;
		if (level == 2) music.clip = end;
		music.loop = true;
		music.Play();
	}
}
