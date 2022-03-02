using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMovement : MonoBehaviour
{
    //[SerializeField] private CanvasStack stack;
    [SerializeField] private Transform leftLimit, rightLimit, sideMovementRoot;
    private float oldLeftLimitX, oldRightLimitX;
    private int stackWidth => StackManager.Instance.Width;
    private int stackLength => StackManager.Instance.Length;
    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;
    private float forwardSpeed => SettingsManager.CanvasSettings.forwardSpeed;
    private float sideMovementSensivity => SettingsManager.CanvasSettings.sideMovementSensivity;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Observer.HandleCanvasLimits += HandleCanvasLimits;
    }
    private void OnDisable()
    {
        
    }
    void Start()
    {
        oldLeftLimitX = leftLimitX;
        oldRightLimitX = rightLimitX;
    }

    // Update is called once per frame
    void Update()
    {
        HandleForwardMovement();
        HandleSideMovement();
    }

    private void HandleCanvasLimits()
    {
        var newLimitLeft = leftLimit.transform.localPosition;
        var newLimitRight = rightLimit.transform.localPosition;

        float newLimit = stackWidth % 2 == 0 ? ((stackWidth - 1) / 2) : (stackWidth / 2);
        newLimit *= SettingsManager.CanvasSettings.gap;

        newLimitLeft.x = -Mathf.Abs(newLimit + oldLeftLimitX);
        newLimitRight.x = Mathf.Abs(newLimit - oldRightLimitX);

        oldLeftLimitX = newLimitLeft.x <= -3.5f ? -3.5f : newLimitLeft.x;
        oldRightLimitX = newLimitRight.x >= 3.5f ? 3.5f : newLimitRight.x;

        leftLimit.transform.localPosition = newLimitLeft;
        rightLimit.transform.localPosition = newLimitRight;

        //sideMovementRoot.transform.localPosition = new Vector3(stack.Width / 2, 0, 0);

    }

    private void HandleForwardMovement()
    {
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }
    //Need to change limits with respect to the width of the stack
    //Todo
    private void HandleSideMovement()
    {
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime * 20f);

        //Rotation of side movement root
        var moveDirection = Vector3.forward + InputManager.Instance.RawMouseInput.x * Vector3.right;
        var targetRotation = pos.x == leftLimitX || pos.x == rightLimitX ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.deltaTime * 5f);
    }

}
