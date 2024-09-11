using System;
using System.Collections.Generic;
using System.Reflection;
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
            StateMachine = new EnemyStateMachine<KnifeEnum>();

            // CreateState
            foreach (KnifeEnum stateEnum in Enum.GetValues(typeof(KnifeEnum)))
            {
                string typeName = $"MIN.Knife{stateEnum}State";
                Type t = Type.GetType(typeName);

                if (t == null)
                {
                    Debug.LogError($"Type not found using Type.GetType: {typeName}");
                    // Alternative method to find the type
                    t = Assembly.GetExecutingAssembly().GetType(typeName);

                    if (t == null)
                    {
                        Debug.LogError($"Type still not found using Assembly.GetExecutingAssembly().GetType: {typeName}");
                        continue;
                    }
                }

                try
                {
                    EnemyState<KnifeEnum> state = Activator.CreateInstance(t, this, StateMachine, stateEnum.ToString()) as EnemyState<KnifeEnum>;

                    if (state == null)
                    {
                        Debug.LogError($"Failed to create instance of {typeName}");
                        continue;
                    }

                    StateMachine.AddState(stateEnum, state);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error creating state: {ex.Message}");
                }
            }
        }

        private void Start()
        {
            StateMachine.Initialize(KnifeEnum.Idle, this);
        }

        private void Update()
        {
            if (StateMachine.CurrentState != null)
            {
                StateMachine.CurrentState.UpdateState();
            }
            else
            {
                Debug.LogWarning("CurrentState is null.");
            }
        }

        public override void AnimationEndTrigger()
        {
            StateMachine.CurrentState?.AnimationFinishTrigger();
        }

        public override void Attack()
        {
            StateMachine.ChangeState(KnifeEnum.Attack, true);
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
