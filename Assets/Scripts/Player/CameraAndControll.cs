using UnityEngine;
using System.Collections;

public class CameraAndControll : MonoBehaviour 
{
	public Transform currentCamera;
	private Rigidbody playerRigidBody;
	private Health health;
	private WeaponsManager weaponsManager;
	private Vector3 movementDirection;
	private float playerMovementModifier = 750.0f;

	private float cameraTurningModifier = 100.0f;
	private const float CAMERA_TURNING_LIMIT = 10.0f;
	private const float MIN_CAMERA_OFFSET = -60.0f;	//Looking up
	private const float MAX_CAMERA_OFFSET = 30.0f;	//Looking down
	private float currentCameraRotationOffset = 0.0f;
	private Vector3 currentCameraPositionOffset = new Vector3(0.0f, 0.0f, 0.0f);
	private float groundedCooldown;

	void Start () 
	{
		Cursor.visible = false;
		if(currentCamera != null)
		{
			currentCameraPositionOffset = currentCamera.transform.position - transform.position;
		}
		
		playerRigidBody = GetComponent<Rigidbody>();
		health = GetComponent<Health>();
		groundedCooldown = 0f;

		weaponsManager = GetComponent<WeaponsManager> ();
	}
	
	void Update () 
	{
		if(groundedCooldown > 0)
		{
			groundedCooldown -= Time.deltaTime;
		}

		if(health == null || health.IsAlive())
		{
			PlayerMovement ();
			if(currentCamera != null) 
			{
				CameraMovement();
				CameraLooking();
			}
		}
		else
		{
			if(currentCamera != null) 
			{
				CameraMovement();
				CameraLooking();
			}
		}
	}

	void OnCollisionStay()
	{
		groundedCooldown = 0.25f;
	}

	private void PlayerMovement()
	{
		if(groundedCooldown > 0)
		{
			if(currentCamera != null) 
			{
				movementDirection = Vector3.zero;
				movementDirection += Input.GetAxis ("Vertical") * (Quaternion.Euler (0, -90, 0) * currentCamera.transform.right);
				movementDirection += Input.GetAxis ("Horizontal") * currentCamera.transform.right;
				
				if(playerRigidBody != null) 
				{
					playerRigidBody.velocity = movementDirection * playerMovementModifier * Time.deltaTime;
				} 
				else 
				{
					//TODO Movement without RigidBody
				}
			} 
			else 
			{
				movementDirection = Vector3.zero;
				movementDirection += Input.GetAxis ("Vertical") * Vector3.forward;
				movementDirection += Input.GetAxis ("Horizontal") * Vector3.right;
				
				if(playerRigidBody != null) 
				{
					playerRigidBody.velocity = movementDirection * playerMovementModifier * Time.deltaTime;
				} 
				else 
				{
					//TODO Movement without RigidBody and Camera
				}
			}
		}
	}
	
	
	private void CameraMovement()
	{
		currentCamera.transform.position = transform.position + currentCameraPositionOffset;
	}
	
	private void PlayerLooking(float horizontalRotation)
	{
		transform.RotateAround(transform.position, Vector3.up, horizontalRotation);
	}
	
	private void CameraLooking()
	{
		float horizontalRotation = cameraTurningModifier * Input.GetAxis ("Mouse X");
		horizontalRotation *= Time.deltaTime;
		if(horizontalRotation > CAMERA_TURNING_LIMIT)
		{
			horizontalRotation = CAMERA_TURNING_LIMIT;
		} 
		else if(horizontalRotation < -CAMERA_TURNING_LIMIT)
		{
			horizontalRotation = -CAMERA_TURNING_LIMIT;
		}
		
		float verticalRotation = cameraTurningModifier * Input.GetAxis ("Mouse Y");
		verticalRotation *= Time.deltaTime;
		if(verticalRotation > cameraTurningModifier)
		{
			verticalRotation = cameraTurningModifier;
		} 
		else if(verticalRotation < -cameraTurningModifier)
		{
			verticalRotation = -cameraTurningModifier;
		}
		
		if(currentCameraRotationOffset + verticalRotation > MAX_CAMERA_OFFSET)
		{
			verticalRotation = MAX_CAMERA_OFFSET - currentCameraRotationOffset;
		}
		else if(currentCameraRotationOffset + verticalRotation < MIN_CAMERA_OFFSET)
		{
			verticalRotation = MIN_CAMERA_OFFSET - currentCameraRotationOffset;
		}
		currentCameraRotationOffset += verticalRotation;
		
		currentCamera.transform.RotateAround(transform.position, Vector3.up, horizontalRotation);
		PlayerLooking(horizontalRotation);
		//currentCamera.transform.RotateAround(transform.position, currentCamera.transform.right, verticalRotation);
		currentCameraPositionOffset = currentCamera.transform.position - transform.position;

		if(weaponsManager != null)
		{
			//TODO celowanie w pionie
			/*
			Ray ray = new Ray(Camera.main.transform.position + Camera.main.transform.forward*currentCameraPositionOffset.magnitude, Camera.main.transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 1000))
			{
				weaponsManager.Aim(hit.point);
			}
			else
			{
				weaponsManager.Aim(Camera.main.transform.position);
			}
			*/
		}
	}
}
