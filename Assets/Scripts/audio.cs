using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour {

    public float timer = 0;
    public float start;
    public float end;
    public AudioSource me;
    float poopy = 1f;

	// Use this for initialization
	void Start () {
        timer = start;
        me.time = start;
        poopy = PlayerPrefs.GetFloat("volume",1);
        me.GetComponent<AudioSource>().volume = poopy;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= end)
        {
                me.time = start;
                timer = start;
        }
	}

    public void resetaudio(float s, float e)
    {
        start = s;
        end = e;
        timer = start;
        me.time = start;
        me.Play();
    }

    public void setaudio(string s)
    {
        me.clip = Resources.Load<AudioClip>("Music/" + s);
    }
}
