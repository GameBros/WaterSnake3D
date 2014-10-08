using UnityEngine;
using System.Collections;

public class HeadScript : MonoBehaviour,  IPositionTeller{

	public float speed;
	public float gridSize;
	public float rotDamping;
	public int bodyCount;

	Vector3 destPos;
	Quaternion destRot;
	bool arrived = true;
	KeyCode lastKey;
	Vector3 _lastPos;
	Quaternion _lastRot;

	public Vector3 GetLastPosition (){return this._lastPos;}
	public Quaternion GetLastRotation(){return this._lastRot;}
	public float GetSpeed(){return this.speed;}
	public float GetGrid(){return this.gridSize;}
	public float GetRotationDamping(){return this.rotDamping;}

	public Quaternion GetDestRot(){
		return this.destRot;
	}
	
	// Use this for initialization
	void Start () {
		_lastPos = transform.position;
		_lastRot = transform.rotation;

		var lastTrans = transform;
		for (int i = 0; i < bodyCount; i++) {
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			BodyScript script = cube.AddComponent<BodyScript>();
			script.parentTrans = lastTrans;
			lastTrans = cube.transform;
			cube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-(i+1));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrived) {
			if (Input.GetKeyDown (KeyCode.LeftArrow)){lastKey = KeyCode.LeftArrow;}
			else if (Input.GetKeyDown (KeyCode.RightArrow)){lastKey = KeyCode.RightArrow;}
			else if (Input.GetKeyDown (KeyCode.UpArrow)){lastKey = KeyCode.UpArrow;}
			else if (Input.GetKeyDown (KeyCode.DownArrow)){lastKey = KeyCode.DownArrow;}

			if(Vector3.Distance(transform.position, destPos) > 0.01f){
				transform.Translate((destPos - transform.position).normalized * speed * Time.deltaTime, Space.World);
				transform.rotation = Quaternion.Slerp(transform.rotation, destRot, Time.deltaTime * rotDamping);
			}else{
				transform.position = _lastPos = destPos;
				transform.rotation = _lastRot = destRot;
				arrived = true;
			}

		} else {
			switch(lastKey){
			case KeyCode.UpArrow: destPos = transform.TransformPoint(0, -gridSize, 0);
				destRot = Quaternion.LookRotation(destPos - transform.position, transform.forward);break;
			case KeyCode.DownArrow: destPos = transform.TransformPoint(0, gridSize, 0);
				destRot = Quaternion.LookRotation(destPos - transform.position, -transform.forward);break;
			case KeyCode.LeftArrow: destPos = transform.TransformPoint(-gridSize, 0, 0);
				destRot = Quaternion.LookRotation(destPos - transform.position, transform.up);break;
			case KeyCode.RightArrow: destPos = transform.TransformPoint(gridSize, 0, 0);
				destRot = Quaternion.LookRotation(destPos - transform.position, transform.up);break;
			default: destPos = transform.TransformPoint(0, 0, gridSize);break;
			}

			lastKey = KeyCode.None;

			arrived = false;
		}

	}
}
