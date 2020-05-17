using UnityEngine;
using System.Collections;

public class Explosion_scr : MonoBehaviour {

	int count;
	void Start () {
		count = Random.Range(4, 8);
		transform.localScale = Vector3.one * Random.Range(1, 2.2f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		count --;
		if (count < 0)
		{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
		count --;

		if (count < -Random.Range(4, 8))
		{
			Destroy(gameObject);
		}
	}
}
