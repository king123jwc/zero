using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

    // 射击计时器
    float m_shootTimer = 0;

	void Update () {
         UpdateInput();
	}

    void UpdateInput()
    {
        m_shootTimer -= Time.deltaTime; // 更新射击间隔时间计时

        // 获得鼠标位置
        Vector3 ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);

        // 大炮的位置
        Vector3 mypos = this.transform.position;

        // 按鼠标左键开火
        if (Input.GetMouseButton(0))
        {
            // 计算鼠标位置与大炮位置之间的角度
            Vector2 targetDir = ms - mypos;
            float angle = Vector2.Angle(targetDir, Vector3.up);
            if (ms.x > mypos.x)
                angle = -angle;
            this.transform.eulerAngles = new Vector3(0, 0, angle);


            if (m_shootTimer <= 0)
            {
                m_shootTimer = 0.1f;  // 每隔0.1秒可以射击一次

                // 开火，创建子弹实例
                Fire.Create(this.transform.TransformPoint(0, 1, 0), new Vector3(0, 0, angle));
            }
        }
    }
}
