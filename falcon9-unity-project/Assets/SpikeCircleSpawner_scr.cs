using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpikeCircleSpawner_scr : MonoBehaviour {

	public GameObject spikeCirclePrefab;
	List<GameObject> circles = new List<GameObject>(10);
	void Start () {
		for (int i=0; i < Random.Range(3, 6); i ++)
		{
			GameObject a = Instantiate(spikeCirclePrefab);
			a.transform.position = transform.position + new Vector3(Random.Range(-20f, 20f), Random.Range(-10f, 10f), 0);
		}
	}
	
	public void Destruct ()
	{
		while (circles.Count > 0)
		{
			Destroy(circles[0]);
			circles.RemoveAt(0);
		}
		Destroy(gameObject);
	}
}
