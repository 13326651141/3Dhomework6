public class Timer

{

    bool _isTicking;//是否在计时中

    public float _currentTime;//当前时间

    public float _endTime;//结束时间

    public delegate void EventHander();
     


    public Timer(float second)

    {

        _currentTime = 0;

        _endTime = second;

    }

    /// <summary>

    /// 开始计时

    /// </summary>

    public void StartTimer()

    {

        _isTicking = true;

    }

    /// <summary>

    /// 更新中

    /// </summary>

    public void UpdateTimer(float deltaTime)

    {

        if (_isTicking)

        {

            _currentTime += deltaTime;

            if (_currentTime > _endTime)

            {

                _isTicking = false;


            }

        }

    }

    /// <summary>

    /// 停止计时

    /// </summary>

    public void StopTimer()

    {

        _isTicking = false;

    }

    /// <summary>

    /// 持续计时

    /// </summary>

    public void ContinueTimer()

    {

        _isTicking = true;

    }

    /// <summary>

    /// 重新计时

    /// </summary>

    public void ReStartTimer()

    {

        _isTicking = true;

        _currentTime = 0;

    }

    /// <summary>

    /// 重新设定计时器

    /// </summary>

    public void ResetEndTimer(float second)

    {

        _endTime = second;

    }

}