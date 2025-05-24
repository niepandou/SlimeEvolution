/*
* ┌──────────────────────────────────┐
* │  描    述:                       
* │  类    名: 单例TEST.cs       
* │  创    建: By 4463fger                     
* └──────────────────────────────────┘
*/

using System;
using UnityEngine;

namespace TEST
{
    public class Test01_Singleton : MonoBehaviour
    {
        private void Awake()
        {
            // [0]：继承
            // 想让哪个东西单例，那么就让继承SingletonMono
            // 比如public class SkillManager : SingletonMono<SkillManager>
            
            // [1]: 使用
            // 比如这个地方，我想调用SkillManager
            // 如果SkillManager是单例
            // 那么直接
            // SkillManager.Instance.
            // 后边能访问到里边的成员了就
        }
    }
}