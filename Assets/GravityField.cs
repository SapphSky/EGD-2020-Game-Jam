using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour {
	public Light light;
	public LayerMask layerMask;
	private void Update() {

		RaycastHit hit;
		Physics.Raycast(transform.position, transform.forward, out hit, light.range, layerMask, QueryTriggerInteraction.Ignore);
	}
}
