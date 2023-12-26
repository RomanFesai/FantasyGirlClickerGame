using Assets.Scripts.DungeonCrawlerScripts;
using Assets.Scripts.UI;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    public float collisionCheckRayLength;

    public KeyCode forward = KeyCode.W;
    public KeyCode back = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode turnRight = KeyCode.E;
    public KeyCode turnLeft = KeyCode.Q;

    PlayerMovement movement;
    [SerializeField] private GameObject controlButtons;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        if (!MobileInputManager.instance.IsMobileDevice())
        {
            controlButtons.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerStats.instance.isBattle)
            Movement();

        if (PlayerStats.instance.isBattle || GameOverWindow.gameOver)
            controlButtons.SetActive(false);
        else if(DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().dialogueIsPlaying && MobileInputManager.instance.IsMobileDevice())
            controlButtons.SetActive(true);
    }

    /// <summary>
    /// for keyboard
    /// </summary>
    private void Movement()
    {
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (Input.GetKeyDown(forward) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveForward();
        if (Input.GetKeyDown(back) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveBackward();
        if (Input.GetKeyDown(left) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveLeft();
        if (Input.GetKeyDown(right) && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveRight();
        if (Input.GetKeyDown(turnLeft)) movement.RotateLeft();
        if (Input.GetKeyDown(turnRight)) movement.RotateRight();
    }

    /// <summary>
    /// for touch controls
    /// </summary>
    public void MoveForward()
    {
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (!PlayerStats.instance.isBattle && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveForward();
    }

    public void MoveBackWard()
    {
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (!PlayerStats.instance.isBattle && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveBackward();
    }

    public void MoveLeft()
    {
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (!PlayerStats.instance.isBattle && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveLeft();
    }

    public void MoveRight()
    {
        int layer = 1 << LayerMask.NameToLayer("Default");
        if (!PlayerStats.instance.isBattle && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), collisionCheckRayLength, layer, QueryTriggerInteraction.Ignore)) movement.MoveRight();
    }

    public void RotateLeft()
    {
        if (!PlayerStats.instance.isBattle) movement.RotateLeft();
    }
    public void RotateRight()
    {
       if (!PlayerStats.instance.isBattle) movement.RotateRight();
    }
}
