using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // �̱��� �ν��Ͻ��� ������ ���� ����
    private static T instance;

    // �ν��Ͻ��� ������ ������Ƽ
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� ������ ã�ų� ����
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // Awake�� �ν��Ͻ��� �ߺ����� �ʵ��� ����
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
