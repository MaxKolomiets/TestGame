using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float doubleAttackCooldown;
    public float DoubleAttackCooldown => doubleAttackCooldown; //под≥бний запис € роблю б≥льше по звичц≥, € розум≥ю, що повне в≥докремленн€
                                                               //≥ ≥нкапсул€ц≥€ у тестовому завданн≥ не потр≥бн≥
    public float Hp;
    public float SimpleAttackDamage;
    public float DoubleAttackDamage;
    public float AtackSpeed;
    public float AttackRange = 2;

    private bool isDead = false;
    public Animator AnimatorController;
    private PlayerInput input;
    public CanUnitAttack CanUnitAttack = new();

    private void Awake()
    {
        input = new PlayerInput();
        input.Player.SimpleAttack.performed += SimpleAttack_performed;
        input.Player.DoubleAttack.performed += DoubleAttack_performed;
    }

    private void DoubleAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!isDead && CanUnitAttack.IsSimpleAttackAnimationFinished && 
            CanUnitAttack.IsDoubleAttackAnimationFinished &&
            CanUnitAttack.IsDoubleAttackCooldownFinished)
        {
            DoubleAttack();
        }
    }

    public void AddHP(int hp) {
        Hp += hp;
    }

    private void SimpleAttack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

        if (!isDead && CanUnitAttack.IsSimpleAttackAnimationFinished && CanUnitAttack.IsDoubleAttackAnimationFinished)
        {
            SimpleAttack();
        }
    }

    // This methods called from simpleAnimation1
    public void StartSimpleAttack()
    {
        CanUnitAttack.StartSimpleAttack();
    }
    public void EndSimpleAttack() {
        CanUnitAttack.EndSimpleAttack();
    }
    // This methods called from sword double attack1
    public void StartDoubleAttack()
    {
        CanUnitAttack.StartDoubleAttack();
    }
    public void EndDoubleAttack()
    {
        CanUnitAttack.EndDoubleAttack(doubleAttackCooldown);
    }

    private void Move() {
        Vector2 direction = input.Player.Move.ReadValue<Vector2>();
        float scaledSpeed = moveSpeed * Time.deltaTime;
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        gameObject.transform.position += moveDirection * scaledSpeed;
    }
    private void DoubleAttack() {
        Enemy closestEnemie = GetClosestEnemie();
        if (CheckEnemieNearPlayer(closestEnemie)) {
            transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);
            AnimatorController.SetTrigger("DoubleAttack");
            closestEnemie.Hp -= DoubleAttackDamage;
        }
    }
    private void SimpleAttack()
    {
        var closestEnemie = GetClosestEnemie();
        if (CheckEnemieNearPlayer(closestEnemie))
        {
            transform.transform.rotation = Quaternion.LookRotation(closestEnemie.transform.position - transform.position);
            closestEnemie.Hp -= SimpleAttackDamage;
        }
        AnimatorController.SetTrigger("Attack");
    }
    public bool CheckEnemieNearPlayer() {
        return CheckEnemieNearPlayer(GetClosestEnemie());
    }
    private bool CheckEnemieNearPlayer(Enemy closestEnemie) {
        if (closestEnemie != null)
        {
            var distance = Vector3.Distance(transform.position, closestEnemie.transform.position);
            if (distance <= AttackRange)
            {
                return true;
            }
        }
        return false;
    }
    private Enemy GetClosestEnemie() {
        var enemies = SceneManager.Instance.Enemies;
        Enemy closestEnemie = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            var enemie = enemies[i];
            if (enemie == null)
            {
                continue;
            }

            if (closestEnemie == null)
            {
                closestEnemie = enemie;
                continue;
            }

            var distance = Vector3.Distance(transform.position, enemie.transform.position);
            var closestDistance = Vector3.Distance(transform.position, closestEnemie.transform.position);

            if (distance < closestDistance)
            {
                closestEnemie = enemie;
            }

        }
        return closestEnemie;   
    }




    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        else {
            Move();
        }

        if (Hp <= 0)
        {
            Die();
            return;
        }
    }

    private void Die()
    {
        isDead = true;
        AnimatorController.SetTrigger("Die");
        SceneManager.Instance.GameOver();
    }
}
