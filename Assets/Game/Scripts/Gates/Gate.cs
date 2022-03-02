using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    [SerializeField] private GateType gateType;
    [SerializeField] private TextMeshProUGUI gateText;
    //True if gate is positive, if it is false gate is negative
    [SerializeField] private bool status;

    //Amount of rows/columns to add to the canvas
    private int amount;
    // Start is called before the first frame update
    void Start()
    {
        amount = Random.Range(1, 6);
        amount = status ? amount : -amount;
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
                //gateText.SetText(amount*StackManager.Instance.)
                break;
            case GateType.LENGHT:
                break;
            default:
                break;
        }
    }
}
