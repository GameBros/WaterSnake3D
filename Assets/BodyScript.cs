using UnityEngine;
using System.Collections;

public class BodyScript : MonoBehaviour {
	
	public float speed;
	public float gridSize;

	public Transform parentTrans;
	private HeadScript parentScript;
	
	Vector3 destPos;
	Quaternion destRot;
	public Vector3 lastPos;
	public Quaternion lastRot;
	bool arrived = true;
	
	// Use this for initialization
	void Start () {
		parentScript = parentTrans.GetComponent<HeadScript> ();
		lastPos = transform.position;
		lastRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrived) {
			if(Vector3.Distance(transform.position, destPos) > 0.01f){
				transform.Translate((destPos - transform.position).normalized * speed);
				transform.rotation = Quaternion.Slerp(transform.rotation, destRot, Time.deltaTime);
			}else{
				transform.position = lastPos = destPos;
				transform.rotation = lastRot = destRot;
				arrived = true;
			}
			
		} else {
			destRot = parentScript.lastRot;
			destPos = parentScript.lastPos;
			
			arrived = false;
		}
		
	}
}
