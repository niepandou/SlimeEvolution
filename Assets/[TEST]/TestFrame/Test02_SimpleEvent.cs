/*
* ┌──────────────────────────────────┐
* │  描    述:                       
* │  类    名: SimpleEvent.cs       
* │  创    建: By 4463fger                     
* └──────────────────────────────────┘
*/

using System;
using Frame;
using UnityEngine;

namespace TEST
{
    public class Test02_SimpleEvent : MonoBehaviour
    {
        // 事件需要提交注册
        // 比如这里是UI，这个地方去注册事件
        // 并且在合适的位置去取消注册
        private void Awake()
        {
            // 第一个参数string，是事件的名称
            // 第二个是监听事件的方法，外部传入的数据，在这个方法中进行操作
            SimpleEvent.RegisterEvent("血量变化",HpChanged);
        }

        // 比如，这里，玩家那么扣血后，就进行触发，吧自己血量传进来
        // SimpleEvent.PostEvent("血量变化",curHp,maxHp);
        // 传入的是obj，需要手动转化为你想要的类型
        private void HpChanged(object[] args)
        {
            int curHp = (int)args[0];
            int maxHp = (int)args[1];
            // 做血量UI
        }
    }
}