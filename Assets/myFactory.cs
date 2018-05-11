using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFactory : MonoBehaviour
{//工厂用来生产怪物，本游戏只生产一次怪物。因为怪物不会死
    public GameObject monster;//怪物预制
    private int min_x = 6;//怪物初始位置x坐标最小值
    private int min_z = 10;//怪物初始位置z坐标最小值
    private int max_x = 86;//怪物初始位置x坐标最大值
    private int max_z = 86;//怪物初始位置z坐标最大值
    //用于分散怪物
    private int cellLengthX = 30;
    private int cellLengthZ = 29;
    //地图的行数和列数
    private int col = 3;
    private int row = 3;
    //每个格子怪物的数量
    private int MonsterNumEveryCell = 2;

    public List<GameObject> getMonsters()
    {
        List<GameObject> monsters = new List<GameObject>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //每个格子大致的坐标范围
                int rangeMinX = min_x + cellLengthX * i;
                int rangeMinZ = min_z + cellLengthZ * j;
                int rangeMaxX = min_x + cellLengthX * (i + 1);
                int rangeMaxZ = min_z + cellLengthZ * (j + 1);
                for (int k = 0; k < MonsterNumEveryCell; k++)
                {
                    //在每个格子范围产生怪物
                    System.Random ran = new System.Random(GetRandomSeed());
                    float x = ran.Next(rangeMinX, rangeMaxX);
                    while (x > max_x)
                    {
                        x = ran.Next(rangeMinX, rangeMaxX);
                    }
                    float z = ran.Next(rangeMinZ, rangeMaxZ);
                    while (z > max_z)
                    {
                        z = ran.Next(rangeMinZ, rangeMaxZ);
                    }
                    GameObject newMonster = Instantiate<GameObject>(monster);
                    newMonster.transform.position = new Vector3(x, 0, z);
                    monsters.Add(newMonster);
                }
            }
        }
        return monsters;
    }
    private int GetRandomSeed()
    {//获取随机数种子，网上抄来
        byte[] bytes = new byte[4];
        System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
        rng.GetBytes(bytes);
        return System.BitConverter.ToInt32(bytes, 0);
    }
}
