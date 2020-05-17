using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver_scr : MonoBehaviour {

	public GameObject txtGame;
	public GameObject txtOver;
	public GameObject txtNext;

	GameObject player;

	bool gameover;
	void Start () {
		Hide();
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
			return;
		}
		if (Input_scr.OnUIForwardPressed() && gameover == true)
		{			

			SceneManager.LoadScene("Level");

			Camera_scr.timeScale = 1;
			Time.timeScale = 1;
		}

		if (txtGame.activeSelf)
		{
			txtGame.transform.localPosition = Vector3.Lerp(txtGame.transform.localPosition, Vector3.zero, 0.1f);
		}
		if (txtOver.activeSelf)
		{
			txtOver.transform.localPosition = Vector3.Lerp(txtOver.transform.localPosition, Vector3.zero, 0.1f);
		}
		if (txtNext.activeSelf)
		{
			txtNext.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(txtNext.GetComponent<RectTransform>().anchoredPosition, Vector3.zero, 0.1f);
		}

	}

	public void Show ()
	{
		gameover = true;
		for (int i=0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
		GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().Stop();
		GameObject.FindWithTag("MainCamera").GetComponent<Camera_scr>().Pause(int.MaxValue);

		HighScores_scr.LoadHighScores();
		if (player.GetComponent<Player_scr>().score > HighScores_scr.scores[4])
		{
			HighScores_scr.newHighScore = player.GetComponent<Player_scr>().score;
			Camera_scr.timeScale = 1;
			Time.timeScale = 1;
			SceneManager.LoadScene("Highscores");
		}
	}

	public void Hide ()
	{
		gameover = false;
		for (int i=0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
