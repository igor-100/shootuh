using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Core.Audio;

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
    private IPlayerInput PlayerInput;
    private IAudioManager AudioManager;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 movement;
    private bool isMoving;
    private float nextTimeToStep = 0f;

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
        PlayerInput = CompositionRoot.GetPlayerInput();
        AudioManager = CompositionRoot.GetAudioManager();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Init(WarriorProperties warriorProperties)
    {
        this.warriorProperties = warriorProperties;

        SaveManager.AddToSaveRegistry(this);
        PlayerInput.Move += OnMove;

        currentHealth = warriorProperties.HealthStat.BaseValue;
        currentMoveSpeed = warriorProperties.MoveSpeedStat.BaseValue;
    }

    public void Load(string jsonProperties)
    {
        JObject jObject = JObject.Parse(jsonProperties);

        var warriorProperties = jObject.SelectToken("warriorProperties").ToObject<WarriorProperties>();
        Init(warriorProperties);

        currentHealth = jObject.SelectToken("currentHealth").ToObject<float>();
        HealthPercentChanged((float)currentHealth / warriorProperties.HealthStat.Value);

        currentMoveSpeed = jObject.SelectToken("currentMoveSpeed").ToObject<float>();

        var curPos = jObject.SelectToken("currentPositionXZ").ToObject<float[]>();
        transform.position = new Vector3(curPos[0], 0, curPos[1]);
    }

    void Update()
    {
        UpdateIsMoving();
        OnIsMoving();
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

    private void OnIsMoving()
    {
        if (isMoving)
        {
            PlayStep();
            animator.SetBool(IsRunning, true);
        }
        else
        {
            animator.SetBool(IsRunning, false);
        }
    }

    private void PlayStep()
    {
        if (Time.time >= nextTimeToStep)
        {
            nextTimeToStep = Time.time + 0.29f;
            AudioManager.PlayEffect(EAudio.Run);
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
        AudioManager.PlayEffect(EAudio.Death);
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
