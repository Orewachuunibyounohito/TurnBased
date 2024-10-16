using System;

namespace SingletonPractice.Singletons
{
    public class Singleton<T> where T : new()
    {
        public static T Instance{
            get{
                if(Instance == null){
                    Instance = new T();
                }
                return Instance;
            }
            set => Instance = value;
        }
    }
}