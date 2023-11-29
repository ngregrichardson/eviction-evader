using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSpeed = 3f;
	[SerializeField]
	private float jumpSpeed = 3f;

	public bool isPaused;

	private PlayerMotor motor;

	void Start()
	{
		motor = GetComponent<PlayerMotor>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (!MenuManager.instance.isShopping && !MenuManager.instance.isPaused && !GameManager.instance.isLost)
		{
			float xMove = Input.GetAxis("Horizontal");
			float yMove = Input.GetAxis("Vertical");

			Vector3 moveHorizontal = transform.right * xMove;
			Vector3 moveVertical = transform.forward * yMove;

			Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

			motor.Move(velocity);

			float yRot = Input.GetAxisRaw("Mouse X");
			Vector3 rotPlayer = new Vector3(0, yRot, 0) * lookSpeed;

			motor.RotatePlayer(rotPlayer);

			float xRot = Input.GetAxisRaw("Mouse Y");
			float rotCamera = xRot * lookSpeed;

			motor.RotateCamera(rotCamera);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				motor.Jump(jumpSpeed);
			}
		}
	}
}
