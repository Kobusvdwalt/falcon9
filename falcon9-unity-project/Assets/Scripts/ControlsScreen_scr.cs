using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlsScreen_scr : MonoBehaviour {

	public AudioClip navClip;
	public AudioClip selClip;
	void Update () {
		if (Input_scr.OnUIBackwardPressed())
		{
			SceneManager.LoadScene("Menu");
		}
	}
}
