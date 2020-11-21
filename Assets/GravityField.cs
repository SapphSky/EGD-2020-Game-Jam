using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour {
	public Transform rootField;
	public float force = 0.5f;
	private void OnTriggerStay(Collider other) {
		
		if (other.GetComponent<Rigidbody>()) {
			Rigidbody otherBody = other.GetComponent<Rigidbody>();
			RaycastHit hit;

			// Test to see that the closest point on the object is at least within line of sight of the lamp's cast.
			if (Physics.Raycast(rootField.position, other.ClosestPoint(rootField.position) - rootField.position, out hit)) {
				// Debug.DrawLine(rootField.position, other.ClosestPoint(rootField.position) - rootField.position, Color.blue, 1.0f, true);
				otherBody.useGravity = false;
				otherBody.AddForce(Vector3.up * force, ForceMode.VelocityChange);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.GetComponent<Rigidbody>()) {
			Rigidbody otherBody = other.GetComponent<Rigidbody>();
			otherBody.useGravity = true;
		}
	}
}
