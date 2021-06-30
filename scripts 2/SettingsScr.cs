using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScr : MonoBehaviour
{
    public static SettingsScr instance;

    public bool JoyOnLeft;
    public int GameSpeed;
	public bool SoundOn=true;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(instance.gameObject);
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void JoyLeft()
    {
		JoyOnLeft = true;
    }
	public void JoyRight()
	{
		JoyOnLeft = false;
	}

	public void SoundOff()
    {
		GameObject.Find("AudioManager").GetComponent<AudioManager>().SoundActive = false;
		GameObject.Find("AudioManager").GetComponent<AudioManager>().PauseSound("mainmusic");

	}
	public void SoundOnn()
	{
		GameObject.Find("AudioManager").GetComponent<AudioManager>().SoundActive = true;
		GameObject.Find("AudioManager").GetComponent<AudioManager>().UnPauseSound("mainmusic");
	}

	public void SpeedSlow()
    {
		GameSpeed = -1;
    }
	public void SpeedFast()
	{
		GameSpeed = 1;
	}
	public void SpeedNormal()
	{
		GameSpeed = 0;
	}
}
