using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class ThirdPersonNetworkInit : MonoBehaviour
{
	public MonoBehaviour[] ComponentesDes;
	public Camera camara;
	public CharacterController Ccontroler;
	void Start(){
		if (GetComponent<CharacterController> () && Ccontroler == null)
			Ccontroler = GetComponent<CharacterController> ();
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
			if(Ccontroler!=null)
				Ccontroler.enabled = false;
			if (camara != null)
				camara.enabled = false;
		}
	}
	public void OnNetworkStart()
	{
		// This is our own player
		if (ConnectGui.isServer) {

			GetComponent<NetworkInterpolatedTransform> ().enabled = false;
			print ("soyprincipal");
			// This is just some remote controlled player
		} else {
			name += "Remote";
			GetComponent<NetworkInterpolatedTransform> ().enabled = true;
			foreach (MonoBehaviour c in ComponentesDes) {
				c.enabled = false;
			}
			if(Ccontroler!=null)
				Ccontroler.enabled = false;
			if (camara != null)
				camara.enabled = false;
		}
	}
}
