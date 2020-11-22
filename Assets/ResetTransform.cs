using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		other.transform.SetPositionAndRotation(Vector3.up * 2, Quaternion.identity);

		if (other.CompareTag("Player")) {
			other.GetComponent<CharacterController>().enabled = false;

			// For reasons, we have to disable the charcater controller when teleporting.
			// Otherwise, Move functions during this frame will override the Set Position.

			other.transform.SetPositionAndRotation(Vector3.up * 25, Quaternion.identity);
			other.GetComponent<CharacterController>().enabled = true;
		}
	}
}
