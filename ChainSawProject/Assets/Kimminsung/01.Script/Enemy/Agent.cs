using System.Collections;
using System;
using UnityEngine;

 public abstract class Agent : MonoBehaviour
 {
        #region component list section
        public Animator AnimatorCompo { get; protected set; }
        //public IMovement MovementCompo { get; protected set; }
        //public AgentVFX VFXCompo { get; protected set; }
        //public DamageCaster DamageCasterCompo { get; protected set; }
        //public Health HealthCompo { get; protected set; }
        #endregion

        //[field: SerializeField] public AgentStat Stat { get; protected set; }

        public bool CanStateChangeable { get; protected set; } = true;
        public bool isDead;
            
        protected virtual void Awake()
        {
            
            Transform visualTrm = transform.Find("Visual");
            AnimatorCompo = visualTrm.GetComponent<Animator>();
            //MovementCompo = GetComponent<IMovement>();
            //MovementCompo.Initialize(this);

            //VFXCompo = transform.Find("AgentVFX").GetComponent<AgentVFX>();

            //Transform damageTrm = transform.Find("DamageCaster");
            //if (damageTrm != null)
            //{
            //    DamageCasterCompo = damageTrm.GetComponent<DamageCaster>();
            //    DamageCasterCompo.InitCaster(this);
            //}

            //Stat = Instantiate(Stat); //자기자신 복제본으로 만들고 들어간다.
            //Stat.SetOwner(this);

            //HealthCompo = GetComponent<Health>();
            //HealthCompo.Initialize(this);
        }

        public Coroutine StartDelayCallback(float time, Action Callback)
        {
            return StartCoroutine(DelayCoroutine(time, Callback));
        }

        protected IEnumerator DelayCoroutine(float time, Action Callback)
        {
            yield return new WaitForSeconds(time);
            Callback?.Invoke();
        }

        public virtual void Attack()
        {
            //여기서는 아무것도 안한다.
        }

        public abstract void SetDead();
    }

