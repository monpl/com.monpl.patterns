using UnityEngine;

namespace Monpl.Patterns
{
    public static class SingletonUtil
    {
        private static Transform _singletonParent;

        public static Transform Parent
        {
            get
            {
                if (_singletonParent != null)
                    return _singletonParent;
                
                _singletonParent = new GameObject("Singletons").transform;
                Object.DontDestroyOnLoad(_singletonParent);

                return _singletonParent;
            }
        }
    }
}