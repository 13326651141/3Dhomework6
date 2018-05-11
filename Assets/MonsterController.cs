using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public List<Vector3> path;//储存怪物巡逻路径
    private int currentNode = 0;//怪物在路径上的当前节点
    private int speed = 4;//初始速度
    private Rigidbody rb;
    private Animator ani;
    public delegate void hitPlayer();
    public delegate void Score();
    public static event hitPlayer hitPlayerEvent;//撞击到玩家的事件
    public static event Score scoreEvent;//玩家离开追踪范围
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        setPath();
    }

    void FixedUpdate()
    {
        Vector3 target = path[currentNode];
        Vector3 V = target - transform.position;//到下个目标的向量
        V = V.normalized;//单位化
        Quaternion rotation = Quaternion.LookRotation(V, Vector3.up);
        transform.rotation = rotation;//面向下个目标
        rb.velocity = new Vector3(V.x, 0, V.z) * speed;//设置刚体向下个目标移动
        if (Vector3.Distance(transform.position, target) < 0.1)
        {//到达下个目标
            currentNode = (currentNode + 1) % path.Count;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "monster")
        {//撞击到同类，转下下个目标，但是有可能两个怪物就贴在一起移动不了
            currentNode = (currentNode + 1) % path.Count;
        }
        if (other.gameObject.tag == "Walls")
        {//撞到墙，重新生成巡逻路线
            setPath();
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {//与玩家碰撞，播放攻击动画，触发撞击玩家事件
            ani.SetTrigger("attack");
            if (hitPlayerEvent != null)
            {
                hitPlayerEvent();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {//玩家进入追踪范围
            path.Clear();
            path.Add(other.gameObject.transform.position);//目标是玩家所在位置
            ani.SetBool("run", true);//播放奔跑动画
            speed = 6;//速度加快
            currentNode = 0;//当前路线节点为0
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {//玩家逃出追踪范围
            setPath();//重新生产巡逻路线
            ani.SetBool("run", false);//停止奔跑
            speed = 4;//速度变为巡逻速度
            if (scoreEvent != null)
            {//触发加分事件
                scoreEvent();
            }
        }
    }

    private void setPath()
    {//生成巡逻正方形的巡逻路线
        path.Clear();
        System.Random ran = new System.Random();
        int length = ran.Next(10, 15);//随机生成长度为10~15的边长
        Vector3 right = new Vector3(transform.right.x, 0, transform.right.z).normalized;
        Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        Vector3 pos = transform.position - forward * 2;
        path.Add(pos);
        path.Add(pos + right * length);
        path.Add(pos - forward * length + right * length);
        path.Add(pos - forward * length);
        currentNode = 0;
    }
}