using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

    // 移动速度
    float m_moveSpeed = 10.0f;

    // 创建子弹实例,注意在实际项目的Update中不建议使用Resources.Load, Instantiate和Destroy
    public static Fire Create( Vector3 pos, Vector3 angle )
    {
        // 读取子弹Sprite prefab
        GameObject prefab = Resources.Load<GameObject>("fire");
        // 创建子弹Sprite实例
        GameObject fireSprite = (GameObject)Instantiate(prefab, pos, Quaternion.Euler(angle));
        Fire f = fireSprite.AddComponent<Fire>();
        Destroy(fireSprite, 2.0f);
        return f;
    }

	// Update is called once per frame
	void Update () {
        // 更新子弹位置
        this.transform.Translate(new Vector3(0, m_moveSpeed * Time.deltaTime, 0));
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        Fish f =other.GetComponent<Fish>();
        if (f == null)
            return;
        else   // 如果击中鱼
            f.SetDamage(1);
        Destroy(this.gameObject);
    }
}
