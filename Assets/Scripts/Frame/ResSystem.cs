/*
 * ┌──────────────────────────────────┐
 * │  描    述: 资源加载系统_基于AddressableAssets
 * │  类    名: ResSystem.cs
 * │  创    建: By 4463fger
 * └──────────────────────────────────┘
 */

using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Frame
{
    public static class ResSystem
    {
        #region 游戏物体

        /// <summary>
        /// 加载游戏物体
        /// </summary>
        /// <param name="keyName">对象池中的分组名称</param>
        /// <param name="parent">父物体</param>
        public static GameObject InstantiateGameObject(Transform parent ,string keyName)
        {
            GameObject go = Addressables.InstantiateAsync(keyName, parent).WaitForCompletion();
            go.name = keyName;
            return go;
        }

        #endregion
        
        #region 游戏Asset

        /// <summary>
        /// 加载Unity资源  如AudioClip Sprite 预制体
        /// 要注意，资源不在使用时候，需要调用一次Release
        /// </summary>
        /// <param name="assetName">AB资源名称</param>
        public static T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            return Addressables.LoadAssetAsync<T>(assetName).WaitForCompletion();
        }

        /// <summary>
        /// 异步加载Unity资源 AudioClip Sprite GameObject(预制体)
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetName">AB资源名称</param>
        /// <param name="callBack">回调函数</param>
        public static void LoadAssetAsync<T>(string assetName ,Action<T> callBack)
        {
            Addressables.LoadAssetAsync<T>(assetName).Completed += (handle) =>
            {
                OnLoadAssetAsyncCompleted(handle,callBack);
            };
        }

        private static void OnLoadAssetAsyncCompleted<T>(AsyncOperationHandle<T> handle, Action<T> callBack)
        {
            callBack?.Invoke(handle.Result);
        }

        /// <summary>
        /// 同步加载指定Key的所有资源
        /// 注意:批量加载时，如果释放资源要释放掉handle，直接去释放资源是无效的
        /// </summary>
        /// <typeparam name="T">加载类型</typeparam>
        /// <param name="keyName">一般是Lable</param>
        /// <param name="handle">用来Release时使用</param>
        /// <param name="callBackOnEveryOne">注意这里是针对每一个资源的回调</param>
        /// <returns>所有资源</returns>
        public static IList<T> LoadAssets<T>(string keyName, out AsyncOperationHandle<IList<T>> handle,
            Action<T> callBackOnEveryOne = null) where T : UnityEngine.Object
        {
            handle = Addressables.LoadAssetsAsync<T>(keyName,callBackOnEveryOne,true);
            return handle.WaitForCompletion();
        }

        /// <summary>
        /// 异步加载指定Key的所有资源
        /// 注意1:批量加载时，如果释放资源要释放掉handle，直接去释放资源是无效的
        /// 注意2:回调后使用callBack中的参数使用(.Result)即可访问资源列表
        /// </summary>
        /// <typeparam name="T">加载类型</typeparam>
        /// <param name="keyName">一般是lable</param>
        /// <param name="callBack">所有资源列表的统一回调，注意这是很必要的，因为Release时需要这个handle</param>
        /// <param name="callBackOnEveryOne">注意这里是针对每一个资源的回调,可以是Null</param>
        public static void LoadAssetAsync<T>(string keyName, Action<AsyncOperationHandle<IList<T>>> callBack,
            Action<T> callBackOnEveryOne = null) where T : UnityEngine.Object
        {
            Addressables.LoadAssetsAsync<T>(keyName, callBackOnEveryOne).Completed += callBack;
        }
        
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">具体对象</param>
        public static void UnLoadAsset<T>(T obj)
        {
            Addressables.Release(obj);
        }

        /// <summary>
        /// 卸载因为批量加载而产生的handle
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="handle"></param>
        public static void UnLoadAsset<TObject>(AsyncOperationHandle<TObject> handle)
        {
            Addressables.Release(handle);
        }

        /// <summary>
        /// 卸载资源实例
        /// </summary>
        public static bool UnLoadInStance(GameObject obj)
        {
            return Addressables.ReleaseInstance(obj);
        }
        
        #endregion
    }
}