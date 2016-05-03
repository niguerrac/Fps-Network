using UnityEngine;
using System.Collections;

public class ThirdPersonAction : MonoBehaviour
{
	public GameObject cube;

	void Update ()
	{
		//自分が操作しているプレイヤーのみにアクションさせる場合
		//networkView.isMineで自分と判断しなければならない
		//これをしないとすべてのプレイヤーがアクションを起こしてしまう
		if (GetComponent<NetworkView>().isMine) {
			CreateCube ();
		}
	}
	void CreateCube ()
	{
		if (Input.GetKeyDown (KeyCode.Z)) {

			Network.Instantiate (cube, transform.position + transform.forward * 2 + transform.up * 0.5f, Quaternion.identity, 0);

		}
	}
	
	
	
	//キャラクターコントローラーがRigidbodyのものを押すためのもの
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		float pushPower = 2.0f;
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;
		
		if (hit.moveDirection.y < -0.3f)
			return;
		
		Vector3 pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;
	}
}
