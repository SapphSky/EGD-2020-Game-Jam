using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public CharacterController controller;

	[Header("Collision Check")]
	public LayerMask groundMask;
	public float groundDistance = 0.125f;

	[Header("Locomotion")]
	public float moveSpeed = 5.0f;
	public float jumpHeight = 2.0f;
	public Vector3 velocity = new Vector3(0, 0, 0);

	[Header("Flags")]
	public bool isGrounded = false;

	private void Start() {
		if (controller == null)
			controller = GetComponent<CharacterController>();
	}

	private void Update() {
		isGrounded = Physics.CheckSphere(transform.position, controller.skinWidth * 1.10f, groundMask, QueryTriggerInteraction.Ignore);
		if (isGrounded) {
			if (velocity.y < 0) {
				velocity.y = Physics.gravity.y * Time.deltaTime * 10;
			}
		}

		velocity += Physics.gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
	}

	public void Move(Vector3 direction) {
		controller.Move(direction * moveSpeed * Time.deltaTime);
	}

	public void Jump() {
		if (isGrounded) {
			velocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
		}
	}

	public void Interact() {
		// TODO
	}
}
