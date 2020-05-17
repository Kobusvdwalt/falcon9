using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_scr : MonoBehaviour {

	public int menuPos;
	public AudioClip navClip;
	public AudioClip selClip;
	public GameObject selector;
	public GameObject[] menuText;

	int menuPosBuffer;
	Vector3 selectorTargetPos;
	Vector2 selectorTargetSize;
	Vector3 selectorStartPos;
	void Start () {
		Cursor.visible = false;

		menuPos = 0;
		menuPosBuffer = -1;
		selectorStartPos = selector.transform.localPosition;
	}
	float timer;
	void Update () {

		if (Input_scr.OnUIForwardPressed())
		{
			StartCoroutine("MenuSelection");
		}

		if (menuPos != menuPosBuffer)
		{
			menuPosBuffer = menuPos;

			for (int i=0; i < menuText.Length; i ++)
			{
				menuText[i].GetComponent<Text>().color = Color.white;
			}
			menuText[menuPos].GetComponent<Text>().color = Color.black;
			selectorTargetPos = new Vector3(0, selectorStartPos.y + menuPos*-35, 0);
			selectorTargetSize = new Vector2(menuText[menuPos].GetComponent<RectTransform>().sizeDelta.x, 35);

			GetComponent<AudioSource>().clip = navClip;
			GetComponent<AudioSource>().Play();
		}
		selector.transform.localPosition = Vector3.Lerp(selector.transform.localPosition, selectorTargetPos, 0.3f);
		selector.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(selector.GetComponent<RectTransform>().sizeDelta, selectorTargetSize, 10 * Time.deltaTime);

		if (menuPos < menuText.Length-1 && Input_scr.OnUIDownPressed())
		{
			menuPos++;
		}
		if (menuPos > 0 && Input_scr.OnUIUpPressed())
		{
			menuPos--;
		}

		// Visual Juice
		if (timer < 0)
		{
			selector.GetComponent<RectTransform>().sizeDelta *= 1.2f;
			timer = 0.5f;
		}
		timer -= Time.deltaTime;
	}

	IEnumerator MenuSelection ()
	{
		GetComponent<AudioSource>().clip = selClip;
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(0.1f);

		if (menuPos == 0)
		{
			SceneManager.LoadScene("Level");
		}
		if (menuPos == 1)
		{
			SceneManager.LoadScene("Highscores");
		}
		if (menuPos == 2)
		{
			SceneManager.LoadScene("Controls");
		}
		if (menuPos == 3)
		{
			Application.Quit();
		}
	}
}