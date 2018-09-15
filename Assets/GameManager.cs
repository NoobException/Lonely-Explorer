using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one Game Manager!");
            Destroy(gameObject);
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EndGame()
    {
        Cursor.visible = true;
        SceneChanger.instance.FadeInTo("FinalScreen");
    }
    public void Restart()
    {
        SceneChanger.instance.FadeOut();
    }
    
}
