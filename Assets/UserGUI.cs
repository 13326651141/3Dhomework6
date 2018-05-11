using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    private float speed;//Player移动的速度
    private bool isGameOver = false;
    public Text scoreText;//显示分数
    public Text gameOverText;//显示游戏结束
    public ScoreRecorder sR;//计分员

    void Start()
    {
        action = GameDirector.getInstance().currentSceneController as IUserAction;
        speed = 10;//初始化Player移动速度
        sR = Singleton<ScoreRecorder>.Instance;//获取计分员单例
    }

    void Update()
    {
        if (isGameOver)
        {//显示结束游戏
            gameOverText.text = "Game Over!";
            return;
        }
        float offsetX = Input.GetAxis("Horizontal");//获取水平轴上的增量，目的在于控制玩家的转向
        action.changeDirection(offsetX * 3);//改变玩家方向
        isGameOver = action.getGameOver();//检查游戏是否结束
        scoreText.text = "Score: " + sR.getScore();//显示分数
    }

    void FixedUpdate()
    {//移动玩家实际是改变刚体的速度，所以应当在FixedUpdate里写
        if (isGameOver)
        {//如果游戏结束，则不能进行下面的操作
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {//玩家向前移动
            action.movePlayer(Direction.forward, speed);
        }
        else
        {//停止玩家，实际就是设置状态机进入等待的状态，而且只能在这里停止
            action.stopPlayer();
        }
        if (Input.GetKey(KeyCode.S))
        {//玩家向后移动
            action.movePlayer(Direction.backward, speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {//玩家跳跃
            action.jump();
        }
    }

}
