using UnityEngine;
using System.Collections;

public class HeadScript : MonoBehaviour {

	public float speed;
	public float gridSize;

	Vector3 destPos;
	Quaternion destRot;
	public Vector3 lastPos;
	public Quaternion lastRot;
	bool arrived = true;

	// Use this for initialization
	void Start () {
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
			if(Input.GetKey(KeyCode.UpArrow)){
				destPos = transform.TransformPoint (0, gridSize, 0);
			}else if(Input.GetKey(KeyCode.DownArrow)){
				destPos = transform.TransformPoint (0, -gridSize, 0);
			}else if(Input.GetKey(KeyCode.LeftArrow)){
				destPos = transform.TransformPoint (-gridSize, 0, 0);
			}else if(Input.GetKey(KeyCode.RightArrow)){
				destPos = transform.TransformPoint (gridSize, 0, 0);
			}else{
				destPos = transform.TransformPoint (0, 0, gridSize);
			}
			
			destRot = Quaternion.LookRotation(destPos - transform.position);

			arrived = false;
		}

	}
}
