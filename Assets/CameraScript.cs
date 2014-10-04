using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform star;
	public float distance;
	public float height;
	public float posDamping;
	public float rotDamping;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var posToBe = star.TransformPoint (0, height, -distance);
		transform.position = Vector3.Lerp (transform.position, posToBe, Time.deltaTime * posDamping);

		var rotToBe = Quaternion.LookRotation (star.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotToBe, Time.deltaTime * rotDamping);
	}
}
