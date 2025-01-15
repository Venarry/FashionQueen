using System;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _moveSpeed = 3f;

    public event Action Reached;

    public async void Go()
    {
        for (int i = 0; i < _movePoints.Length; i++)
        {
            try
            {
                await GoToPoint(_movePoints[i]);

                Reached?.Invoke();
            }
            catch { }
        }
    }

    private async Task GoToPoint(Transform point)
    {
        float minMagnitude = 0.1f;

        while (Vector3.Distance(transform.position, point.position) > minMagnitude)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _moveSpeed * Time.deltaTime);

            await Task.Yield();
        }
    }
}
