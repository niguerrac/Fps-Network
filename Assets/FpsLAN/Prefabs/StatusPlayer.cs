﻿using UnityEngine;
using System.Collections;


public class StatusPlayer : MonoBehaviour {

	public float vida = 100;
	public float kill = 0;
	public float dead = 0;
	public GameObject[] spawnpoint;


	// Use this for initialization
	void Start () {
		spawnpoint = GameObject.FindGameObjectsWithTag ("Spawn");

	
	}

	// Update is called once per frame
	void Update () {
		if (vida <= 0) {
			transform.position = spawnpoint[Random.Range(0,spawnpoint.Length)].transform.position;
			vida = 100;
			dead ++;
		}
	
	}
	public void masKill()
	{
		kill++;
	}
	string barravida = "##########";
	public bool Vida (float damage)
	{
		vida = vida - damage;
		barravida = "";
		for (int i = 0; i < vida; i = i + 10)
			barravida = barravida +"#";
		if (vida <= 0)
			return true;
		return false;
	}
	void OnGUI()
	{if (GetComponent<NetworkView> ().isMine) {
			

			GUI.Label (new Rect (1, 1, 300, 20), "Vida: " + vida +" ["+ barravida + "] Kill: " + kill + " dead: " + dead);
		}
	}
}
