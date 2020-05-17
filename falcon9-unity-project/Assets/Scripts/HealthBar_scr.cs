using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthBar_scr : MonoBehaviour {

	public GameObject bar;
	GameObject player;

	Vector2 targetSize;

	void Start () {
	}
	
	int count = 0;
	void Update () {
		while (player == null)
		{
			player = GameObject.FindWithTag("Player");
			return;
		}

		float percentage = (float)player.GetComponent<Player_scr>().health / (float)player.GetComponent<Player_scr>().maxHealth;
		percentage = Mathf.Clamp(percentage, 0, 1);
		targetSize = new Vector2(percentage * 300, 20);

		bar.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(bar.GetComponent<RectTransform>().sizeDelta, targetSize, 0.2f);
		bar.transform.localScale = Vector3.Lerp(bar.transform.localScale, Vector3.one, 0.2f);

		if (count < 0)
		{
			count = 5;
			bar.GetComponent<RectTransform>().sizeDelta += new Vector2(6f, 0);
		}
		count --;
	}

	public void Shake ()
	{
		bar.transform.localScale = new Vector3(bar.transform.localScale.x, bar.transform.localScale.y * 2f, 0);
		bar.transform.localScale = new Vector3(bar.transform.localScale.x, Mathf.Clamp(bar.transform.localScale.y, 0, 3f), 1);
	}
}
