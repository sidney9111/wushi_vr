using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public GameObject prefab;
	private ParticleSystem hitParticles;
	void Awake(){

		//hitParticles = prefab
		//hitParticles = GetComponent<ParticleSystem> ();
	}
	// Use this for initialization
	void Start () {
//		GameObject obj =  (GameObject)Instantiate(prefab, new Vector3(0, 1.5f, 0), Quaternion.Euler(0, 0, 0));
//		hitParticles = obj.GetComponentInChildren<ParticleSystem> ();
//		hitParticles.playOnAwake = false;
	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}

	public void TakeDamage(int amount, Vector3 hitPoint){
		GameObject obj =  (GameObject)Instantiate(prefab, new Vector3(0, 1.5f, 0), Quaternion.Euler(0, 0, 0));
		hitParticles = obj.GetComponentInChildren<ParticleSystem> ();
		hitParticles.transform.position = hitPoint;
		//hitParticles.playOnAwake = false;

		Debug.Log ("take damage hit");
		if (hitParticles == null)
			return;

		Debug.Log ("take damage");
		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play ();


	}
}
