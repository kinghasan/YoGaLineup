/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ColliderListener2D.cs
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Physical
{
    [RequireComponent(typeof(Collider2D))]
    public class ColliderListener2D : MonoBehaviour, IColliderListener2D
    {
        public Collider2D Collider2D { get; set; }

        public ColliderCallback<Collider2D> onTriggerEnter2D = new ColliderCallback<Collider2D>();
        public ColliderCallback<Collider2D> onTriggerStay2D = new ColliderCallback<Collider2D>();
        public ColliderCallback<Collider2D> onTriggerExit2D = new ColliderCallback<Collider2D>();

        public Collider2DEvent TriggerEnter2DEvent;
        public Collider2DEvent TriggerStay2DEvent;
        public Collider2DEvent TriggerExit2DEvent;

        public void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter2D.Trigger(other);
            TriggerEnter2DEvent?.Invoke(other);
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
            onTriggerStay2D.Trigger(other);
            TriggerStay2DEvent?.Invoke(other);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            onTriggerExit2D.Trigger(other);
            TriggerExit2DEvent?.Invoke(other);
        }
        public virtual void Clear()
        {
            onTriggerEnter2D.Clear();
            onTriggerStay2D.Clear();
            onTriggerExit2D.Clear();

            TriggerEnter2DEvent?.RemoveAllListeners();
            TriggerStay2DEvent?.RemoveAllListeners();
            TriggerExit2DEvent?.RemoveAllListeners();
        }
    }
}