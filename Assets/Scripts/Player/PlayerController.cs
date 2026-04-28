using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private float currentHP;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        

        if (playerData != null)
        {
            currentHP = playerData.maxHP;
        }
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {

        MovePlayer();
    }

    private void MovePlayer()
    {

        rb.linearVelocity = moveInput * playerData.moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
   
            TakeDamage(0.1f * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        

        currentHP = Mathf.Max(currentHP, 0);
        Debug.Log($"Player HP: {currentHP:F2}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead!");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}