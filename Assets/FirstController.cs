using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, SceneController, IUserAction
{
    public GameObject player;//玩家对象预制
    public List<GameObject> monsters;//存储所有的巡逻兵（怪物）
    public myFactory mF;//工厂
    private bool isGameOver = false;//标示游戏是否结束
    private bool isJump = true;//玩家是否能够跳跃（防止多级跳跃）


    void Awake()
    {
        GameDirector director = GameDirector.getInstance();
        director.currentSceneController = this;
        //把场景控制权交给FirstController

    }

    // Use this for initialization
    void Start () {
        mF = Singleton<myFactory>.Instance;//获得工厂单例
        monsters = mF.getMonsters();//从工厂获得所有的怪物
        MonsterController.hitPlayerEvent += gameOver;//订阅怪物撞击玩家的事件
        Player.upEvent += enableJump;//订阅玩家跳跃事件
        Player.downEvent += disableJump;//订阅玩家跳跃结束事件
    }

    // Update is called once per frame
    public void movePlayer(Direction direction, float speed)
    {
        player.GetComponent<Animator>().SetBool("run", true);//播放跑步动画
        Rigidbody rb = player.GetComponent<Rigidbody>();
        float y = rb.velocity.y;//记录刚体y轴方向上的速度，按前进或后退的时候y轴的速度不能改变
        switch (direction)
        {
            case Direction.forward://前进
                rb.velocity = new Vector3(player.transform.forward.x * speed, y, player.transform.forward.z * speed);
                break;
            case Direction.backward://后退
                rb.velocity = new Vector3(player.transform.forward.x * speed * -1, y, player.transform.forward.z * speed * -1);
                break;
        }
    }

    public void jump()
    {
        if (isJump)
        {//落到地面时才能再次跳跃
            player.GetComponent<Animator>().SetTrigger("jump");
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
    }

    public void changeDirection(float offsetX)
    {//控制转向，通过改变欧拉角实现
        float x = player.transform.localEulerAngles.y + offsetX;
        float y = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3(y, x, 0);
    }

    public void stopPlayer()
    {//停止播放奔跑动画
        player.GetComponent<Animator>().SetBool("run", false);
    }

    public bool getGameOver()
    {
        return isGameOver;
    }

    public void gameOver()
    {//游戏结束
        player.GetComponent<Animator>().SetTrigger("death");
        player.GetComponent<Rigidbody>().isKinematic = true;
        this.isGameOver = true;
    }

    public void enableJump()
    {//设置能够跳跃
        isJump = true;
    }

    public void disableJump()
    {//设置不能跳跃
        isJump = false;
    }

    public void LoadResources()
    {
        //
    }
}
