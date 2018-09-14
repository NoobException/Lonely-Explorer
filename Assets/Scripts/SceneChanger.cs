using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public static SceneChanger instance;
    public Animator animator;

    private string sceneToLoad;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one Scene Changer!");
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeInTo(string scene)
    {
        sceneToLoad = scene;
        animator.SetTrigger("FadeOut");
       
    }
    public void FadeOut()
    {
        animator.CrossFade("PureFadeOut", 0f);
    }
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    
    public void OnAnimationFinished()
    {
        SceneManager.LoadScene(sceneToLoad);
        FadeIn();
    }
}
