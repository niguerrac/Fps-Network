using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ThirdPersonNetworkInit : MonoBehaviour
{
	public MonoBehaviour[] ComponentesDes;
	public Camera camara;
	void Start(){
	}
	void OnNetworkInstantiate (NetworkMessageInfo msg)
	{
		// This is our own player
		if (GetComponent<NetworkView>().isMine) {
			
			GetComponent<NetworkInterpolatedTransform> ().enabled = false;
			print ("soyprincipal");
			// This is just some remote controlled player
		} else {
			name += "Remote";
			GetComponent<NetworkInterpolatedTransform> ().enabled = true;
			foreach (MonoBehaviour c in ComponentesDes) {
				c.enabled = false;
			}
			if(GetComponent<CharacterController>())
				GetComponent<CharacterController> ().enabled = false;
			if (camara != null)
				camara.enabled = false;
		}
	}
}
