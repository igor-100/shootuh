using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[JsonObject(MemberSerialization.OptIn)]
public class Warrior : MonoBehaviour, IWarrior
{
    private const string IsRunning = "isRunning";
    private const float GameOverDelay = 2f;

    public event Action<IAlive> Died = warrior => { };
    public event Action StartedDying = () => { };
    public event Action<float> HealthPercentChanged = percent => { };

    [SerializeField] private WeaponHolder weaponHolder;

    private ISaveManager SaveManager;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private bool isMoving;

    [JsonProperty]
    private WarriorProperties warriorProperties;

    [JsonProperty]
    private float currentHealth;

    [JsonProperty]
    private float currentMoveSpeed;

    [JsonProperty]
    private float[] currentPositionXZ;

    public CharacterStat HealthStat => warriorProperties.HealthStat;
    public CharacterStat MoveSpeedStat => warriorProperties.MoveSpeedStat;
    public Transform Transform => transform;
    public IWeaponHolder WeaponHolder => weaponHolder;

    private void Awake()
    {
        SaveManager = CompositionRoot.GetSaveManager();
        var playerInput = CompositionRoot.GetPlayerInput();

        SaveManager.AddToSaveRegistry(this);

        playerInput.Move += OnMove;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Init(string jsonProperties)
    {
        JObject jObject = JObject.Parse(jsonProperties);
        this.warriorProperties = jObject.SelectToken("warriorProperties").ToObject<WarriorProperties>();

        var currentHealthToken = jObject.SelectToken("currentHealth");
        if (currentHealthToken == null)
        {
            currentHealth = warriorProperties.HealthStat.Value;
        }
        else
        {
            currentHealth = currentHealthToken.ToObject<float>();
            HealthPercentChanged((float)currentHealth / warriorProperties.HealthStat.Value);
        }

        var currentMoveSpeedToken = jObject.SelectToken("currentMoveSpeed");
        if (currentMoveSpeedToken == null)
        {
            currentMoveSpeed = warriorProperties.MoveSpeedStat.Value;
        }
        else
        {
            currentMoveSpeed = currentMoveSpeedToken.ToObject<float>();
        }

        var curPos = jObject.SelectToken("currentPositionXZ").ToObject<float[]>();
        transform.position = new Vector3(curPos[0], 0, curPos[1]);
    }

    void Update()
    {
        UpdateIsMoving();
        AnimationMove();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnMove(Vector2 moveVector)
    {
        movement.x = moveVector.x;
        movement.z = moveVector.y;
    }

    private void Move()
    {
        rb.MovePosition(rb.position + currentMoveSpeed * Time.fixedDeltaTime * movement);
    }


    private void UpdateIsMoving()
    {
        isMoving = movement.x != 0 || movement.z != 0;
    }

    private void AnimationMove()
    {
        if (isMoving)
        {
            animator.SetBool(IsRunning, true);
        }
        else
        {
            animator.SetBool(IsRunning, false);
        }
    }

    public void Rotate(Vector3 rotationPoint)
    {
        var lookDir = rotationPoint - rb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            Hit(projectile.Damage);
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / warriorProperties.HealthStat.Value);
        Debug.Log("Hit. Current health: " + currentHealth + ", healthstat value: " + warriorProperties.HealthStat.Value);
        if (currentHealth <= 0)
        {
            StartDying();    
        }
    }

    private void StartDying()
    {
        StartedDying();
        currentHealth = 0;
        gameObject.SetActive(false);
        Invoke("FinallyDie", GameOverDelay);
    }

    private void FinallyDie()
    {
        Died(this);
    }

    public void Heal(float healValue)
    {
        if (healValue >= HealthStat.Value - currentHealth)
        {
            currentHealth = HealthStat.Value;
        }
        else
        {
            currentHealth += healValue;
        }
        HealthPercentChanged((float)currentHealth / warriorProperties.HealthStat.Value);
    }

    public void PrepareSaveData()
    {
        this.currentPositionXZ = new float[] { transform.position.x, transform.position.z };
    }
}
