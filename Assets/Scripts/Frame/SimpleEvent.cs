/*
* ┌──────────────────────────────────┐
* │  描    述: 简单事件中心                      
* │  类    名: SimpleEvent.cs       
* │  创    建: By 4463fger                     
* └──────────────────────────────────┘
*/

using System.Collections.Generic;

namespace Frame
{
    /// <summary>
    /// 事件中心
    /// </summary>
    public static class SimpleEvent
    {
        // 存储普通事件
        private static readonly Dictionary<string, System.Action<object>> _messageDic = new();    
        // 存储一次性事件
        private static readonly Dictionary<string, System.Action<object>> _tempMessageDic = new();   
        // 存储特定对象的事件
        private static readonly Dictionary<System.Object, Dictionary<string, System.Action<object>>> _objMessageDic = new();
        // 存储多参数事件
        private static readonly Dictionary<string, System.Action<object[]>> _messageArgsDic = new();    

        /// <summary>
        /// 注册普通事件
        /// </summary>
        public static void RegisterEvent(string eventName , System.Action<object> callBack)
        {
            if (_messageDic.ContainsKey(eventName))
            {
                _messageDic[eventName] += callBack;
            }
            else
            {
                _messageDic.Add(eventName, callBack);
            }
        }
        
        /// <summary>
        /// 移除普通事件
        /// </summary>
        public static void UnRegisterEvent(string eventName, System.Action<object> callBack)
        {
            if (_messageDic.ContainsKey(eventName))
            {
                _messageDic[eventName] -= callBack;
                if (_messageDic[eventName] == null)
                {
                    _messageDic.Remove(eventName);
                }
            }
        }
        
        /// <summary>
        ///  执行事件
        /// </summary>
        public static void PostEvent(string eventName, object arg = null)
        {
            if (_messageDic.ContainsKey(eventName))
            {
                _messageDic[eventName].Invoke(arg);
            }            
        }

        /// <summary>
        /// 注册特定对象的事件
        /// </summary>
        public static void RegisterEvent(System.Object listenerObj , string eventName , System.Action<object> callBack)
        {
            if (_objMessageDic.ContainsKey(listenerObj))
            {
                if (_objMessageDic[listenerObj].ContainsKey(eventName))
                {
                    _objMessageDic[listenerObj][eventName] += callBack;
                }
                else
                {
                    _objMessageDic[listenerObj].Add(eventName, callBack);
                }
            }
            else
            {
                Dictionary<string, System.Action<object>> _tempDic = new Dictionary<string, System.Action<object>>();
                _tempDic.Add(eventName, callBack);
                _objMessageDic.Add(listenerObj, _tempDic);
            }
        }
        
        /// <summary>
        /// 移除特定对象的事件
        /// </summary>
        public static void UnRegisterEvent(System.Object listenerObj, string evneName, System.Action<object> callBack)
        {
            if (_objMessageDic.ContainsKey(listenerObj))
            {
                if (_objMessageDic[listenerObj].ContainsKey(evneName))
                {
                    _objMessageDic[listenerObj][evneName] -= callBack;
                    if (_objMessageDic[listenerObj][evneName] == null)
                    {
                        _objMessageDic[listenerObj].Remove(evneName);
                        if (_objMessageDic[listenerObj].Count == 0)
                        {
                            _objMessageDic.Remove(listenerObj);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清楚对象的所有事件
        /// </summary>
        /// <param name="listenerObj"></param>
        public static void ClearObjectAllEvent(System.Object listenerObj)
        {
            if (_objMessageDic.ContainsKey(listenerObj))
            {
                _objMessageDic.Remove(listenerObj);
            }
        }

        /// <summary>
        /// 执行特定对象的事件
        /// </summary>
        public static void PostEvent(System.Object listenerObj, string eventName, object arg = null)
        {
            if (_objMessageDic.ContainsKey(listenerObj))
            {
                if (_objMessageDic[listenerObj].ContainsKey(eventName))
                {
                    _objMessageDic[listenerObj][eventName].Invoke(arg);
                }               
            }
        }
        
        /// <summary>
        /// 添加临时事件
        /// </summary>
        public static void RegisterTempEvent(string eventName, System.Action<object> callBack)
        {
            if (_tempMessageDic.ContainsKey(eventName))
            {
                _tempMessageDic[eventName] = callBack; //添加临时事件，是覆盖 不是 添加
            }
            else
            {
                _tempMessageDic.Add(eventName, callBack);
            }
        }
        
        /// <summary>
        /// 触发一次性事件
        /// </summary>
        public static void PostTempEvent(string eventName, object arg = null)
        {
            if (_tempMessageDic.ContainsKey(eventName))
            {
                _tempMessageDic[eventName].Invoke(arg);
                _tempMessageDic[eventName] = null;
                _tempMessageDic.Remove(eventName);
            }
        }

        /// <summary>
        /// 清除所有事件
        /// </summary>
        public static void ClearAllEvent()
        {
            _objMessageDic.Clear();
            _messageDic.Clear();
            _tempMessageDic.Clear();
            _messageArgsDic.Clear();
        }

        /// <summary>
        /// 注册多个参数的事件
        /// </summary>
        public static void RegisterEvent(string eventName,System.Action<object[]> callBack)
        {
            if (_messageDic.ContainsKey(eventName))
            {
                _messageArgsDic[eventName] += callBack;
            }
            else
            {
                _messageArgsDic.Add(eventName, callBack);
            }
        }
        
        /// <summary>
        /// 移除多个参数事件的监听
        /// </summary>
        public static void UnRegisterEvent(string eventName,System.Action<object[]> callBack)
        {
            if (_messageArgsDic.ContainsKey(eventName))
            {
                _messageArgsDic[eventName] -= callBack;
                if (_messageArgsDic[eventName] == null)
                {
                    _messageArgsDic.Remove(eventName);
                }
            }
        }

        /// <summary>
        /// 触发多参事件
        /// </summary>
        public static void PostEvent(string eventName,params object[] args)
        {
            if (_messageArgsDic.ContainsKey(eventName))
            {
                _messageArgsDic[eventName].Invoke(args);
            }    
        }
    }
}