using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBar_scr : MonoBehaviour {

	Player_scr player;
	void Update () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player").GetComponent<Player_scr>();
			return;
		}

		GetComponent<Text>().text = player.score.ToString();

		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.3f);
		transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, 0, 0.3f));
	}

	public void Shake ()
	{
		transform.localScale *= 1.5f;
		transform.localEulerAngles += new Vector3(0, 0, Random.Range(-15, 15));
	}
}
