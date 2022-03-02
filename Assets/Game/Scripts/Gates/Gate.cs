using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateType gateType;
    [SerializeField] private TextMeshProUGUI gateText;
    [SerializeField] private TextMeshProUGUI gateTypeText;
    //True if gate is positive, if it is false gate is negative
    [SerializeField] private bool status;
    private bool isTriggered = false;
    //Amount of rows/columns to add to the canvas
    private int amount;
    // Start is called before the first frame update
    void Start()
    {
        amount = Random.Range(1, 6);
        amount = status ? amount : -amount;
        gateTypeText.SetText(gateType.ToString());
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetText()
    {
        switch (gateType)
        {
            case GateType.WIDTH:
                gateText.SetText((amount * StackManager.Instance.Width).ToString());
                break;
            case GateType.LENGHT:
                gateText.SetText((amount * StackManager.Instance.Length).ToString());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Canvas") && !isTriggered)
        {
            Debug.Log("Entered");
            StackManager.Instance.HandleGate(gateType, amount);
            isTriggered = true;
            
        }
    }
}
