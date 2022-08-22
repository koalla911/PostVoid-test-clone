using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool groundedPlayer;
	private InputManager inputManager;
	private Transform cameraTransform;

	public float playerSpeed = 2.0f;
	public float jumpHeight = 1.0f;
	public float gravityValue = -9.81f;

	private void Start()
	{
		controller = gameObject.GetComponent<CharacterController>();
		inputManager = InputManager.Instance;
		cameraTransform = Camera.main.transform;
	}

	void Update()
	{
		groundedPlayer = controller.isGrounded;
		if (groundedPlayer && playerVelocity.y < 0)
		{
			playerVelocity.y = 0f;
		}

		Vector2 movement = inputManager.GetPlayerMovement();
		Vector3 move = new Vector3(movement.x, 0f, movement.y);
		move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
		move.y = 0f;
		controller.Move(move * Time.deltaTime * playerSpeed);

		/*if (move != Vector3.zero)
		{
			gameObject.transform.forward = move;
		}*/

		// Changes the height position of the player..
		if (inputManager.IsPlayerJumped() && groundedPlayer)
		{
			playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
		}

		playerVelocity.y += gravityValue * Time.deltaTime;
		controller.Move(playerVelocity * Time.deltaTime);
	}
}
