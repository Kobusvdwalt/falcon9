using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HighScore_scr : MonoBehaviour {

	Player_scr player;
	bool highscore = false;
	void Start () {
		HighScores_scr.LoadHighScores();
	}
	
	int count = 0;
	void Update () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player").GetComponent<Player_scr>();
			return;
		}

		if (player.score > HighScores_scr.scores[4] && highscore == false)
		{
			highscore = true;
		}

		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.2f);
		GetComponent<Text>().text = HighScores_scr.scores[4].ToString();

		if (highscore)
		{
			GetComponent<Text>().color = new HSBColor(Random.Range(0, 1f), 1, 1, 1).ToColor();
			PlayerPrefs.SetInt("Score", player.score);

			if (count < 0)
			{
				count = 7;
				transform.localScale *= 1.2f;
			}
			count --;

			GetComponent<Text>().text = "HIGHSCORE";
		}



	}
}
