using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour, new()
{
    private static T instance = null;

    public virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            var go = new GameObject(typeof(T).ToString());
            GameObject.DontDestroyOnLoad(go);
            instance = go.AddComponent<T>();
            return instance;
        }
    }
    public static void Close()
    {
        var cName = typeof(T).ToString();
        var cObject = GameObject.Find(cName);
        if (cObject != null)
        {
            GameObject.Destroy(cObject);
        }

        instance = null;
    }
}
