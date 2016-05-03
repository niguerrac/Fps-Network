using UnityEngine;
using System.Collections;

public class StatusBala : MonoBehaviour {
	public float damage = 20 ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<StatusPlayer> ()) {
			if (collision.gameObject.GetComponent<StatusPlayer> ().Vida (damage))
				GameObject.FindGameObjectWithTag ("Player").GetComponent<StatusPlayer> ().masKill ();
			Destroy(this);
		}

	}
}
