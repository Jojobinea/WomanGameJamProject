using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatic : EnemyController
{
    private GameObject _player;
    public float attackInterval = 2.0f;
    public float projectileSpeed = 5.0f;
    private GameObject projectile;

    [SerializeField] private Animator animator; // GameObject que representa o projétil

    // Adiciona uma variável para armazenar a vida do inimigo
    public int enemyHealth = 10;  // Vida inicial do inimigo

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        projectile = transform.Find("projetil").gameObject;

        if (projectile == null)
        {
            Debug.LogError("Projétil 'Circle' não encontrado diretamente no prefab");
        }
        else
        {
            projectile.SetActive(false);
        }

        // Certifique-se de que o Animator foi atribuído através do inspector
        if (animator == null)
        {
            Debug.LogError("Animator não foi atribuído ao prefab_Enemy_Shadow");
            this.enabled = false; // Desativa o script para prevenir erros
        }

        StartCoroutine(AttackRoutine());
    }


    private void Update()
    {


        if (_player != null)
        {
            // Calcula a direção do jogador a partir da posição do inimigo
            Vector3 direction = _player.transform.position - transform.position;

            // Calcula o ângulo necessário para olhar para o jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ajusta a rotação do inimigo para olhar para o jogador
            // Subtrai 90 graus se o sprite do inimigo estiver orientado verticalmente
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        if (projectile != null && _player != null)
        {
            // Ativar o projétil e posicioná-lo no local do inimigo
            projectile.SetActive(true);
            projectile.transform.position = transform.position;

            // Calcular a direção para o jogador
            Vector3 direction = (_player.transform.position - transform.position).normalized;

            // Adicionar força ao projétil para que ele se mova na direção do jogador
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colidiu com o jogador");
            TakeDamage(1);  // Reduz a vida do inimigo em 1
            ResetProjectile();  // Reseta o projétil para reutilização
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
            ResetProjectile();  // Reseta o projétil para reutilização
        }
    }

    private void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Vida restante do inimigo: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            animator.SetBool("isDead", true);
            Destroy(gameObject, 1f);  // Destrói o inimigo quando a vida chegar a zero
        }
    }

    private void ResetProjectile()
    {
        // Para o movimento do projétil e desativa-o para reutilização
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        projectile.SetActive(false);
    }
}
