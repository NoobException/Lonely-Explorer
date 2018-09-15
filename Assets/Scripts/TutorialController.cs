using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public string TutorialCompletedScene;

    bool usedArrows = false;
    bool usedSpace = false;
    bool firstTutorial = false;

    public GameObject collectible;
    public Transform start;
    public  Vector3 distance;

    int tutorialIndex = 0;
    [TextArea(3,5)]
    public string[] tutorialTexts;
    public Text TutorialText;

    bool secondTutorial = false;
    GameObject[] objects;

    bool thirdTutorial = false;
    public GameObject Switch1;
    public GameObject Switch2;
    public GameObject Switch3;

    public GameObject Exit;

    bool fourthTutorial = false;
    bool fifthTutorial = false;

    PlayerController controller;

    private void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        TutorialText.text = tutorialTexts[tutorialIndex];
        if (Input.GetAxisRaw("Horizontal") != 0)
            usedArrows = true;
        if (Input.GetKeyDown(KeyCode.Space))
            usedSpace = true;

        if (firstTutorial == false && usedArrows && usedSpace) //Passed first tutorial
        {
            firstTutorial = true;
            tutorialIndex++;
            InitSecondTutorial();
        }
        if(firstTutorial == true && secondTutorial == false && controller.collected == 3) //Passed the second Tutorial
        {
            secondTutorial = true;
            tutorialIndex++;
            InitThirdTutorial();
        }
        if(secondTutorial == true && thirdTutorial == false 
            && Switch2.GetComponent<Switch>().switched)
        {
            thirdTutorial = true;
            tutorialIndex++;
        }
        if(thirdTutorial == true && fourthTutorial == false && transform.position.y < 0)
        {
            fourthTutorial = true;
            tutorialIndex++;
            InitFifthTutorial();
        }
        if(fourthTutorial == true && fifthTutorial == false && Vector3.Distance(Exit.transform.position, transform.position) < 0.5)
        {
            Cursor.visible = true;
            SceneChanger.instance.FadeInTo(TutorialCompletedScene);
        }
        
       
	}

    void InitFifthTutorial()
    {
        Switch3.SetActive(true);
    }
    void InitThirdTutorial()
    {
        Switch1.SetActive(true);
    }
    void InitSecondTutorial()
    {
        controller.collected = 0;
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(collectible, start.position + distance * i, Quaternion.identity);
            obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(-90, 90));
        }
    }
}
