using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private static InputManager instance;
	private PlayerInputAction inputActions;

	public static InputManager Instance => instance;

	private void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
		inputActions = new PlayerInputAction();
		Cursor.visible = false;
	}

	private void OnEnable()
	{
		inputActions.Enable();
	}

	public Vector2 GetPlayerMovement()
	{
		return inputActions.Player.Movement.ReadValue<Vector2>();
	}

	public Vector2 GetMouseDelta()
	{
		return inputActions.Player.Look.ReadValue<Vector2>();
	}

	public bool IsPlayerJumped()
	{
		return inputActions.Player.Jump.triggered;
	}

	public bool IsPlayerShot()
	{
		return inputActions.Player.Shoot.triggered;
	}

	private void OnDisable()
	{
		inputActions.Disable();
	}
}
