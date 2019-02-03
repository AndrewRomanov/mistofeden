using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
	public float speed;

	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
        if (!isLocalPlayer)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
	}

    public override void OnStartLocalPlayer()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        base.OnStartLocalPlayer();
    }
}