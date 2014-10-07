using UnityEngine;
using System.Collections;

public class BodyScript : MonoBehaviour, IPositionTeller {

	public Transform parentTrans;

	float speed;
	float gridSize;
	float rotDamping;

	IPositionTeller parentScript;
	
	Vector3 destPos;
	Quaternion destRot;
	Vector3 lastPos;
	Quaternion lastRot;
	bool arrived = true;

	public Vector3 GetLastPosition (){return this.lastPos;}
	public Quaternion GetLastRotation(){return this.lastRot;}
	public float GetSpeed(){return this.speed;}
	public float GetGrid(){return this.gridSize;}
	public float GetRotationDamping(){return this.rotDamping;}
	
	// Use this for initialization
	void Start () {
		parentScript = (IPositionTeller) parentTrans.GetComponent(typeof(IPositionTeller));
		lastPos = transform.position;
		lastRot = transform.rotation;
		speed = parentScript.GetSpeed ();
		gridSize = parentScript.GetGrid ();
		rotDamping = parentScript.GetRotationDamping ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrived) {
			if(Vector3.Distance(transform.position, destPos) > 0.01f){
				transform.Translate((destPos - transform.position).normalized * speed * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Slerp(transform.rotation, destRot, Time.deltaTime * rotDamping);
			}else{
				transform.position = lastPos = destPos;
				transform.rotation = lastRot = destRot;
				arrived = true;
			}
			
		} else {
			destRot = parentScript.GetLastRotation();
			destPos = parentScript.GetLastPosition();
			
			arrived = false;
		}
		
	}
}
