using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIN
{
    public enum KnifeEnum
    {
        Idle,
        Run,
        Attack,
        Dead
    }
    public class KnifeEnemy : Enemy
    {
        public EnemyStateMachine<KnifeEnum> StateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new();

            // CreateState
            foreach(KnifeEnum stateEnum in Enum.GetValues(typeof(KnifeEnum)))
            {
                string typeName = stateEnum.ToString();
                Type t = Type.GetType($"MIN.Knife{typeName}State");

                try
                {
                    EnemyState<KnifeEnum> state =
                        Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<KnifeEnum>;
                    StateMachine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Enemy Hammer : no State found [ {typeName} ] - {ex.Message}");
                }
            }
        }

        private void Start()
        {
            StateMachine.Initialize(KnifeEnum.Idle, this);
        }

        private void Update()
        {
            StateMachine.CurrentState.UpdateState();
        }

        public override void AnimationEndTrigger()
        {
            StateMachine.CurrentState.AnimationFinishTrigger();
        }

        public override void Attack()
        {
            
        }

        public override void SetDead()
        {
            StateMachine.ChangeState(KnifeEnum.Dead, true);
            isDead = true;
            CanStateChangeable = false;
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }
    }
}
