using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public PlayerInputSystem actionMap;
    public Animator animator;

    private Vector2 moveDirection = Vector2.zero;
    private bool isAtk = false;

    // Start is called before the first frame update
    void Start()
    {
        actionMap = new PlayerInputSystem();
        actionMap.Enable();

        actionMap.Player.Fire.performed += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = actionMap.Player.Move.ReadValue<Vector2>();
 
        if (moveDirection.x > 0 )
        {
            animator.SetBool("isMove", true);
        } else
        {
            animator.SetBool("isMove", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired");
        StartCoroutine(ShootDelayCoroutine());
    }

    private IEnumerator ShootDelayCoroutine()
    {
        animator.SetBool("isAtk", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isAtk", false);
    }
}
