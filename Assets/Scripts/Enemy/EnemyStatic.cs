using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyStatic : EnemyController
{
    [SerializeField] private float attackInterval = 2.0f;
    [SerializeField] private float projectileSpeed = 5.0f;

    [SerializeField] private Transform _spriteTransform;

    private GameObject projectile;

    private EnemyAnimEvents enemyAnimEvents;


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

        if (animator == null)
        {
            Debug.LogError("Animator não foi atribuído ao prefab_Enemy_Shadow");
            this.enabled = false;
        }

        enemyAnimEvents = GetComponent<EnemyAnimEvents>();

        StartCoroutine(AttackRoutine());
    }


    private void Update()
    {

        if (_player != null)
        {

            Vector3 direction = _player.transform.position - transform.position;

            // Calcula o ângulo necessário para olhar para o jogador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ajusta a rotação do inimigo para olhar para o jogador
            // Subtrai 90 graus se o sprite do inimigo estiver orientado verticalmente
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        _spriteTransform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
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
            projectile.SetActive(true);
            projectile.transform.position = transform.position;
            StartCoroutine(FollowPlayer(projectile, 1f)); // 2 segundos para o tempo de vida do projétil
        }
    }



    private IEnumerator FollowPlayer(GameObject proj, float lifetime)
    {
        float elapsedTime = 0;  // Tempo decorrido desde o lançamento do projétil

        while (proj != null && _player != null && elapsedTime < lifetime)
        {
            // Verificar se o projétil ainda está ativo
            if (proj.activeSelf)
            {
                Vector3 direction = (_player.transform.position - proj.transform.position).normalized;
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }
            }

            elapsedTime += Time.deltaTime;  // Incrementar o tempo decorrido
            yield return null;  // Espera pelo próximo frame
        }

        // Se o projétil ainda estiver ativo após o tempo especificado, desativá-lo
        if (proj != null && proj.activeSelf && elapsedTime >= lifetime)
        {
            ResetProjectile();  // Chamada para a função que desativa o projétil
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ProjetilPlayer"))
        {
            Debug.Log("Colidiu com o jogador");
            TakeDamage(1);  // Reduz a vida do inimigo em 1
            ResetProjectile();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede");
            ResetProjectile();
        }
    }

    private void ResetProjectile()
    {
        if (projectile != null)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Parar o movimento do projétil
            }
            projectile.SetActive(false);  // Desativar o projétil
        }
    }

    protected override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

}