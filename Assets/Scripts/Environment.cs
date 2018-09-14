using UnityEngine;

public class Environment : MonoBehaviour {

    public GameObject Player;
    public GameObject firstDoor;

    public Transform firstDoorOpenLocation;
    private bool openedFirstDoor = false;
    [Header("Teleport 1")]
    public Transform firstTeleportPoint;
    public Transform firstTeleportTarget;

    [Header("Teleport 2")]
    public Transform secondTeleportPoint;
    public Transform secondTeleportTarget;
    public Transform secondTeleportTarget2;

    [Header("Teleport 3")]
    public Transform thirdTeleportPoint;
    public Transform thirdTeleportTarget;

    [Header("Switch")]
    public Transform Switch1;
    public GameObject SwitchGround;
    private bool switched1 = false;

    public Transform Switch2;
    public GameObject SwitchGround2;
    private bool switched2 = false;

    public Transform Switch3;
    public GameObject SwitchGround3;
    public Transform Switch3Target;
    private bool switched3 = false;

    public Transform Switch4;
    public GameObject SwitchGround4;
    private bool switched4 = false;

    public Transform Switch5;
    public Transform Switch5Target;
    public GameObject SwitchGround5;

    [Header("Checkpoints")]
    public Transform checkPoint1;
    public Transform checkPoint2;
    public Transform checkPoint3;

    private void Update()
    {
        if (!PlayerController.gameIsRunning)
            return;

        //First door
        if(Vector3.Distance(firstDoorOpenLocation.position, Player.transform.position) < 0.5f)
        {
            OpenFirstDoor();
        }

        //Teleports
        #region Teleports
        if (Vector3.Distance(firstTeleportPoint.position, Player.transform.position) < 0.2f)
        {
            FirstTeleport();
        }
        if (Vector3.Distance(secondTeleportPoint.position, Player.transform.position) < 0.6f)
        {
           SecondTeleport();
        }
        if (Vector3.Distance(thirdTeleportPoint.position, Player.transform.position) < 0.6f)
        {
           ThirdTeleport();
        }
        #endregion

        //Switches
        #region Switches
        if (Vector3.Distance(Switch1.position, Player.transform.position) < 1f)
        {
            SwitchOne();
        }
        if (Vector3.Distance(Switch2.position, Player.transform.position) < 1f)
        {
            SwitchTwo();
        }
        if (Vector3.Distance(Switch3.position, Player.transform.position) < .2f)
        {
            SwitchThree();
        }
        if (Vector3.Distance(Switch4.position, Player.transform.position) < 1f)
        {
            SwitchFour();
        }
        if (Mathf.Abs(Switch5.position.x - Player.transform.position.x) <.2f && Player.transform.position.y > Switch5.position.y)
        {
            SwitchFive();
        }
        #endregion

        //Checkpoints
        #region Checkpoints
        if (Vector3.Distance(checkPoint1.position, Player.transform.position) < 1f)
        {
            Player.GetComponent<PlayerController>().SetCheckpoint(checkPoint1);
        }
        if (Vector3.Distance(checkPoint2.position, Player.transform.position) < 1f)
        {
            Player.GetComponent<PlayerController>().SetCheckpoint(checkPoint2);
        }
        if (Vector3.Distance(checkPoint3.position, Player.transform.position) < 1f)
        {
            Player.GetComponent<PlayerController>().SetCheckpoint(checkPoint3);
        }
        #endregion

    }

    void SwitchOne()
    {
        if (switched1)
            return;

        switched1 = true;
        secondTeleportTarget = secondTeleportTarget2;
        SwitchGround.SetActive(true);
    }
    void SwitchTwo()
    {
        if (switched2)
            return;
        switched2 = true;
        firstDoor.SetActive(true);
        SwitchGround.SetActive(false);
        SwitchGround2.SetActive(false);
    }

    void SwitchThree()
    {
        Player.transform.position = Switch3Target.position + (Player.transform.position - Switch3.position);
        if (switched3)
            return;
        switched3 = true;
        SwitchGround3.SetActive(true);
    }
    void SwitchFour()
    {
        if (switched4)
            return;
        switched4 = true;
        SwitchGround4.SetActive(true);
    }
    void SwitchFive()
    {
        Player.transform.position = Switch5Target.position + (Player.transform.position - Switch5.position);
        SwitchGround5.SetActive(false);
    }

    void FirstTeleport()
    {
        Player.transform.position = firstTeleportTarget.position + (Player.transform.position - firstTeleportPoint.position);

    }
    void SecondTeleport()
    {
        Player.transform.position = secondTeleportTarget.position + (Player.transform.position - secondTeleportPoint.position);
    }
    void ThirdTeleport()
    {
        Player.transform.position = thirdTeleportTarget.position + (Player.transform.position - thirdTeleportPoint.position);
    }

    void OpenFirstDoor()
    {
        if (openedFirstDoor)
            return;

        openedFirstDoor = true;
        firstDoor.SetActive(false);
    }
}
