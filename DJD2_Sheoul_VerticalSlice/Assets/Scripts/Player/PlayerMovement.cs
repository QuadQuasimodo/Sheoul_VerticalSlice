using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Constants

    private const float MAX_ACCELERATION = 100.0f;
    private const float GRAVITY_ACCELERATION = 20.0f;

    private const float MAX_FORWARD_VELOCITY = 5.0f;
    private const float MAX_BACKWARD_VELOCITY = 4.0f;
    private const float MAX_STRAFE_VELOCITY = 4.0f;
    private const float MAX_FALL_VELOCITY = 100.0f;
    private const float ANGULAR_VELOCITY_FACTOR = 2.0f;

    private const float DIAGONAL_VELOCITY_FACTOR = 0.7f;
    private const float WALK_VELOCITY_FACTOR = 1.0f;
    private const float RUN_VELOCITY_FACTOR = 1.25f;

    private const float MIN_HEAD_LOOK_ROTATION = 75.0f;
    private const float MAX_HEAD_LOOK_ROTATION = 75.0f;

    //Istance Variables

    private CharacterController controller;
    private Transform cameraTransform;
    private AudioSource audioSource;
    private SceneManager sceneManager;

    private Vector3 acceleration;
    private Vector3 velocity;
    private float velocityFactor;

    private Vector3 spawn;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        audioSource = GetComponent<AudioSource>();
        sceneManager = new SceneManager();
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        velocityFactor = WALK_VELOCITY_FACTOR;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        spawn = transform.localPosition;
        spawn.y += 2;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateVelocityFactor();
        UpdateRotation();
        UpdateCamera();
        UpdateWalkSound();
    }
    private void UpdateVelocityFactor()
    {
        velocityFactor = Input.GetButton("Run") ?
            RUN_VELOCITY_FACTOR : WALK_VELOCITY_FACTOR;

        if (Input.GetAxis("Strafe") != 0 && Input.GetAxis("Forward") != 0)
            velocity *= DIAGONAL_VELOCITY_FACTOR;
    }

    private void UpdateRotation()
    {
        float rotation = Input.GetAxis("Mouse X") * ANGULAR_VELOCITY_FACTOR;

        transform.Rotate(0f, rotation, 0f);
    }

    private void UpdateCamera()
    {
        Vector3 cameraRotation = cameraTransform.localEulerAngles;

        cameraRotation.x -= Input.GetAxis("Mouse Y") * ANGULAR_VELOCITY_FACTOR;

        if (cameraRotation.x > 180.0f)
            cameraRotation.x = Mathf.Max(cameraRotation.x, MIN_HEAD_LOOK_ROTATION);
        else
            cameraRotation.x = Mathf.Min(cameraRotation.x, MAX_HEAD_LOOK_ROTATION);

        cameraTransform.localEulerAngles = cameraRotation;
    }



    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();
        if (transform.position.y < -50) Kill();
    }

    /// <summary>
    /// Updates the players acceleration based on the keys pressed
    /// </summary>
    private void UpdateAcceleration()
    {
        // Sets acceleration.x to the axis value of
        // "Strafe" times the max acceleration constant
        acceleration.x = Input.GetAxis("Strafe") * MAX_ACCELERATION;

        // Sets acceleration.z to the axis value of "Forward"
        // times the max acceleration constant
        acceleration.z = Input.GetAxis("Forward") * MAX_ACCELERATION;

        // Subtracts (-)gravity acceleration constant to
        // acceleration.y if the controller is not grounded
        if (!controller.isGrounded) acceleration.y = -GRAVITY_ACCELERATION;

        // Sets acceleration.y to 0 otherwise
        else acceleration.y = 0f;
    }

    /// <summary>
    /// Updates the players Velocity according to
    /// its acceleration and velocityFactor
    /// </summary>
    private void UpdateVelocity()
    {
        // Adds acceleration * Time.fixedDeltaTime to the current velocity
        velocity += acceleration * Time.fixedDeltaTime;

        // Clamps the Players velocity.x based on its
        // velocityFactor and movement constants
        velocity.x = acceleration.x == 0f ? velocity.x = 0f : Mathf.Clamp(
            velocity.x, -MAX_STRAFE_VELOCITY * velocityFactor,
            MAX_STRAFE_VELOCITY * velocityFactor);

        // Clamps the Players velocity.y based on its movement constants
        velocity.y = acceleration.y == 0f ? velocity.y = -0.1f : Mathf.Clamp(
            velocity.y, -MAX_FALL_VELOCITY, 0f);

        // Clamps the Players velocity.z based on its
        // velocityFactor and movement constants
        velocity.z = acceleration.z == 0f ? velocity.z = 0f : Mathf.Clamp(
            velocity.z, -MAX_BACKWARD_VELOCITY * velocityFactor,
            MAX_FORWARD_VELOCITY * velocityFactor);
    }

    /// <summary>
    /// Moves the player according to it's velocity
    /// </summary>
    private void UpdatePosition()
    {
        // Creates a new Vector3 that is set to the players velocity
        // times the Time.fixedDeltaTime
        Vector3 move = velocity * Time.fixedDeltaTime;

        // Moves the player controller according to the move Vector3
        controller.Move(transform.TransformVector(move));
    }

    /// <summary>
    /// Plays the players walking sound and
    /// randomizes its volume and pitch for effect
    /// </summary>
    private void UpdateWalkSound()
    {
        // Checks if the controller is ground, if its velocity magnitude
        // is more than 2 and if the audioSouce is not playing
        if (controller.isGrounded && controller.velocity.magnitude > 2f &&
            !audioSource.isPlaying)
        {
            // Randomizes the audioSource volume and pitch for effect
            audioSource.volume = Random.Range(0.8f, 1f);
            audioSource.pitch = Random.Range(0.8f, 1.1f);

            // Plays the audioSource
            audioSource.Play();
        }
    }

    /// <summary>
    /// Handles the player death
    /// </summary>
    private void Kill()
    {
        // Sets acceleration and velocity values to Vector3.zero
        acceleration = Vector3.zero;
        velocity = Vector3.zero;

        // Disables the players controller
        controller.enabled = false;

        // Resets the players position to its spawn position
        transform.localPosition = spawn;
        // Enables the players controller
        controller.enabled = true;
    }

    /// <summary>
    /// Checks when the player collides with a trigger
    /// </summary>
    /// <param name="col">collider that collided with the player</param>
    private void OnTriggerEnter(Collider col)
    {
        // Checks it the colliders tag is "Finish"
        if (col.tag == "Finish")
        {
            // Goes back to the MainMenu
            sceneManager.MainMenu();

            // Unlocks the cursor and makes it visible
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}