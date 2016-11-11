using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{

    private AudioSource _audioSource;
    
    // Use this for initialization
	void Start ()
	{
	    _audioSource = GetComponent<AudioSource>();
        //Debug.Log("Audio channels =" + _audioSource.clip.channels);
    }
	
	// Update is called once per frame
	void Update ()
	{
        //_audioSource.time

    }
}
