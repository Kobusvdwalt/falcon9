using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUpBar_scr : MonoBehaviour {


	GameObject player;
	GameObject bar;
	GameObject leftCap;
	GameObject rightCap;

	void Start () {
		bar = transform.Find("Bar").gameObject;
		leftCap = bar.transform.Find("LeftCap").gameObject;
		rightCap = bar.transform.Find("RightCap").gameObject;
	}
	
	int count = 5;
	int energyBuffer;
	void FixedUpdate () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
			return;
		}

		if (player.GetComponent<PowerUp_scr>())
		{
			if (bar.activeSelf == false)
			{
				bar.SetActive(true);
			}


			int energy = player.GetComponent<PowerUp_scr>().energy;
			int maxEnergy = player.GetComponent<PowerUp_scr>().maxEnergy;
			bar.GetComponent<RectTransform>().sizeDelta = new Vector2((float)energy/(float)maxEnergy * 1600, 8);



			if (energyBuffer != player.GetComponent<PowerUp_scr>().energy)
			{
				
				energyBuffer = player.GetComponent<PowerUp_scr>().energy;

				Color randomColor = new HSBColor(Random.Range(0, 1f), 1, 1, 1).ToColor();
				bar.GetComponent<Image>().color = randomColor;
				leftCap.GetComponent<Image>().color = randomColor;
				rightCap.GetComponent<Image>().color = randomColor;

				if (count < 0)
				{
					count = 5;
					bar.transform.localScale = new Vector3(bar.transform.localScale.x, bar.transform.localScale.y * 5, bar.transform.localScale.z);
				}
			}
			else
			{
				bar.GetComponent<Image>().color = Color.Lerp(bar.GetComponent<Image>().color, Color.white, 0.1f);
				leftCap.GetComponent<Image>().color = Color.Lerp(leftCap.GetComponent<Image>().color, Color.white, 0.1f);
				rightCap.GetComponent<Image>().color = Color.Lerp(rightCap.GetComponent<Image>().color, Color.white, 0.1f);
			}
			count --;

			bar.transform.localScale = Vector3.Lerp(bar.transform.localScale, new Vector3(1, 1, 1), 0.3f);

		}
		else
		{
			if (bar.activeSelf == true)
			{
				bar.SetActive(false);
			}
		}


	}
}
