using UnityEngine;

namespace Henohenon.Scripts.LibUnity
{
    // https://naichilab.blogspot.com/2013/11/unitymanager.html
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        static T instance;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if(instance == null)
                    {
                        Debug.LogError( typeof(T) + " none");
                    }
                }

                return instance;
            }
        }
    }
}