using UnityEngine;
using System.Collections;

public class SpikeCircle_scr : MonoBehaviour {

	public GameObject spikePrefab;
	float size;
	float lerpRate;
	void Start () {
		size = Random.Range(3, 5);
		lerpRate = Random.Range(0.02f, 0.1f);
		transform.localScale = new Vector3(size, size, size);

		float circ = 2 * Mathf.PI * size;
		int spikeCount = Mathf.RoundToInt(circ);

		for (int i=0; i < spikeCount; i ++)
		{
			GameObject a = Instantiate(spikePrefab);
			a.transform.SetParent(transform, true);
			float spikeAngle = 360f/(float)spikeCount * i;
			a.transform.localPosition = new Vector3(Mathf.Cos(spikeAngle * Mathf.Deg2Rad), Mathf.Sin(spikeAngle * Mathf.Deg2Rad), 0) / 2;
			a.transform.localEulerAngles = new Vector3(0, 0, spikeAngle-90);
		}

		transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
	}

	// Update is called once per frame
	void Update () {
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(size, size, size), 0.05f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.GetComponent<Player_scr>().DoDamage(80);
		}
		if (col.tag == "Enemy")
		{
			if (col.GetComponent<EnemyShip_scr>())
			{
				col.GetComponent<EnemyShip_scr>().DoDamage(10);
			}
			if (col.GetComponent<FollowEnemy_scr>())
			{
				col.GetComponent<FollowEnemy_scr>().DoDamage(10);
			}
		}
	}
}
