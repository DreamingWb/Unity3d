using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticalInfo {
	public float radius = 0f, angle = 0f, time = 0f;
	public ParticalInfo(float radius, float angle, float time){this.radius = radius;this.angle = angle;this.time = time;}
}
public class ParticleHalo : MonoBehaviour {
	private ParticleSystem particleSys;
	private ParticleSystem.Particle[] particleArr; 
	private ParticalInfo[] particles;
	private float[] radius; 
	private float[] collect_radius;
	private int tier = 10;
	private int time = 0;
	public Gradient colorGradient;
	public int particleNum = 10000;
	public float size = 0.03f;
	public float minRadius = 7.0f;
	public float maxRadius = 10.0f;
	public float collect_MaxRadius = 4.0f;
	public float collect_MinRadius = 1.0f;
	public bool clockwise = true; 
	public float speed = 2f; 
	public float pingPong = 0.02f; 
	public int isCollected = 0;
	void changeColor(){
		float colorValue;
		for (int i = 0; i < particleNum; i++){
			colorValue = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
			colorValue += particles[i].angle/360;
			while (colorValue > 1) colorValue--;
			particleArr[i].color = colorGradient.Evaluate(colorValue);
		}
	}
	void RandomlySpread(){
		for (int i = 0; i < particleNum; ++i){  
			float midRadius = (maxRadius + minRadius) / 2;
			float minRate = UnityEngine.Random.Range(1.0f, midRadius / minRadius);
			float maxRate = UnityEngine.Random.Range(midRadius / maxRadius, 1.0f);
			float _radius = UnityEngine.Random.Range(minRadius * minRate, maxRadius * maxRate);
			radius[i] = _radius;
			float collect_MidRadius = (collect_MaxRadius + collect_MinRadius) / 2;
			float collect_outRate = Random.Range (1f, collect_MidRadius / collect_MinRadius);
			float collect_inRate = Random.Range (collect_MaxRadius / collect_MidRadius, 1f);
			float _collect_radius = Random.Range (collect_MinRadius * collect_outRate, collect_MaxRadius * collect_inRate);
			collect_radius[i] = _collect_radius;
			float angle = UnityEngine.Random.Range(0.0f, 360.0f);
			float theta = angle / 180 * Mathf.PI;
			float time = UnityEngine.Random.Range(0.0f, 360.0f);
			if(isCollected == 0) particles [i] = new ParticalInfo (_radius, angle, time);
			else particles [i] = new ParticalInfo (_collect_radius, angle, time);
			particleArr[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0f, particles[i].radius * Mathf.Sin(theta));
		}
		particleSys.SetParticles(particleArr, particleArr.Length);
	}
	void Start () {
		particleArr = new ParticleSystem.Particle[particleNum];
		particles = new ParticalInfo[particleNum];
		radius = new float[particleNum];
		collect_radius = new float[particleNum];
		particleSys = this.GetComponent<ParticleSystem>();
		particleSys.startSpeed = 0;            
		particleSys.startSize = size; 
		particleSys.loop = false;
		particleSys.maxParticles = particleNum;      
		particleSys.Emit(particleNum); 
		particleSys.GetParticles(particleArr);
		RandomlySpread();
	}
	
	// Update is called once per frame
	void Update (){
		for (int i = 0; i < particleNum; i++) {
			if (clockwise) particles [i].angle -= (i % tier + 1) * (speed / particles [i].radius / tier);
			else particles [i].angle += (i % tier + 1) * (speed / particles [i].radius / tier);
			particles [i].angle = (360.0f + particles [i].angle) % 360.0f;
			float theta = particles [i].angle / 180 * Mathf.PI;
			if (isCollected == 1){
				if (particles [i].radius > collect_radius [i]) particles [i].radius -= 15f * (collect_radius [i] / collect_radius [i]) * Time.deltaTime;  
			 	else particles [i].radius = collect_radius [i];
			} 
			else {
				if (particles [i].radius < radius [i]) particles [i].radius += 15f * (collect_radius [i] / collect_radius [i]) * Time.deltaTime;  
				else particles [i].radius += Mathf.PingPong (particles [i].time / minRadius / maxRadius, pingPong) - pingPong / 2.0f;
			}
			particleArr [i].position = new Vector3 (particles [i].radius * Mathf.Cos (theta), 0f, particles [i].radius * Mathf.Sin (theta));
		}
		changeColor ();
		particleSys.SetParticles(particleArr, particleArr.Length);
	}
	void OnGUI(){
		if(Input.GetMouseButtonDown(0)){
			time++;
			if(time==2){
				isCollected = 1 - isCollected;
				time = 0;
			}
		}
	}
}