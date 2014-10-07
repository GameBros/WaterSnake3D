using UnityEngine;
using System.Collections;

public interface IPositionTeller{
	float GetSpeed();
	float GetGrid();
	Vector3 GetLastPosition();
	Quaternion GetLastRotation();
	float GetRotationDamping();
}
