using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {
	public Character character;
	public GameObject flashlight;

	[Header("Input Settings")]
	public string horizontalMoveInput = "Horizontal";
	public string verticalMoveInput = "Vertical";
	public string jumpInput = "Jump";
	public string interactInput = "Fire1";
	public string flashlightInput = "Toggle Flashlight";

	[Header("Runtime Parameters")]
	[SerializeField] public float turnSmoothTime = 0.1f;
	[SerializeField] private float turnSmoothVelocity;

	private void Start() {
		character = GetComponent<Character>();
	}

	private void Update() {
		Vector3 moveInput = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw(horizontalMoveInput), 0, Input.GetAxisRaw(verticalMoveInput)), 1);
		Vector3 moveDir = transform.right * moveInput.x + transform.forward * moveInput.z;

		transform.Rotate(Vector3.up * CinemachineCore.GetInputAxis("Mouse X"));
		character.Move(moveDir.normalized);

		// Jump
		if (Input.GetButtonDown(jumpInput)) {
			character.Jump();
		}

		// Toggle Flashlight
		if (Input.GetButtonDown(flashlightInput)) {
			flashlight.SetActive(!flashlight.activeInHierarchy);
		}

		// Interact
		if (Input.GetButtonDown(interactInput)) {
			character.Interact();
		}
	}
}
