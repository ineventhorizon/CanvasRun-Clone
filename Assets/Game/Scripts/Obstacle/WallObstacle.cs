using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WallObstacle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wallText;
    private ObstacleType type = ObstacleType.WALL;
    private bool isTriggered = false;
    private int amount;
    private int value;
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
        amount = Random.Range(10, 50);
    }
    private void SetText()
    {
        value = Mathf.FloorToInt((amount * StackManager.Instance.StackCount) / 100);
        wallText.SetText(amount.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        var collidePoint = this.transform.position - other.attachedRigidbody.transform.position;
        if (other.CompareTag("Sphere") && !isTriggered && !(collidePoint.z <= 0))
        {
            isTriggered = true;
            StackManager.Instance.HandleObstacle(type, null, amount);
            Debug.Log("Hit wall");
            HandleHit();

        }
    }
    private void HandleHit()
    {
        transform.DOMove(Vector3.right*12, 0.5f)
            .SetRelative()
            .SetEase(Ease.OutElastic)
            .OnComplete(() => Destroy(gameObject));
    }
}
