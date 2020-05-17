using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScores_scr : MonoBehaviour {

	public GameObject tableParent;
	public GameObject newHighScoreGO;
	public GameObject newHighScoreTxt;
	public GameObject inputField;
	public GameObject txtNumberPrefab;
	public GameObject txtScorePefab;
	public GameObject txtNamePrefab;

	public static int newHighScore = 0;
	public static string[] names = new string[5];
	public static int[] scores = new int[5];

	void Start () {
		if (newHighScore > 0)
		{
			newHighScoreGO.SetActive(true);
		}
		else
		{
			GenerateTable();
		}
	}
	
	int count = 8;
	void Update () {
		if (Input.GetKeyDown(KeyCode.X))
		{
			SetupDefaultHighScores();
			SceneManager.LoadScene("Highscores");
		}
		if (count < 0)
		{
			count = 12;
			newHighScoreTxt.transform.localScale *= 1.05f;
		}
		count --;

		newHighScoreTxt.transform.localScale = Vector3.Lerp(newHighScoreTxt.transform.localScale, Vector3.one, 0.1f);

		if (Input_scr.OnUIBackwardPressed())
		{
			SceneManager.LoadScene("Menu");
		}
	}

	void GenerateTable ()
	{
		// prepare array
		LoadHighScores();

		for (int i=0; i < 5; i ++)
		{
			// Number
			GameObject a = Instantiate(txtNumberPrefab);
			a.transform.SetParent(tableParent.transform, false);
			a.GetComponent<RectTransform>().anchoredPosition = new Vector2(-430, 200 - 60 * i);
			a.GetComponent<Text>().text = (i+1).ToString() + ".";

			// Score
			GameObject b = Instantiate(txtScorePefab);
			b.transform.SetParent(tableParent.transform, false);
			b.GetComponent<RectTransform>().anchoredPosition = new Vector2(-380, 200 - 60 * i);
			b.GetComponent<Text>().text = scores[i].ToString();

			// Name
			GameObject c = Instantiate(txtNamePrefab);
			c.transform.SetParent(tableParent.transform, false);
			c.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 200 - 60 * i);
			c.GetComponent<Text>().text = names[i];
		}
	}

	public void NameComplete ()
	{
		LoadHighScores();

		SetNewScore(newHighScore, inputField.GetComponent<InputField>().text);

		SaveHighScores();

		newHighScoreGO.SetActive(false);
		newHighScore = 0;

		GenerateTable();
	}

	public static void SetNewScore (int score, string name)
	{
		if (score > scores[4])
		{
			scores[4] = score;
			names[4] = name;
			SortHighScores();
		}
	}

	public static void SortHighScores ()
	{
		for (int i=0; i < 5; i ++)
		{
			for (int j=0; j < 5; j++)
			{
				if (scores[i] > scores[j])
				{
					int bucket1;
					bucket1 = scores[i];
					scores[i] = scores[j];
					scores[j] = bucket1;

					string bucket2;
					bucket2 = names[i];
					names[i] = names[j];
					names[j] = bucket2;
				}
			}
		}
	}
	public static void LoadHighScores ()
	{
		if (PlayerPrefs.HasKey("HighScores") == false)
		{
			SetupDefaultHighScores();
		}

		string highScoreTable = PlayerPrefs.GetString("HighScores");
		string[] tempRecordArr = highScoreTable.Split('|');

		for (int i=0; i < 5; i ++)
		{
			string[] tempRecord = tempRecordArr[i].Split(',');
			scores[i] = int.Parse(tempRecord[0]);
			names[i] = tempRecord[1];
		}
	}

	public static void SaveHighScores ()
	{
		string highScoreTable = "";

		for (int i=0; i < 5; i++)
		{
			highScoreTable += scores[i].ToString() +","+ names[i] +"|";
		}

		PlayerPrefs.SetString("HighScores", highScoreTable);
		PlayerPrefs.Save();
	}

	public static void SetupDefaultHighScores ()
	{
		names[0] = "Adolf 'Kill joy' Hitler";
		scores[0] = 600000;

		names[1] = "Donald";
		scores[1] = 300000;

		names[2] = "Pieter";
		scores[2] = 100000;

		names[3] = "John";
		scores[3] = 50000;

		names[4] = "Richard";
		scores[4] = 20000;

		SaveHighScores();
	}
}
