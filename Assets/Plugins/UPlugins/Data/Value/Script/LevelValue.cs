/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LevelValue.cs
//  Info     : 等级数据，可以根据指定公式设定每一级的经验值，然后计算出各种相关信息
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Data.Json;
using Exp = Aya.Maths.ExpressionParser;

namespace Aya.Data
{
	[Serializable]
	public class LevelValue
	{
		/// <summary>
		/// 等级和经验值
		/// </summary>
		[JsonIgnore]
		private readonly KeyValueSet<int, int> _levelExp = new KeyValueSet<int, int>();
		/// <summary>
		/// 等级和累积经验值
		/// </summary>
		[JsonIgnore]
		private readonly KeyValueSet<int, int> _levelExpCount = new KeyValueSet<int, int>();
		/// <summary>
		/// 表达式计算
		/// </summary>
		[JsonIgnore]
		private static readonly Exp ExpressionParser = new Exp();

		/// <summary>
		/// 最小等级
		/// </summary>
		[JsonIgnore]
		public int MinLevel
		{
			get
			{
				return 1;
			}
		}

		/// <summary>
		/// 最大等级
		/// </summary>
		[JsonIgnore]
		public int MaxLevel { get; private set; }

		/// <summary>
		/// 当前经验值（唯一需要存储的值，其他值均通过该值计算得出）
		/// </summary>
		public int NowExp { get; private set; }

		/// <summary>
		/// 当前等级
		/// </summary>
		[JsonIgnore]
		public int NowLevel
		{
			get
			{
				var level = 1;
				while (level <= MaxLevel && _levelExpCount[level] <= NowExp)
				{
					level++;
				}
				return level - 1;
			}
		}

		/// <summary>
		/// 升级事件
		/// </summary>
		[JsonIgnore]
		public Action<int, int> OnLevelChanged = delegate { };

		/// <summary>
		/// 当前等级已获得的经验
		/// </summary>
		[JsonIgnore]
		public int NowLevelGotExp
		{
			get { return NowExp - _getExp(NowLevel); }
		}

		/// <summary>
		/// 当前等级总共需要的经验
		/// </summary>
		[JsonIgnore]
		public int NowLevelFullExp
		{
			get { return IsMaxLevel ? 0 : _levelExp[NowLevel + 1]; }
		}

		/// <summary>
		/// 当前等级已获得经验的比例
		/// </summary>
		[JsonIgnore]
		public float NowLevelExpRate
		{
			get { return NowLevelFullExp == 0 ? 0 : (NowLevelGotExp * 1f/ NowLevelFullExp); }
		}

		/// <summary>
		/// 下一级所需经验
		/// </summary>
		[JsonIgnore]
		public int NextLevelExp
		{
			get { return IsMaxLevel ? 0 : _getExp(NowLevel + 1) - NowExp; }
		}

		/// <summary>
		/// 满级所需经验值
		/// </summary>
		[JsonIgnore]
		public int MaxLevelExp
		{
			get { return _getExp(MaxLevel) - NowExp; }
		}

		/// <summary>
		/// 是否满级
		/// </summary>
		[JsonIgnore]
		public bool IsMaxLevel
		{
			get { return NowLevel == MaxLevel; }
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="firstExp">第一级的经验值</param>
		/// <param name="maxLevel">最高等级</param>
		/// <param name="formula">经验值计算公式</param>
		/// 关键字 ：	exp 上一级的经验值	<para/>
		///				level 当前的等级	<para/>
		public LevelValue(int firstExp, int maxLevel, string formula)
		{
			MaxLevel = maxLevel;
			if (maxLevel <= MinLevel) maxLevel = MinLevel;
			// 构造 1 - maxLevel 各个等级的经验值
			_levelExp.Clear();
			_levelExp.Set(1, firstExp);
			_levelExpCount.Set(1, firstExp);
			var sum = firstExp;
			for (var level = 2; level <= maxLevel; level++)
			{
				var str = formula;
				str = str.Replace("exp", _levelExp[level - 1].ToString());
				str = str.Replace("level", level.ToString());
				var value = (int) ExpressionParser.Evaluate(str);
				_levelExp.Set(level, value);
				sum += value;
				_levelExpCount.Set(level, sum);
			}
		}

		/// <summary>
		/// 获取指定等级所需经验
		/// </summary>
		/// <param name="level">等级</param>
		/// <returns>结果</returns>
		private int _getExp(int level)
		{
			level = level > MaxLevel ? MaxLevel : level;
			level = level < MinLevel ? MinLevel : level;
			return _levelExpCount[level];
		}

		/// <summary>
		/// 更改经验值
		/// </summary>
		/// <param name="exp">经验值（可以为负数）</param>
		/// <returns>更改后的经验值</returns>
		public int ChangeExp(int exp)
		{
			if (exp > 0)
			{
				if (exp > MaxLevelExp) exp = MaxLevelExp;
			}
			else if (exp < 0)
			{
				if (NowExp - exp < 0) exp = NowExp;
			}
			// 更改经验值，检查等级是否变化
			var level = NowLevel;
			NowExp += exp;
			if (NowLevel != level)
			{
				OnLevelChanged(level, NowLevel);
			}
			return NowExp;
		}

		/// <summary>
		/// 直接设置目标等级（不会触发等级变更事件）
		/// </summary>
		/// <param name="level">等级</param>
		public void SetLevel(int level)
		{
			NowExp = _getExp(level);
		}
	}
}

