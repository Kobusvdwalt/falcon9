using UnityEngine;
using System.Collections;

public class PowerUp_scr : MonoBehaviour {

	public int energy;
	public int maxEnergy;

	protected void Update ()
	{
		if (energy < 0)
		{
			Destroy(this);
		}
	}
}
