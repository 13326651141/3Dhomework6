using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction : int { forward, backward, left, right }
//动作管理类


public interface IUserAction
{
    void movePlayer(Direction direction, float speed);
    //移动
    void jump();
    //跳
    void changeDirection(float offsetX);
    //改变方向
    void stopPlayer();
    //停止
    bool getGameOver();
    //结束
}