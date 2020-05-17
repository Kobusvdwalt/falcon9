using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LowHealthEffect_scr : MonoBehaviour {

	Player_scr player;
	void Start () {
		
	}
	
	Color targetColor;
	void Update () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player").GetComponent<Player_scr>();
			return;
		}

		if (player.health < (float)player.maxHealth /2)
		{
			targetColor = new HSBColor(1, 1, 1, 0.8f- (float)player.health/ ((float)player.maxHealth /2)).ToColor();
		}
		else
		{
			targetColor = new HSBColor(1, 1, 1, 0).ToColor();
		}

		GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, targetColor, 0.2f);
	}
}
