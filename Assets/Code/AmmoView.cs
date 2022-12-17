using System;
using UnityEngine;

namespace Code
{
    public class AmmoView: MonoBehaviour
    {
        private EnemyView _enemy;
        private Action Callback = delegate {  };
        private float intrapolateAmount;
        
        
        public void Setup(EnemyView enemy, Action callback)
        {
            _enemy = enemy;
            Callback = callback;
        }

        private void Update()
        {
            if(_enemy.gameObject.activeInHierarchy)
            {
                var startPoint = transform.position;
                var endPoint = _enemy.transform.position;
                intrapolateAmount = (intrapolateAmount + Time.deltaTime) % 1f;
                transform.position = Vector3.Lerp(startPoint, endPoint, intrapolateAmount);
            }
            else Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            var enemy = collider.TryGetComponent(out EnemyView enemyView);
            if (!enemy) return;
            if (enemyView.Equals(_enemy))
            {
                Callback.Invoke();
                Destroy(gameObject);
            }
        }
    }
}