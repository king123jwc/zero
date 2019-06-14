using UnityEngine;
using System.Collections;

public class FishSpawn : MonoBehaviour {

    /// <summary>
    /// 生成计时器
    /// </summary>
    public float timer = 0;

    /// <summary>
    /// 最大生成数量
    /// </summary>
    public int max_fish = 30;
    /// <summary>
    /// 当前鱼的数量
    /// </summary>
    public int fish_count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // 重新计时
            timer = 2.0f;

            // 如果鱼的数量达到最大数量则返回
            if (fish_count >= max_fish)
                return;

            // 随机1、2、3产生不同的鱼
            int index = 1 + (int)(Random.value * 3.0f);
            if (index > 3)
                index = 3;
            // 更新鱼的数量
            fish_count++;
            // 读取鱼的prefab
            GameObject fishprefab = (GameObject)Resources.Load("fish " + index);

            float cameraz = Camera.main.transform.position.z;
            // 鱼的初始随机位置
            Vector3 randpos = new Vector3(Random.value, Random.value, -cameraz);
            randpos = Camera.main.ViewportToWorldPoint(randpos);

            // 鱼的随机初始方向
            Fish.Target target = Random.value > 0.5f ? Fish.Target.Right : Fish.Target.Left;
            Fish f = Fish.Create(fishprefab, target, randpos);

            // 注册鱼的死亡消息
            f.OnDeath+=OnDeath;
            
        }
        
	}

    void OnDeath( Fish fish )
    {
        // 更新鱼的数量
        fish_count--;
    }

}
