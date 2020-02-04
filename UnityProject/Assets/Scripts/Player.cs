using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;
    [Header("玩家資料")]
    public PlayerData data;

    private Rigidbody rig;
    private FixedJoystick joystick;
    private Animator ani;               // 動畫控制器元件
    private Transform target;           // 目標物件
    private LevelManager levelManager;
    private HpValueManager HpValueManager;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();  // 動畫控制器 = 取得元件<動畫控制器>()
        joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
        
        //target = GameObject.Find("目標").GetComponent<Transform>();
        target = GameObject.Find("目標").transform;

        levelManager = FindObjectOfType<LevelManager>();    // 透過類行尋找物件 (場景上只有一個)
        HpValueManager = GetComponentInChildren<HpValueManager>();
    }

    // 固定更新：一秒執行 50 次 - 處理物理行為
    private void FixedUpdate()
    {
        Move();
    }

    //  碰到物件身上有 IsTrigger 碰撞器執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            StartCoroutine(levelManager.NextLevel());   // 協程方法，必須要用 啟動協程
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float v = joystick.Vertical;
        float h = joystick.Horizontal;

        rig.AddForce(h * speed, 0, v * speed);

        ani.SetBool("跑步開關", v != 0 || h != 0);  // 動畫控制器.設定布林值("參數名稱"，布林值)

        Vector3 pos = transform.position;                               // 玩家做標 = 變形.座標
        target.position = new Vector3(pos.x + h, 0.3f, pos.z + v);      // 目標.座標 = 新 三維向量(玩家.X + 水平，0.3f，玩家.Z + 垂直)

        //transform.LookAt(target); // 這會吃土

        Vector3 posTarget = new Vector3(target.position.x, transform.position.y, target.position.z);    // 目標座標 = 新 三維向量(目標.X，玩家.Y，目標.Z)
        transform.LookAt(posTarget);                                                                    // 玩家變形.看著(目標座標)
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收的傷害值</param>
    public void Hit(float damage)
    {
        data.hp -= damage;
        HpValueManager.SetHp(data.hp, data.hpMax);
        StartCoroutine(HpValueManager.ShowValue(damage, "-", Color.white));
    }
}
