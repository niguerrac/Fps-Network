using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Fire : NetworkBehaviour {
	public GameObject projectile;
	public float fireRate = 0.5F;
	public float firePower = 0.5F;
	private float nextFire = 0.0F;
	private AudioSource sonido ;
	private Camera cam;
	public LayerMask targetingLayerMask = -1;
	private float targetingRayLength = Mathf.Infinity;

	private float vidaenemigo=0;
	private bool enemigovisto= false;
	public NetworkView nView;

	void Start(){
		sonido = GetComponent<AudioSource> ();
		cam = (Camera)FindObjectOfType (typeof(Camera));
		nView = GetComponent<NetworkView>();
	}
	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;

			nView.RPC("disparar", RPCMode.AllBuffered);
		}
		statatus_opoenete ();
	}
	[RPC]
	void disparar()
	{
		GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
		clone.GetComponent<Rigidbody>().AddForce(transform.forward *firePower);
		sonido.Play();
		StartCoroutine (destruir(clone));
	}
	IEnumerator destruir( GameObject obj)
	{
		yield return new WaitForSeconds (3);
		Network.Destroy (obj);
	}
	void statatus_opoenete()
	{
		enemigovisto = false;
		Transform targetTransform = null;
		if (cam != null) {
			RaycastHit hitInfo;
			Ray ray = cam.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			if (Physics.Raycast (ray, out hitInfo, targetingRayLength, targetingLayerMask.value)) {
				// Cache what we've hit
				targetTransform = hitInfo.collider.transform;
			}

			if (targetTransform !=null && targetTransform.GetComponent<StatusPlayer> ()!=null) {
				enemigovisto = true;
				vidaenemigo = targetTransform.GetComponent<StatusPlayer> ().vida;
				print("Enemigo: "+ vidaenemigo);
			}
		}
	}

		void OnGUI()
		{
		{
			GUI.Label (new Rect (Screen.width / 2, Screen.height / 2, 300, 20), "X");
			if (enemigovisto)
				GUI.Label (new Rect (1, Screen.height - 30, 300, 20), "Enemigo: " + vidaenemigo);
		}
		}
}
