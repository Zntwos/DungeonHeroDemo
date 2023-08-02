using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework
{
    public interface ITimerSystem : ISystem
    {
        Timer AddTimer(Action onfinished, float delayTime, bool isLoop);
    }
    public class TimerSystem : AbstractSystem, ITimerSystem
    {
        private List<Timer> _timerList = new List<Timer>();
        private Queue<Timer> _timerQueue = new Queue<Timer>();

        public Timer AddTimer(Action onfinished, float delayTime, bool isLoop)
        {
            var timer = _timerQueue.Count == 0 ? new Timer() : _timerQueue.Dequeue();
            timer.TimerStart(onfinished, delayTime, isLoop);
            _timerList.Add(timer);
            return timer;
        }
        private void TimerUpdate()
        {
            if (_timerList.Count == 0) return;
            for(int i = 0; i < _timerList.Count; i++)
            {
                if(_timerList[i].isFinish)
                {
                    _timerQueue.Enqueue(_timerList[i]);
                    _timerList.RemoveAt(i);
                    continue;
                }
                _timerList[i].TimerUpdate();
            }
        }
        protected override void OnInit()
        {
            MonoManager.GetInstance().AddUpdateListener(TimerUpdate);
        }
    }
    public class Timer
    {
        private Action onFinished;
        private float _finishTime;
        private float _delayTime;
        private bool _Loop;
        private bool _isFinish;
        public bool isFinish => _isFinish;

        public void TimerStart(Action onFinished, float delayTime, bool isLoop)
        {
            this.onFinished = onFinished;
            _finishTime = Time.time + delayTime;
            _delayTime = delayTime;
            _Loop = isLoop;
            _isFinish = false;
        }
        public void TimerStop() => _isFinish = true;
        public void TimerUpdate()
        {
            if (_isFinish) return;
            if (Time.time < _finishTime) return;
            if (!_Loop) TimerStop();
            else _finishTime = Time.time + _delayTime;
            onFinished?.Invoke();
        }
    }

}
