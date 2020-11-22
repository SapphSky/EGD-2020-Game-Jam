using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairAnimator : MonoBehaviour {
	public static CrosshairAnimator Instance;
	public Animator animator;
	public bool interactHover, interactGrab;

	private void Start() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(this);
		}

		animator = GetComponent<Animator>();
	}

	private void Update() {
		if (interactHover) {	// Hovering over interactable
			if (animator.GetBool("Grabbing")) {	// Holding interactable
				animator.Play("hand_grab");
			}
		}
	}
}
