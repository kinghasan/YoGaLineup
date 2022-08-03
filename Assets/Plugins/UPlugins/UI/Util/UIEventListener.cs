/////////////////////////////////////////////////////////////////////////////
//
//  Script   : UIEventListener.cs
//  Info     : UI消息接收器 - 用于统一接收Unity UGUI事件
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Aya.UI
{
	[DisallowMultipleComponent]
	[AddComponentMenu("AUI/UI Event Listener")]
    public class UIEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler,
		IPointerExitHandler,
		IPointerUpHandler, ISelectHandler, IUpdateSelectedHandler, IDeselectHandler, IDropHandler, IMoveHandler,
		IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IScrollHandler
	{

		public delegate void UIPointerEventDelegate(GameObject go, PointerEventData data);
		public delegate void UIBaseEventDelegate(GameObject go, BaseEventData data);
		public delegate void UIAxisEventDelegate(GameObject go, AxisEventData data);

		public UIPointerEventDelegate onClick = delegate { };
	    public UIPointerEventDelegate onDoubleClick = delegate { };
        public UIPointerEventDelegate onDown = delegate { };
		public UIPointerEventDelegate onEnter = delegate { };
		public UIPointerEventDelegate onExit = delegate { };
		public UIPointerEventDelegate onUp = delegate { };
		public UIBaseEventDelegate onSelect = delegate { };
		public UIBaseEventDelegate onUpdateSelect = delegate { };
		public UIBaseEventDelegate onDeSelect = delegate { };
		public UIPointerEventDelegate onDrag = delegate { };
		public UIPointerEventDelegate onInitializePotentialDrag = delegate { };
		public UIPointerEventDelegate onBeginDrag = delegate { };
		public UIPointerEventDelegate onEndDrag = delegate { };
		public UIPointerEventDelegate onDrop = delegate { };
		public UIPointerEventDelegate onScroll = delegate { };
		public UIAxisEventDelegate onMove = delegate { };

	    #region Layout
	    /// <summary>
	    /// UI变换
	    /// </summary>
	    public RectTransform Rect
	    {
	        get
	        {
	            if (_rect == null)
	            {
	                _rect = GetComponent<RectTransform>();
	            }
	            return _rect;
	        }
	    }
	    private RectTransform _rect;
		#endregion

		/// <summary>
		/// UIEventListener 缓存字典，减少 GetComponent 调用
		/// </summary>
		protected static Dictionary<GameObject, UIEventListener> UIEventListenerCacheDic = new Dictionary<GameObject, UIEventListener>();


		protected ScrollRect _scrollRect;

	    private float _lastClickTime = -1f;
	    private float _doubleClickiInterval = 0.5f;

        /// <summary>
        /// 用于对任意UI对象快速获取UI事件监听器
        /// </summary>
        /// <param name="go">对象</param>
        /// <returns>结果</returns>
        public static UIEventListener Get(GameObject go)
		{
		    if (UIEventListenerCacheDic.TryGetValue(go, out var ret))
		    {
		        return ret;
		    }
		    ret = go.GetComponent<UIEventListener>();
		    if (ret == null)
		    {
		        ret = go.AddComponent<UIEventListener>();
		    }
		    UIEventListenerCacheDic.Add(go, ret);
		    return ret;
		}

		/// <summary>
		/// 设置需要透传事件的滑动区域
		/// </summary>
		/// <param name="scrollRect">滑动区域</param>
		public virtual void SetScrollRect(ScrollRect scrollRect)
		{
			_scrollRect = scrollRect;
		}

        /// <summary>
        /// 事件向下透传
        /// 当执行过程中产生新UI，会导致unity卡死
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="data">消息</param>
        /// <param name="function">类型</param>
        /// <param name="onlyOne">只传一层</param>
        /// <param name="callback">回调</param>
        public void PassEvent<T>(PointerEventData data, ExecuteEvents.EventFunction<T> function, bool onlyOne = true, Action<object> callback = null) where T : IEventSystemHandler 
		{
		    if (data == null) return;
            var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(data, results);
			var current = data.pointerCurrentRaycast.gameObject;
			for (var i = 0; i < results.Count; i++)
			{
				var target = results[i].gameObject;
				if (current == target) continue;
				// RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
				ExecuteEvents.Execute(target, data, function);
			    callback?.Invoke(target);
			    if (onlyOne)
				{
					break;
				}
			}
		}

	    /// <summary>
	    /// 事件向下透传到指定组件
	    /// 当执行过程中产生新UI，会导致unity卡死
	    /// </summary>
	    /// <typeparam name="T">消息类型</typeparam>
	    /// <typeparam name="TComponent">限定对象组件类型</typeparam>
	    /// <param name="data">消息</param>
	    /// <param name="function">类型</param>
	    /// <param name="onlyOne">只传一层</param>
	    /// <param name="callback">回调</param>
	    public void PassEvent<T, TComponent>(PointerEventData data, ExecuteEvents.EventFunction<T> function,
	        bool onlyOne = true, Action<TComponent> callback = null) where T : IEventSystemHandler
	    {
	        if (data == null) return;
            var results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(data, results);
	        var current = data.pointerCurrentRaycast.gameObject;
	        for (var i = 0; i < results.Count; i++)
	        {
	            var target = results[i].gameObject;
	            var com = target.GetComponent<TComponent>();
	            if (com == null) continue;
	            if (current == target) continue;
	            // RaycastAll后ugui会自己排序，如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
	            ExecuteEvents.Execute(target, data, function);
	            callback?.Invoke(com);
	            if (onlyOne)
	            {
	                break;
	            }
	        }
	    }

        public void OnPointerClick(PointerEventData eventData)
		{
		    onClick(gameObject, eventData);

            var clickTime = Time.timeSinceLevelLoad;
		    if (clickTime - _lastClickTime < _doubleClickiInterval)
		    {
		        onDoubleClick(gameObject, eventData);
		        _lastClickTime = -1;
                return;
		    }
		    _lastClickTime = clickTime;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			onDown(gameObject, eventData);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			onEnter(gameObject, eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			onExit(gameObject, eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			onUp(gameObject, eventData);
		}

		public void OnSelect(BaseEventData eventData)
		{
			onSelect(gameObject, eventData);
		}

		public void OnUpdateSelected(BaseEventData eventData)
		{
			onUpdateSelect(gameObject, eventData);
		}

		public void OnDeselect(BaseEventData eventData)
		{
			onDeSelect(gameObject, eventData);
		}

		public void OnMove(AxisEventData eventData)
		{
			onMove(gameObject, eventData);
		}

		public void OnDrop(PointerEventData eventData)
		{
			onDrop(gameObject, eventData);
		}

		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			onInitializePotentialDrag(gameObject, eventData);
			if (_scrollRect == null) return;
			_scrollRect.OnInitializePotentialDrag(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			onDrag(gameObject, eventData);
			if (_scrollRect == null) return;
			_scrollRect.OnDrag(eventData);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			onBeginDrag(gameObject, eventData);
			if (_scrollRect == null) return;
			_scrollRect.OnBeginDrag(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			onEndDrag(gameObject, eventData);
			if (_scrollRect == null) return;
			_scrollRect.OnEndDrag(eventData);
		}

		public void OnScroll(PointerEventData eventData)
		{
			onScroll(gameObject, eventData);
			if (_scrollRect == null) return;
			_scrollRect.OnScroll(eventData);
		}
	}
}
