using UnityEngine;
using System.Collections;

public class TimedDestroy_scr : MonoBehaviour {

	public float time;
	void Start () {
		Invoke("Destroy", time);
	}
	
	// Update is called once per frame
	void Destroy () {
		Destroy(gameObject);
	}
}
