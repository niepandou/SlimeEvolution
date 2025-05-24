/*
* ┌──────────────────────────────────┐
* │  描    述: 简单单例                      
* │  类    名: SingletonMono.cs       
* │  创    建: By 4463fger                     
* └──────────────────────────────────┘
*/

namespace Frame
{
    public abstract class SingletonMono<T> where T : SingletonMono<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
        }
    }
}