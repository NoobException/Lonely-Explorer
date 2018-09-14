using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    bool usedArrows = false;
    bool usedSpace = false;
    bool firstTutorial = false;

    int tutorialIndex = 0;
    public string[] tutorialTexts;
    public Text TutorialText;

	// Update is called once per frame
	void Update ()
    {

        if (Input.GetAxisRaw("Horizontal") != 0)
            usedArrows = true;
        if (Input.GetKeyDown(KeyCode.Space))
            usedSpace = true;

        if(usedArrows && usedSpace) //Passed first tutorial
        {
            firstTutorial = true;
        }
	}
}
