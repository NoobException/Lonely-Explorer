using UnityEngine;

//Simple script assigned both to the light and the camera
//that makes them follow player
public class FollowPlayer : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.3F;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    //Late Update in order to be sure that player has already updated
    private void LateUpdate()
    {
        if (!PlayerController.gameIsRunning)
            return;

        Vector3 targetPosition = target.position + offset;
        Vector3 smooth = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime * smoothSpeed);
        transform.position = smooth;
    }

    public void Refresh()
    {
        LateUpdate();
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
