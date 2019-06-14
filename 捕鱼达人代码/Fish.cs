using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

    protected float m_moveSpeed = 2.0f; // 鱼的移动速度
    protected int m_life = 10;  // 生命值

    public enum Target  // 移动方向
    {
        Left=0,
        Right=1
    }
    public Target m_target = Target.Right;  //  当前移动目标（方向）
    public Vector3 m_targetPosition;  // 目标位置

    public delegate void VoidDelegate( Fish fish );
    public VoidDelegate OnDeath;  // 鱼死亡回调

    // 静态函数, 创建一个Fish实例
    public static Fish Create(GameObject prefab, Target target, Vector3 pos )
    {
        GameObject go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
        Fish fish = go.AddComponent<Fish>();
        fish.m_target = target;

        return fish;
    }

    // 受到伤害
    public void SetDamage( int damage )
    {
        m_life -= damage;
        if (m_life <= 0)
        {

            GameObject prefab = Resources.Load<GameObject>("explosion");
            GameObject explosion = (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation);
            Destroy(explosion, 1.0f);

            OnDeath(this);
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {

        SetTarget();
	}
    // 设置移动目标
    void SetTarget()
    {
        // 随机值
        float rand = Random.value;

        // 设置Sprite翻转方向
        Vector3 scale = this.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (m_target == Target.Right ? 1 : -1);
        this.transform.localScale = scale;

        float cameraz = Camera.main.transform.position.z;
        // 设置目标位置
        m_targetPosition = Camera.main.ViewportToWorldPoint(new Vector3((int)m_target, 1 * rand, -cameraz));
    }

    // Update is called once per frame
    void Update () {

        UpdatePosition();
	}
    // 更新当前位置
    void UpdatePosition()
    {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPosition, m_moveSpeed*Time.deltaTime);
        if (Vector3.Distance(pos, m_targetPosition) < 0.1f) // 如果移动到目标位置
        {
            m_target = m_target==Target.Left ? Target.Right : Target.Left;
            SetTarget();
        }
        this.transform.position = pos;
    }
}
