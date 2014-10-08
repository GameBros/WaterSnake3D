using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform star;
	public float distance;
	public float height;
	public float posDamping;
	public float rotDamping;

	private HeadScript headScript;

	// Use this for initialization
	void Start () {
		headScript = star.GetComponent<HeadScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		var posToBe = star.TransformPoint (0, height, -distance);
		transform.position = Vector3.Lerp (transform.position, posToBe, Time.deltaTime * posDamping);

		var rotToBe = headScript.GetDestRot ();
		transform.rotation = Quaternion.Slerp (transform.rotation, rotToBe, Time.deltaTime * rotDamping);
	}
}
