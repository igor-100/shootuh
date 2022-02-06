using UnityEngine;

public class EnemyHM : Enemy
{
    protected override EnemyProperties InitProperties()
    {
        return CompositionRoot.GetConfiguration().GetEnemyHMProperties();
    }

    // called in Animator
    public void Attack()
    {
        AudioManager.PlayEffect(EAudio.Enemy_Melee);
        RaycastHit objectHit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + enemyProperties.HitHeight, transform.position.z),
            transform.forward, out objectHit, enemyProperties.AttackRange, LayerMask.GetMask(PlayerMaskName)))
        {
            var warrior = objectHit.transform.GetComponent<IWarrior>();
            if (warrior != null)
            {
                warrior.Hit(currentDamage);
            }
        }
    }
}
