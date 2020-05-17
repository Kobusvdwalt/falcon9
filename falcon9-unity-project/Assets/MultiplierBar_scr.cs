using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiplierBar_scr : MonoBehaviour {

	GameObject bar;
	GameObject leftCap;
	GameObject rightCap;
	GameObject text;
	Player_scr player;
	void Start () {
		bar = transform.Find("Bar").gameObject;
		leftCap = bar.transform.Find("LeftCap").gameObject;
		rightCap = bar.transform.Find("RightCap").gameObject;
		text = transform.Find("Multiplier Text").gameObject;
	}

	Vector2 targetSize;
	int count = 5;
	int count2 = 3;
	void Update () {
		if (player == null)
		{
			player = GameObject.FindWithTag("Player").GetComponent<Player_scr>();
			return;
		}

		if (player.multiplier == player.maxMultiplier)
		{
			text.GetComponent<Text>().text = "MAX";
		}
		else
		if (player.multiplier == 1)
		{
			text.GetComponent<Text>().text = "";
		}
		else
		{
			text.GetComponent<Text>().text = "x"+player.multiplier.ToString();
		}



		float normilizedMultiCooldown = (float)player.multiplierCooldown / (float)player.maxMultiplierCooldown;

		// Juice

		if (count2 < 0)
		{
			count2 = 2;
			if (player.multiplier == player.maxMultiplier)
			{
				Color randomColor = new HSBColor(Random.Range(0, 1f), 1, 1, 1).ToColor();
				text.GetComponent<Text>().color = randomColor;
				text.GetComponent<Outline>().effectColor = randomColor;

//				randomColor = new HSBColor(Random.Range(0, 1f), 1, 1, 1).ToColor();
//				bar.GetComponent<Image>().color = randomColor;
//				leftCap.GetComponent<Image>().color = randomColor;
//				rightCap.GetComponent<Image>().color = randomColor;
			}
			else
			{
				text.GetComponent<Text>().color = Color.white;
				text.GetComponent<Outline>().effectColor = Color.white;
				bar.GetComponent<Image>().color = Color.white;
				leftCap.GetComponent<Image>().color = Color.white;
				rightCap.GetComponent<Image>().color = Color.white;
			}
		}
		count2--;
		text.transform.localScale = Vector3.Lerp(text.transform.localScale, Vector3.one, 0.2f);
		text.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(text.transform.localEulerAngles.z, 0, 0.2f));

		bar.GetComponent<RectTransform>().sizeDelta = 
			Vector2.Lerp(bar.GetComponent<RectTransform>().sizeDelta, new Vector2(normilizedMultiCooldown * 1600, 0), 0.3f);
		bar.transform.localScale = Vector3.Lerp(bar.transform.localScale, Vector3.one, 0.3f);
		if (count < 0)
		{
			count = 6;
			bar.GetComponent<RectTransform>().sizeDelta += new Vector2(10, 3);
			if (player.multiplier == 30)
			{
				text.transform.localScale *= 1.2f;
				text.transform.localEulerAngles += new Vector3(0, 0, Random.Range(-5, 5));
			}
		}
		count --;
	}

	public void Shake ()
	{
		bar.transform.localScale = new Vector3(1, bar.transform.localScale.y * 3, 1);
		text.transform.localScale *= 1.5f;
		text.transform.localEulerAngles += new Vector3(0, 0, Random.Range(-10, 10));
	}
}
