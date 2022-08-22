﻿using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class CinemachinePOVExtention : CinemachineExtension
	{
		[SerializeField] private float clampAngle = 80f;
		[SerializeField] private float horizontalSpeed = 20f;
		[SerializeField] private float verticleSpeed = 20f;
		private InputManager inputManager;
		private Vector3 startingRotation;

		protected override void Awake()
		{
			inputManager = InputManager.Instance;
			base.Awake();
		}

		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (vcam.Follow)
			{
				if (stage == CinemachineCore.Stage.Aim)
				{
					if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
					Vector2 deltaInput = inputManager.GetMouseDelta();
					startingRotation.x += deltaInput.x * verticleSpeed * Time.deltaTime;
					startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
					startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
					state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
				}
			}
		}
	}
}