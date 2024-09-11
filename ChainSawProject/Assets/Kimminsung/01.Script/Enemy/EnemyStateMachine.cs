using System;
using System.Collections.Generic;
using UnityEngine;


    public class EnemyStateMachine<T> where T : Enum
    {
        public EnemyState<T> CurrentState { get; private set; }
        public Dictionary<T, EnemyState<T>> StateDictionary
            = new Dictionary<T, EnemyState<T>>();
        private Enemy _enemyBase;

        public void Initialize(T startState, Enemy enemy)
        {
            _enemyBase = enemy;

            if (StateDictionary.TryGetValue(startState, out var initialState))
            {
                CurrentState = initialState;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError($"State '{startState}' not found in StateDictionary.");
            }
        }

        public void ChangeState(T newState, bool forceMode = false)
        {
            if (_enemyBase.CanStateChangeable == false && forceMode == false)
                return;

            if (_enemyBase.isDead) return;

            if (StateDictionary.TryGetValue(newState, out var newStateInstance))
            {
                CurrentState?.Exit();
                CurrentState = newStateInstance;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError($"State '{newState}' not found in StateDictionary.");
            }
        }

        public void AddState(T stateEnum, EnemyState<T> state)
        {
            if (!StateDictionary.ContainsKey(stateEnum))
            {
                StateDictionary.Add(stateEnum, state);
            }
            else
            {
                Debug.LogWarning($"State '{stateEnum}' already exists in StateDictionary.");
            }
        }
    }


