﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PE_GravType {
	none, 
	constant,
	floating
}

public enum PE_Collider {
	sphere,
	square
}

public class PhysEngine : MonoBehaviour {
	static public List<PE_Obj>	objs;

	public Vector3		gravity = new Vector3(0,-5f,0);
	public Vector3		float_grav = new Vector3(0,-2f,0);

	// Use this for initialization
	void Awake() {
		objs = new List<PE_Obj>();
	}


	void FixedUpdate () {
		// Handle the timestep for each object
		float dt = Time.fixedDeltaTime;
		foreach (PE_Obj po in objs) {
			TimeStep(po, dt);
		}

		// Resolve collisions


		// Finalize positions
		foreach (PE_Obj po in objs) {
			po.transform.position = po.pos1;
		}

	}


	public void TimeStep(PE_Obj po, float dt) {
		if (po.still) {
			po.pos0 = po.pos1 = po.transform.position;
			return;
		}

		if (po.reached_ground == false) {
			// Velocity
			po.vel0 = po.vel;
			Vector3 tAcc = po.acc;
			switch (po.grav) {
			case PE_GravType.constant:
				tAcc += gravity;
				po.vel += tAcc * dt;
				break;
			case PE_GravType.floating:
				po.vel = float_grav; 
				break;
			}
		}

		// Position
		po.pos1 = po.pos0 = po.transform.position;
		po.pos1 += po.vel * dt;

	}
}
