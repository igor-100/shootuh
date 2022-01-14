using UnityEngine;

public class EnemyCM : Enemy
{
    [SerializeField] private Transform gunTransform;
    //TODO: extract it to the separate properties + projectile type
    [SerializeField] private float projectileSpeed = 12f;

    protected override EnemyProperties InitProperties()
    {
        return CompositionRoot.GetConfiguration().GetEnemyCMProperties();
    }

    // called in Animator
    public void Attack()
    {
        var projectileObj = ResourceManager.GetPooledObject<IProjectile, EComponents>(EComponents.Projectile_Red);
        projectileObj.transform.position = gunTransform.position;
        projectileObj.GetComponent<Rigidbody>().velocity = gunTransform.forward * projectileSpeed;
    }
}
