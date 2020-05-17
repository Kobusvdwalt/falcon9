using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheDirector_scr : MonoBehaviour {

	public float planetSpawnrate;
	public GameObject wormholeSpewPrefab;
	public GameObject playerPrefab;
	public GameObject planetPrefab;
	public GameObject spikeCirclePrefab;

	public List<GameObject> planets;
	public List<GameObject> spikeCircles;
	int planetsDestroyed = 0;

	GameObject player;
	int planetSpawnCount = 0;

	void Start () {
		GameObject a = Instantiate(wormholeSpewPrefab);
		a.transform.position = new Vector3(0, 0, 0);

		player = Instantiate(playerPrefab);
		player.transform.position = new Vector3(0, 0, 0);
		player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1), Random.Range(-1f, 1)) * 8, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (planetSpawnCount < 0)
		{
			planetSpawnCount = Mathf.RoundToInt(planetSpawnrate / Time.fixedDeltaTime);

			// Remove destroyed planets
			int j=0;
			while(j < planets.Count)
			{
				if (planets[j] == null)
				{
					planets.RemoveAt(j);
					planetsDestroyed ++;
				}
				else
				{
					j ++;
				}
			}

			// Planet wrapping
			for (int i=0; i < planets.Count; i ++)
			{
				if (Vector2.Distance((Vector2)player.transform.position, (Vector2)planets[i].transform.position) > 100 + 5 * planetsDestroyed)
				{
					Destroy(planets[i]);
					planets.RemoveAt(i);
				}
			}

			// Spike wrapping
			for (int i=0; i < spikeCircles.Count; i ++)
			{
				if (Vector2.Distance((Vector2)player.transform.position, (Vector2)spikeCircles[i].transform.position) > 100 + 5 * planetsDestroyed)
				{
					spikeCircles[i].GetComponent<SpikeCircleSpawner_scr>().Destruct();
					spikeCircles.RemoveAt(i);
				}
			}

			while (planets.Count < planetsDestroyed +2)
			{
				GameObject a = Instantiate(planetPrefab);
				float angle = Random.Range(0, 360);
				Vector3 position = new Vector3(transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad)* 35 + 5 * planetsDestroyed,
											   transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad)* 35 + 5 * planetsDestroyed, 0);
				a.transform.position = position;
				a.GetComponent<Planet_scr>().size = Random.Range(5, 35);
				a.GetComponent<Planet_scr>().Generate();
				planets.Add(a);
			}

			while (spikeCircles.Count < 1)
			{
				GameObject a = Instantiate(spikeCirclePrefab);
				float angle = Random.Range(0, 360);
				Vector3 position = new Vector3(transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad)* 35 + 5 * planetsDestroyed,
											   transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad)* 35 + 5 * planetsDestroyed, 0);

				a.transform.position = position;
				spikeCircles.Add(a);
			}
		}
		planetSpawnCount --;


	}
}
