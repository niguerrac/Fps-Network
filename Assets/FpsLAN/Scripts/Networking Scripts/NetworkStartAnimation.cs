using UnityEngine;
using System.Collections;

public class NetworkStartAnimation : MonoBehaviour {
	// Use this for initialization
	NetworkView nview;
	void Start () {
		nview = GetComponent<NetworkView> ();

	}
	public void OnNetworkStart(){
		if (!ConnectGui.isServer) {
			nview.RPC ("EnviarAnimacionTime", RPCMode.Others);
		}
	}
	// Update is called once per frame
	void Update () {
	}
	[RPC]
	public void RecivirAnimacionTime(float tiempo){
		print(GetComponent<Animation> () ["Take 001"].normalizedTime);
		GetComponent<Animation> () ["Take 001"].normalizedTime = tiempo;
	}
	[RPC]
	public void EnviarAnimacionTime(){
		nview.RPC ("RecivirAnimacionTime",RPCMode.Others,GetComponent<Animation> () ["Take 001"].normalizedTime);
	}
}
