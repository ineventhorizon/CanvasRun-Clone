using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateCouple gateCouple;
    //Can change gate's material based on type
    [SerializeField] private GateType gateType;
    [SerializeField] private TextMeshProUGUI gateText;
    [SerializeField] private TextMeshProUGUI gateTypeText;
    //True if gate is positive, if it is false gate is negative
    [SerializeField] private bool status;
    private bool isTriggered
    {
        get
        {
            return gateCouple.IsTriggered;
        }
        set
        {
            gateCouple.IsTriggered = value;
        }
    }

    //Amount of rows/columns to add to the canvas
    private int amount;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Observer.StackChanged += SetText;
    }
    private void OnDisable()
    {
        Observer.StackChanged -= SetText;
    }
    void Start()
    {
        amount = Random.Range(1, 6);
        amount = status ? amount : -amount;
        gateTypeText.SetText(gateType.ToString());
        SetText();
    }
    private void SetText()
    {
        switch (gateType)
        {
            case GateType.WIDTH:
                gateText.SetText((amount * StackManager.Instance.Length).ToString());
                break;
            case GateType.LENGTH:
                gateText.SetText((amount * StackManager.Instance.Width).ToString());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //var dist = (other.attachedRigidbody.transform.position - other.transform.position);
        if (other.CompareTag("Sphere") && !isTriggered)
        {
            Debug.Log("Entered");
            StackManager.Instance.HandleGate(gateType, amount);
            UIManager.Instance.InGameScreen.SetScore(amount * 10);
            isTriggered = true;         
        }
    }
}
