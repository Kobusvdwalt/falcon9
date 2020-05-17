using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particles_scr : MonoBehaviour {
	AudioSource aud;
	ParticleSystem particleSys;
	ParticleSystem.Particle[] parts;
	float[] spectrum = new float[1024];
	void Start () {
		aud = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
		particleSys = GetComponent<ParticleSystem>();
		parts = new ParticleSystem.Particle[particleSys.maxParticles];
	}


	void Update () {
		aud.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
		particleSys.GetParticles(parts);

		int channelCount = 10;
		for (int i=0; i < channelCount; i++)
		{
			// Calculate sum
			float sum = 1;
			for (int j= i*spectrum.Length/channelCount; j < (i+1)*spectrum.Length/channelCount; j ++)
			{
				sum += Mathf.Abs(spectrum[i]) * 0.2f;
			}
			// Apply size to particels
			for (int j= i* parts.Length/channelCount; j < (i+1)*parts.Length/channelCount; j++)
			{
				parts[j].startSize = Mathf.Lerp(parts[j].startSize, sum, 0.2f);
				parts[j].randomSeed = (uint)j;
			}
		}

		particleSys.SetParticles(parts, particleSys.particleCount);
	}
}
