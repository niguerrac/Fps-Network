using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NetworkView))]
public class NetworkTools : MonoBehaviour {
	
	NetworkView nView;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		nView = GetComponent<NetworkView> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		nView.RPC("PasarSiguienteEscena", RPCMode.All);
		if(Input.GetKeyDown(KeyCode.O))
			nView.RPC("reespawn", RPCMode.All);
	}
	[RPC]
	void PasarSiguienteEscena(){
		Application.LoadLevel (1);
		foreach (GameObject go in FindObjectsOfType (typeof(GameObject)))
			go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
	}
	[RPC]
	void reespawn(){

		foreach (GameObject go in FindObjectsOfType (typeof(GameObject)))
			go.SendMessage ("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
	}
}
