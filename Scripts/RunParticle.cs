using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour {

	private ParticleSystem pSys;
	private ParticleSystem.Particle[] particles;
	private Particle[] particleProperty;

	// Use this for initialization
	void Start () 
	{
		particles = new ParticleSystem.Particle[5000];  
		particleProperty = new Particle[5000];  

		pSys = this.GetComponent<ParticleSystem>();  

		pSys.startSpeed = 0; 
		pSys.startSize = 0.05f;           
		pSys.loop = false;    
		pSys.maxParticles = 5000; 
		pSys.Emit(5000);  

		pSys.GetParticles(particles);

		for (int i = 0; i < 5000; i++) 
		{
			float midRadius = 3.0f;  
			float minRate = Random.Range(1.0f, midRadius / 2.0f);  
			float maxRate = Random.Range(midRadius / 4.0f, 1.0f);  
			float radius = Random.Range(2.0f * minRate, 4.0f * maxRate);
			float angle = Random.Range(0.0f, 360.0f);
			float time = Random.Range(0.0f, 360.0f);

			particleProperty [i] = new Particle (radius, angle, time);
			particles [i].position = new Vector3 (particleProperty [i].radius * Mathf.Cos (particleProperty [i].angle / 180 * Mathf.PI), 
																						particleProperty [i].radius * Mathf.Sin (45 / 180 * Mathf.PI),
																						particleProperty [i].radius * Mathf.Sin (particleProperty [i].angle / 180 * Mathf.PI));
		}
		pSys.SetParticles(particles, 5000); 

	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < 5000; i++)  
		{   
			particleProperty[i].angle -= 10f; 
			particleProperty[i].angle = particleProperty[i].angle % 360.0f;  

			particles [i].position = new Vector3 (particleProperty [i].radius * Mathf.Cos (particleProperty [i].angle / 180 * Mathf.PI), 
																						particleProperty [i].radius * Mathf.Sin (45 / 180 * Mathf.PI),
																						particleProperty [i].radius * Mathf.Sin (particleProperty [i].angle / 180 * Mathf.PI)); 
		}  

		pSys.SetParticles(particles, 5000);
	}
}
