/////////////////////////////////////////////////////////////////////////////
//
//  Script   : CollisionListener2D.cs
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2021
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Physical
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionListener2D : MonoBehaviour, ICollisionListener2D
    {
        public Rigidbody2D Rigidbody2D { get; set; }

        public ColliderCallback<Collision2D> onCollisionEnter2D = new ColliderCallback<Collision2D>();
        public ColliderCallback<Collision2D> onCollisionStay2D = new ColliderCallback<Collision2D>();
        public ColliderCallback<Collision2D> onCollisionExit2D = new ColliderCallback<Collision2D>();

        public Collision2DEvent CollisionEnter2DEvent;
        public Collision2DEvent CollisionStay2DEvent;
        public Collision2DEvent CollisionExit2DEvent;

        public void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            onCollisionEnter2D.Trigger(other);
            CollisionEnter2DEvent?.Invoke(other);
        }

        public virtual void OnCollisionStay2D(Collision2D other)
        {
            onCollisionStay2D.Trigger(other);
            CollisionStay2DEvent?.Invoke(other);
        }

        public virtual void OnCollisionExit2D(Collision2D other)
        {
            onCollisionExit2D.Trigger(other);
            CollisionExit2DEvent?.Invoke(other);
        }

        public virtual void Clear()
        {
            onCollisionEnter2D.Clear();
            onCollisionStay2D.Clear();
            onCollisionExit2D.Clear();

            CollisionEnter2DEvent?.RemoveAllListeners();
            CollisionStay2DEvent?.RemoveAllListeners();
            CollisionExit2DEvent?.RemoveAllListeners();
        }
    }
}