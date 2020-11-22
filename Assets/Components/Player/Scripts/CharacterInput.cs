using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour {
	public float moveForce = 250;
	public float pickUpRange = 5;
	public Transform holdParent;
	public GameObject heldobj;
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

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, pickUpRange) && hit.transform.GetComponent<Rigidbody>()) {
			if (CrosshairAnimator.Instance.animator.GetBool("Grabbing") == false)
				CrosshairAnimator.Instance.animator.Play("hand_twitch");
		}
		else {
			if (CrosshairAnimator.Instance.animator.GetBool("Grabbing") == false)
				CrosshairAnimator.Instance.animator.Play("default");
		}

		// Interact
		if (Input.GetButtonDown(interactInput)) {
			if (!heldobj && hit.transform.GetComponent<Rigidbody>()) {
				PickupObject(hit.transform.gameObject);
			}
			else {
				if (heldobj) {
					DropObject();
				}
			}
		}

		if (heldobj) {
			//if (Vector3.Distance(heldobj.transform.position, holdParent.position) > 2.5f) {
				//Vector3 moveDirection = (holdParent.position - heldobj.transform.position);
				//heldobj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
			//}
		}

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

	void PickupObject(GameObject pickObj) {
		if (pickObj.GetComponent<Rigidbody>()) {
			CrosshairAnimator.Instance.animator.SetBool("Grabbing", true);
			Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
			objRig.isKinematic = true;
			objRig.useGravity = false;
			// objRig.drag = 10;

			objRig.transform.parent = holdParent;
			heldobj = pickObj;
		}
	}

	void DropObject() {
		if (heldobj) {
			CrosshairAnimator.Instance.animator.SetBool("Grabbing", false);
			Rigidbody heldRig = heldobj.GetComponent<Rigidbody>();
			heldRig.isKinematic = false;
			heldRig.useGravity = true;
			// heldRig.drag = 1;

			heldobj.transform.parent = null;
			heldobj = null;
		}
	}
}
