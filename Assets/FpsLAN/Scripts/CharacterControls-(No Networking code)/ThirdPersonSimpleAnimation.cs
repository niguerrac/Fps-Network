using UnityEngine;
using System.Collections;

[AddComponentMenu("Third Person Player/Third Person Player Animation")]
public class ThirdPersonSimpleAnimation : MonoBehaviour
{

	public float runSpeedScale = 1.0f;
	public float walkSpeedScale = 1.0f;
	public Transform torso;
	private NetworkSyncAnimation networkSyncAnimation;
	void Awake ()
	{
		// By default loop all animations
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		
		// We are in full control here - don't let any other animations play when we start
		GetComponent<Animation>().Stop ();
		GetComponent<Animation>().Play ("idle");
	}

	void Update ()
	{
		ThirdPersonController marioController = GetComponent<ThirdPersonController> ();
		float currentSpeed = marioController.GetSpeed ();
		 networkSyncAnimation = GetComponent<NetworkSyncAnimation> ();
		
		// Fade in run
		if (currentSpeed > marioController.walkSpeed) {
			GetComponent<Animation>().CrossFade ("run");
			// We fade out jumpland quick otherwise we get sliding feet
			GetComponent<Animation>().Blend ("jumpland", 0);
			networkSyncAnimation.SendMessage ("SyncAnimation", "run");
			// Fade in walk
		} else if (currentSpeed > 0.1f) {
			GetComponent<Animation>().CrossFade ("walk");
			// We fade out jumpland realy quick otherwise we get sliding feet
			GetComponent<Animation>().Blend ("jumpland", 0);
			networkSyncAnimation.SendMessage ("SyncAnimation", "walk");
			// Fade out walk and run
		} else {
			GetComponent<Animation>().CrossFade ("idle");
			networkSyncAnimation.SendMessage ("SyncAnimation", "idle");
		}
		
		GetComponent<Animation>()["run"].normalizedSpeed = runSpeedScale;
		GetComponent<Animation>()["walk"].normalizedSpeed = walkSpeedScale;
		
		if (marioController.IsJumping ()) {
			if (marioController.IsCapeFlying ()) {
				GetComponent<Animation>().CrossFade ("jetpackjump", 0.2f);
				networkSyncAnimation.SendMessage ("SyncAnimation", "jetpackjump");
			} else if (marioController.HasJumpReachedApex ()) {
				GetComponent<Animation>().CrossFade ("jumpfall", 0.2f);
				networkSyncAnimation.SendMessage ("SyncAnimation", "jumpfall");
			} else {
				GetComponent<Animation>().CrossFade ("jump", 0.2f);
				networkSyncAnimation.SendMessage ("SyncAnimation", "jump");
			}
			// We fell down somewhere
		} else if (!marioController.IsGroundedWithTimeout ()) {
			GetComponent<Animation>().CrossFade ("ledgefall", 0.2f);
			networkSyncAnimation.SendMessage ("SyncAnimation", "ledgefall");
			// We are not falling down anymore
		} else {
			GetComponent<Animation>().Blend ("ledgefall", 0.0f, 0.2f);
		}
	}

	public void DidLand ()
	{
		GetComponent<Animation>().Play ("jumpland");
		networkSyncAnimation.SendMessage ("SyncAnimation", "jumpland");
	}

	public void DidPunch ()
	{
		GetComponent<Animation>().CrossFadeQueued ("punch", 0.3f, QueueMode.PlayNow);
	}

	public void DidButtStomp ()
	{
		GetComponent<Animation>().CrossFade ("buttstomp", 0.1f);
		networkSyncAnimation.SendMessage ("SyncAnimation", "buttstomp");
		GetComponent<Animation>().CrossFadeQueued ("jumpland", 0.2f);
	}

	public void ApplyDamage ()
	{
		GetComponent<Animation>().CrossFade ("gothit", 0.1f);
		networkSyncAnimation.SendMessage ("SyncAnimation", "gothit");
	}


	public void DidWallJump ()
	{
		// Wall jump animation is played without fade.
		// We are turning the character controller 180 degrees around when doing a wall jump so the animation accounts for that.
		// But we really have to make sure that the animation is in full control so 
		// that we don't do weird blends between 180 degree apart rotations
		GetComponent<Animation>().Play ("walljump");
		networkSyncAnimation.SendMessage ("SyncAnimation", "walljump");
	}
	
	
}
