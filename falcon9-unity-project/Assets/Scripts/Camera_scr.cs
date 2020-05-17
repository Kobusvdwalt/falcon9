using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Camera_scr : MonoBehaviour {
	
	public static float timeScale = 1;

	public float positionSpeed;
    public float aimSpeed;
    public float aimStrength;
    public float screenAngleSpeed;
    public GameObject particles;
	GameObject player;
    Vector3 playerPos;
    Vector3 aimOffset;
    Vector3 ssOffset;
    Vector3 finalPos;

	public List<Vector3> ssPositionsActual;
    public List<Vector3> ssPositionsSmoothed;
	List<float> ssSpeeds;

	int pauseCount =0;
	void Start () {
		director = GetComponent<TheDirector_scr>();
	
        ssPositionsActual = new List<Vector3>(10);
        ssSpeeds = new List<float>(10);
	}
	
	int count;
	Color targetColor;
	TheDirector_scr director;
	Vector3 planetOffsetSmoothed;

	void Update () {
		if (count < 0)
		{
			count = 40;
			targetColor = new HSBColor(Random.Range(0, 1f), 0.65f, 0.45f).ToColor();
		}
		count --;
		GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, targetColor, 0.05f);
		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
			return;
		}
		// Hitpause
		if (pauseCount < 0)
		{
			Time.timeScale = timeScale;
		}
		pauseCount --;

		// Basic target tracking
        playerPos = Vector3.Lerp(playerPos, player.transform.position, positionSpeed);

		// Aim adjustment
        float aimAngle = player.transform.localEulerAngles.z * Mathf.Deg2Rad;
        aimOffset = Vector3.Lerp(aimOffset, new Vector3(Mathf.Cos(aimAngle), Mathf.Sin(aimAngle), 0) * aimStrength, aimSpeed);

		// Screen angle adjustment
        transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.localEulerAngles.z, 0, screenAngleSpeed));

        // Planet focus adjustment
        Vector3 planetOffsetActual = Vector3.zero;
        int planetCount = 0;
       	for (int i=0; i < director.planets.Count; i ++)
       	{
			if (director.planets[i])
			{
				Vector3 delta = (player.transform.position - director.planets[i].transform.position);
				delta /= Mathf.Clamp(Vector3.Distance(director.planets[i].transform.position, player.transform.position) * 0.2f, 1, 1000);
				planetOffsetActual -= delta;
				planetCount ++;	       		
			}
       	}
		if (planetCount > 0)
		{
			planetOffsetSmoothed = Vector3.Lerp(planetOffsetSmoothed, planetOffsetActual/planetCount, 0.2f);
		}

		// Screenshake adjustment
        ssOffset = Vector3.zero;
        for (int i=ssSpeeds.Count-1; i > 0; i --)
        {
            if (System.Math.Round(ssPositionsActual[i].x, 2) == System.Math.Round(ssPositionsActual[i-1].x, 2) &&
                System.Math.Round(ssPositionsActual[i].y, 2) == System.Math.Round(ssPositionsActual[i-1].y, 2))
            {
                ssPositionsActual.RemoveAt(i);
                ssPositionsSmoothed.RemoveAt(i);
                ssSpeeds.RemoveAt(i);
            }
            else
            {
                ssPositionsActual[i] = Vector3.Lerp(ssPositionsActual[i], ssPositionsActual[i-1], ssSpeeds[i]);
                ssPositionsSmoothed[i] = Vector3.Lerp(ssPositionsSmoothed[i], ssPositionsActual[i], ssSpeeds[i]);
                ssOffset += ssPositionsSmoothed[i];
            }
        }
        if (ssSpeeds.Count > 0)
        {
            ssPositionsActual[0] = Vector3.Lerp(ssPositionsActual[0], Vector3.zero, ssSpeeds[0]);
            ssOffset += ssPositionsSmoothed[0];
        }

        // Final position calculation
		finalPos = playerPos + aimOffset + ssOffset + planetOffsetSmoothed;
        transform.position = new Vector3(finalPos.x, finalPos.y, -25);


	}
	IEnumerator ShakeP (float magnatude, float speed, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);


        Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        ssPositionsActual.Add(offset * magnatude);
        ssPositionsSmoothed.Add(Vector3.zero);
        ssSpeeds.Add(speed);
    }
    public void ShakePosition (float magnatude, float verocity, float delayTime, int shakeCount)
    {
        for (int i=0; i < shakeCount; i ++)
        {
            StartCoroutine(ShakeP(magnatude, verocity, delayTime*i));
        }
    }

    public void ShakeAngle (float magnatude, float delayTime)
    {
        transform.localEulerAngles += new Vector3(0, 0, Random.Range(-magnatude, magnatude));
    }

	public void Pause (int frames)
	{
		Time.timeScale = 0.00001f;
		pauseCount = frames;
	}


}
