using UnityEngine;

public class TutorialCompleteButtons : MonoBehaviour {

	public void LoadScene(string name)
    {
        SceneChanger.instance.FadeInTo(name);
    }
}
