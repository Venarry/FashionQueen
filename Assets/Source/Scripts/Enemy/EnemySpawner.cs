using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Character[] _prefabs;

    private Transform[] _movePoints;
    private Transform _attackPoint;
    private Character _activeCharacter;
    private RateSmilesDataSource _rateSmilesDataSource;
    private readonly int _projectionDespawnDelay = 1;

    public void Init(RateSmilesDataSource rateSmilesDataSource, Transform[] points, Transform attackPoint)
    {
        _rateSmilesDataSource = rateSmilesDataSource;
        _movePoints = points;
        _attackPoint = attackPoint;
    }

    public IEnumerator SpawnWithProjection(Vector3 position, Quaternion rotation, float speed, Action<Character> callback)
    {
        int prefabsForShowCount = 4;
        int[] randomValues = GetRandomValues(prefabsForShowCount, 0, _prefabs.Length);

        for (int i = 0; i < randomValues.Length; i++)
        {
            if (_activeCharacter != null)
            {
                Destroy(_activeCharacter.gameObject);
            }

            Character prefab = _prefabs[randomValues[i]];
            _activeCharacter = Instantiate(prefab, position, rotation);

            yield return new WaitForSecondsRealtime(_projectionDespawnDelay);
        }

        _activeCharacter.Init(_movePoints, _attackPoint, null, speed, _rateSmilesDataSource);
        callback?.Invoke(_activeCharacter);
    }

    private int[] GetRandomValues(int count, int min, int max)
    {
        List<int> numbers = Enumerable.Range(min, max).ToList();
        Shuffle(numbers);
        
        List<int> randomValues = numbers.Take(count).ToList();

        return randomValues.ToArray();
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
