using UnityEngine;

public class DamageMessenger: MonoBehaviour
{
    [SerializeField] private DamagePopupView _damagePopup;

    private IHealth _damagedObject;

    public void ShowMessage(int damage, Vector3 position)
    {
        var damagePopup = Instantiate(_damagePopup, position, Quaternion.identity);
        damagePopup.Setup(damage.ToString(), position);
        damagePopup.Animate();
    }
}