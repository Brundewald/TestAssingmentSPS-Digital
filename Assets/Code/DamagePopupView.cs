using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamagePopupView: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _messageField;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _animationTime;


    public void Setup(string message, Vector3 position)
    {
        _messageField.text = message;
        transform.position = position;
    }

    public void Animate()
    {
        var sequence = DOTween.Sequence();
        var targetDistance = transform.localPosition.y + _maxDistance;
        sequence.Append(transform.DOLocalMoveY(targetDistance, _animationTime));
        sequence.OnComplete(DestroyPopup);
    }

    private void DestroyPopup()
    {
        Destroy(gameObject);
    }
}