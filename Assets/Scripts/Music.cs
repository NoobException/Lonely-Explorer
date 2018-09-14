using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    private static Music instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one Music Manager!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeInTo(string scene)
    {
        
    }

}
