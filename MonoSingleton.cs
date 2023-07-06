using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_Instance = null;
    public static T Instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if( m_Instance == null )
            {
                m_Instance = GameObject.FindObjectOfType<T>();
                // Object not found, we create a temporary one
                if( m_Instance == null )
                {
                    if (GameManager.ApplicationClosing)
                    { 
                        Debug.LogError("Creation of " + typeof(T).ToString()+" when application is closing...");
                    }

                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
                    m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
 
                    // Problem during the creation, this should not happen
                    if( m_Instance == null )
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
                }
                m_Instance.Init();
            }
            return m_Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        if( m_Instance == null )
        {
            m_Instance = (T)this;
            m_Instance.Init();
        }
		else if(m_Instance.gameObject!=this.gameObject)
		{
            Debug.Log("Destorying Dupe");
            Destroy(this.gameObject);
		}
    } 

    public virtual void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
 
    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init(){}
 
    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        m_Instance = null;
    }

    public static bool ApplicationClosing = false;
}
