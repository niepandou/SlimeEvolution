/*
* ┌──────────────────────────────────┐
* │  描    述: 场景系统                      
* │  类    名: SceneSystem.cs       
* │  创    建: By 4463fger                     
* └──────────────────────────────────┘
*/

using System;

namespace Frame
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public static class SceneSystem
    {
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        /// <param name="callBack">回调函数</param>
        public static void LoadScene(string sceneName, Action callBack = null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            callBack?.Invoke();
        }
    }
}