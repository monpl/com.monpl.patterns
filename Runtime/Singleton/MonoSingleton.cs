using UnityEngine;

namespace Monpl.Patterns
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isDestroyed;
        private static bool _isApplicationQuitting;

        public static bool HasInstance => _instance != null;
        public static bool IsDestroyed => _isDestroyed;

        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            gameObject.transform.SetParent(SingletonUtil.Parent);
        }

        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                {
                    Debug.LogError($"Already singleton is Destroyed. :{typeof(T)} " +
                                   $"InstanceHave: {HasInstance}, Destroyed: {IsDestroyed}");
                    return null;
                }

                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                        CreateNewSingletonObject();
                }

                return _instance;
            }
        }

        private static void CreateNewSingletonObject()
        {
            var fullName = typeof(T).FullName;
            _instance = new GameObject(fullName).AddComponent<T>();
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
            _isDestroyed = true;
        }

        protected void OnApplicationQuit()
        {
            _instance = null;
            _isDestroyed = true;
            _isApplicationQuitting = true;
        }
    }
}