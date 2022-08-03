using System.Collections;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;

public class PlayerControlPath : PlayerControl
{
    private bool _isMouseDown;
    private Vector3 _startMousePos;
    private float _startX;
    /// <summary>
    /// 转向力度
    /// </summary>
    private float _turnPower;
    /// <summary>
    /// 基础比例
    /// </summary>
    private float _scale;

    public override void InitComponent()
    {
        _yogaList.Clear();
        AnimationScale = 0.5f;
    }

    public override void UpdateImpl(float deltaTime)
    {
        if (State.EnableRun)
        {
            var nextPathPos = Move.MovePath(Move.MoveSpeed * State.SpeedMultiply * deltaTime);
            var nextPos = nextPathPos;

            if (nextPos != transform.position)
            {
                if (!State.KeepDirection)
                {
                    var rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - transform.position), deltaTime * Move.RotateSpeed).eulerAngles;
                    if (Move.KeepUp)
                    {
                        rotation.x = 0f;
                    }

                    transform.eulerAngles = rotation;
                }

                transform.position = nextPos;
                RunGirlList();
            }
        }

        var canInput = Game.GamePhase == GamePhaseType.Gaming && State.EnableInput;
        var turnX = Self.Render.RenderTrans.localPosition.x;
        if (canInput)
        {
            if (Input.GetMouseButtonDown(0) || (!_isMouseDown && Input.GetMouseButton(0)))
            {
                _isMouseDown = true;
                _startMousePos = Input.mousePosition;
                _startX = Self.Render.RenderTrans.localPosition.x;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isMouseDown = false;
            }

            if (_isMouseDown)
            {
                var offset = Input.mousePosition - _startMousePos;
                turnX = _startX + offset.x * Move.TurnSpeed / 200f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            turnX = Mathf.Clamp(turnX, State.TurnRange.x, State.TurnRange.y);
            //turnX = Mathf.Lerp(AnimationScale, turnX, Move.TurnLerpSpeed * deltaTime);
            var Fx = turnX;
            float range = State.TurnRange.y - State.TurnRange.x;
            Fx -= State.TurnRange.x;
            Fx /= 10f;
            Animator.Play("Idle", 0, Fx);
            AnimationScale = Fx;
            //_turnPower = Mathf.Abs(Self.Render.RenderTrans.localPosition.x - turnX);
            //Self.Render.RenderTrans.SetLocalPositionX(turnX);
        }
        //UpdateYoga();
    }

    public void RunGirlList()
    {
        foreach (var girl in Game.YogaGirlList)
        {
            girl.Run();
        }
    }

    /// <summary>
    /// 初始化瑜伽动作
    /// </summary>
    public override void InitYoga()
    {
        foreach (var parameter in Animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
                _yogaList.Add(parameter.name);
        }
        float range = State.TurnRange.y - State.TurnRange.x;
        _scale = range / _yogaList.Count;
        int index = (int)Mathf.Floor(_yogaList.Count / 2);
        _targetIndex = index;
        //StartCoroutine(YogaControl());
    }

    public IEnumerator YogaControl()
    {
        //var lastX = 0f;
        while (true)
        {
            if (State.EnableRun)
            {
                if (_targetIndex < _yogaIndex)
                    _yogaIndex--;
                else if (_targetIndex > _yogaIndex)
                    _yogaIndex++;
                string yogaStr = _yogaList[_targetIndex];
                //var nowX = _scale * _yogaIndex;
                //var length = Mathf.Abs(_scale * _targetIndex - lastX);
                //Animator.speed = Mathf.Lerp(0f, 10f, length / Move.TurnLerpSpeed) + 0.3f;
                //if (!string.IsNullOrEmpty(CurrentClip))
                    //Animator.ResetTrigger(CurrentClip);
                //Animator.SetTrigger(yogaStr);
                CurrentClip = yogaStr;
                //Play(yogaStr);
                //Animator.Play(yogaStr);
                //Animator.SetTrigger(yogaStr);
                //lastX = nowX;
                yield return null;
                //yield return new WaitForSeconds(Animator.GetCurrentAnimatorStateInfo(0).length);
            }
            yield return null;
        }
    }

    /// <summary>
    /// 计算瑜伽动作
    /// </summary>
    public void UpdateYoga()
    {
        float yogaF = Mathf.Clamp(Self.Render.RenderTrans.GetLocalPositionX(), State.TurnRange.x, State.TurnRange.y);
        yogaF -= State.TurnRange.x;
        Animator.Play("Idle", 0, yogaF / 10f);
        int index = (int)Mathf.Floor(yogaF / _scale);
        if (index >= _yogaList.Count)
            index = _yogaList.Count - 1;
        if (index < 0)
            index = 0;
        _targetIndex = index;
    }
}
