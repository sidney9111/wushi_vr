using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class Shooting : MonoBehaviour {
	float timer;
	Ray shootRay;                   // A ray from the gun end forwards.
	RaycastHit shootHit;            // A raycast hit to get information about what was hit.
	LineRenderer gunLine;           // Reference to the line renderer.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	public float range = 100; 			// the distance the gun can fire
	public int damagePerShot;
	int shootableMask;				// a layer mask that 
	void Awake(){
		shootableMask = LayerMask.GetMask ("shootable");
		timer = 0;
		gunLine = GetComponent<LineRenderer> ();
	}
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//????????????????
		#if !MOBILE_INPUT
		if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
			Shoot ();
		}
		#else
		// If there is input on the shoot direction stick and it's time to fire...
		if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
		{
			// ... shoot the gun
			Shoot();
		}
		#endif

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			// ... disable the effects.
			DisableEffects ();
		}
	}
	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		//faceLight.enabled = false;
		//gunLight.enabled = false;
	}

	/// <summary>
	/// 由于个模型很奇葩，所以旋转位不好找 
	/// </summary>
	void Shoot ()
	{
		Debug.Log ("sssssssssssss");
		Transform infantryTransform = transform.parent.Find ("US_infantry_MG");
		Transform vickersTransform = infantryTransform.Find ("Biped_Shadow:vickers:vickers");
		Transform neutralPoseTransform = vickersTransform.Find ("Biped_Shadow:vickers:base_NeutralPose");
		Transform baseTransform = neutralPoseTransform.Find ("Biped_Shadow:vickers:base");
		Transform topTransoform = baseTransform.Find ("Biped_Shadow:vickers:top_NeutralPose");

		//gunLine.transform.rotation = rotateTransform.rotation;

		// Reset the timer.
		timer = 0f;

		// Play the gun shot audioclip.
		//gunAudio.Play ();

		// Enable the lights.
		//gunLight.enabled = true;
		//faceLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		//gunParticles.Stop ();
		//gunParticles.Play ();

		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;

		if (topTransoform != null) {
			Debug.Log ("topTransoform");
			gunLine.SetPosition (0, topTransoform.position);
			transform.rotation = topTransoform.rotation;
		}

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			}

			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

}
