using UnityEngine;
using System.Collections;

public class ExplosionCluster_scr : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject audioGOPrefab;
	public AudioClip explosionClip;

	void Start () {
		for (int i=0; i < 4; i ++)
		{
			StartCoroutine("SpawnExplosion");
		}
	}
	IEnumerator SpawnExplosion ()
	{
		yield return new WaitForSeconds(Random.Range(0, 0.1f));

		GameObject a = Instantiate(explosionPrefab);
		a.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

		GameObject b = Instantiate(audioGOPrefab);
		b.transform.position = a.transform.position;
		b.GetComponent<AudioSource>().clip = explosionClip;
		b.GetComponent<AudioSource>().Play();
	}
}
