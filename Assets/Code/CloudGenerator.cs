using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudGenerator: MonoBehaviour
{
    [SerializeField] private GameObject _cloudPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _cloudAmount;
    [SerializeField] private float _cloudsOffset = 100f;
    [SerializeField] private Transform _intialPoint;
    [SerializeField] private Transform _endPoint;

    private List<Transform> _clouds;
    private float _interpolation;
    private bool _playAnimation;

    private Action Callback = delegate {  };
    private float _duration;
    private bool _needCallback;

    public void GenerateCloudPull()
    {
        _clouds = new List<Transform>();
        for (var i = 0; i < _cloudAmount; i++)
        {
            var cloud = Instantiate(_cloudPrefab, _container);
            var localPosition = _intialPoint.localPosition;
            cloud.transform.localPosition = new Vector3(localPosition.x, Random.Range(localPosition.y - 5, localPosition.y + 5), localPosition.z);
            _clouds.Add(cloud.transform);
        }
    }

    public void StartAnimation(Action callback, float duration)
    {
        _duration = duration;
        Callback = callback;
        _needCallback = true;
    }
    
    private void Update()
    {
        _playAnimation = _duration > 0;
        if(_playAnimation)
        {
            _duration -= Time.deltaTime;
            for (var i =0; i < _clouds.Count; i++)
            {
                if (i == 0) MoveCloud(_clouds[i]);
                if(i > 0 && TimeToMove(_clouds[i], _clouds[i-1])) MoveCloud(_clouds[i]);
                
                if (_clouds[i].localPosition.x <= _endPoint.localPosition.x)
                {
                    _clouds[i].localPosition = _intialPoint.localPosition;
                }
            }
        }

        else
        {
            if (_needCallback)
            {
                Callback.Invoke();
                _needCallback = false;
            }
        }
    }

    private bool TimeToMove(Transform currentCloud, Transform previous)
    {
        return Mathf.Abs((currentCloud.localPosition - previous.localPosition).sqrMagnitude) > _cloudsOffset;
    }

    private void MoveCloud(Transform cloud)
    {
        cloud.localPosition += Vector3.left*(10f * Time.deltaTime);
    }
}