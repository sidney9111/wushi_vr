using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class Behavior : MonoBehaviour {
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	#if !MOBILE_INPUT
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	#endif
	void Awake(){
		#if !MOBILE_INPUT
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		#endif

		playerRigidbody = GetComponent <Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		

	}
	
//	// Update is called once per frame
//	void Update () {
//		
//	}
	void FixedUpdate(){

		Turning ();
	}
	void Turning ()
	{
		#if !MOBILE_INPUT
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		if(Physics.Raycast(camRay,out floorHit))
		// Perform the raycast and if it hits something on the floor layer...
		//if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotatation);
		}
		#else

		Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

		if (turnDir != Vector3.zero)
		{
		// Create a vector from the player to the point on the floor the raycast from the mouse hit.
		Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

		// Ensure the vector is entirely along the floor plane.
		playerToMouse.y = 0f;

		// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
		Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

		// Set the player's rotation to this new rotation.
		playerRigidbody.MoveRotation(newRotatation);
		}
		#endif
	}
}
