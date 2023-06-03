using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private List<PlatformDestination> _platformDestinations;
    private int _currentDestinationIndex = 0;
    private float _timeWaited = 0.0f;

    void Update()
    {
        // Move towards current destination at specified speed
        float step = Time.deltaTime * _platformDestinations[_currentDestinationIndex].MovementSpeed;
        transform.position = Vector2.MoveTowards(transform.position, _platformDestinations[_currentDestinationIndex].DestinationObject.transform.position, step);

        // If destination is reached start counting up towards delay
        if (Vector2.Distance(transform.position, _platformDestinations[_currentDestinationIndex].DestinationObject.transform.position) < 0.01f)
        {
            _timeWaited += Time.deltaTime;
        }

        // When delay is reached, point to next destination
        if (Vector2.Distance(transform.position, _platformDestinations[_currentDestinationIndex].DestinationObject.transform.position) < 0.01f && _timeWaited > _platformDestinations[_currentDestinationIndex].DelayOnArrival)
        {
            _currentDestinationIndex++;
            if (_currentDestinationIndex > _platformDestinations.Count - 1) _currentDestinationIndex = 0;
            _timeWaited = 0.0f;
        }
    }
}
