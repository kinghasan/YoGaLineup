/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ExpressionParser.cs
//  Info     : 数学表达式计算类
//  Author   : Internet
//  E-mail   : ls9512@vip.qq.com
//
//  How to use :	var exp = new ExpressionParser.ExpressionParser();
//					Debug.Log(exp.EvaluateExpression("1+2+3+4*5-6+(1+2*(3-5))").Value);
//					Debug.Log(exp.Evaluate("1+2+3"));
//
//  Copyright : Internet
//
/////////////////////////////////////////////////////////////////////////////
using System.Linq;
using System.Collections.Generic;

namespace Aya.Maths
{

	/// <summary>
	/// 值接口
	/// </summary>
	public interface IValue
	{
		double Value { get; }
	}

	/// <summary>
	/// 数字
	/// </summary>
	public class Number : IValue
	{
		public double Value
		{
			get => _mValue;
		    set => _mValue = value;
		}
	    private double _mValue;

        public Number(double aValue)
        {
			_mValue = aValue;
		}
		public override string ToString()
		{
			return "" + _mValue + "";
		}
	}

	/// <summary>
	/// 求和操作
	/// </summary>
	public class OperationSum : IValue
	{
		private IValue[] _mValues;
		public double Value
		{
			get { return _mValues.Select(v => v.Value).Sum(); }
		}

		public OperationSum(params IValue[] aValues)
		{
			// collapse unnecessary nested sum operations.
			var v = new List<IValue>(aValues.Length);
			foreach (var I in aValues)
			{
			    if (!(I is OperationSum sum))
			    {
			        v.Add(I);
			    }
			    else
			    {
			        v.AddRange(sum._mValues);
			    }
			}
			_mValues = v.ToArray();
		}

		public override string ToString()
		{
			return "( " + string.Join(" + ", _mValues.Select(v => v.ToString()).ToArray()) + " )";
		}
	}

	/// <summary>
	/// 乘积操作
	/// </summary>
	public class OperationProduct : IValue
	{
		private IValue[] _mValues;
		public double Value
		{
			get { return _mValues.Select(v => v.Value).Aggregate((v1, v2) => v1 * v2); }
		}

		public OperationProduct(params IValue[] aValues)
		{
			_mValues = aValues;
		}

		public override string ToString()
		{
			return "( " + string.Join(" * ", _mValues.Select(v => v.ToString()).ToArray()) + " )";
		}

	}

	/// <summary>
	/// 次方操作
	/// </summary>
	public class OperationPower : IValue
	{
		private IValue _mValue;
		private IValue _mPower;
		public double Value => System.Math.Pow(_mValue.Value, _mPower.Value);

	    public OperationPower(IValue aValue, IValue aPower)
	    {
			_mValue = aValue;
			_mPower = aPower;
		}

		public override string ToString()
		{
			return "( " + _mValue + "^" + _mPower + " )";
		}

	}

	/// <summary>
	/// 负数操作
	/// </summary>
	public class OperationNegate : IValue
	{
		private IValue _mValue;
		public double Value => -_mValue.Value;

	    public OperationNegate(IValue aValue)
	    {
			_mValue = aValue;
		}

		public override string ToString()
		{
			return "( -" + _mValue + " )";
		}
	}
	
	/// <summary>
	/// 倒数操作
	/// </summary>
	public class OperationReciprocal : IValue
	{
		private IValue _mValue;
		public double Value => 1.0 / _mValue.Value;

	    public OperationReciprocal(IValue aValue)
	    {
			_mValue = aValue;
		}

		public override string ToString()
		{
			return "( 1/" + _mValue + " )";
		}
	}

	/// <summary>
	/// 多参数列表
	/// </summary>
	public class MultiParameterList : IValue
	{
		private IValue[] _mValues;
		public IValue[] Parameters => _mValues;

	    public double Value
		{
			get { return _mValues.Select(v => v.Value).FirstOrDefault(); }
		}

		public MultiParameterList(params IValue[] aValues)
		{
			_mValues = aValues;
		}

		public override string ToString()
		{
			return string.Join(", ", _mValues.Select(v => v.ToString()).ToArray());
		}
	}

	/// <summary>
	/// 普通函数
	/// </summary>
	public class CustomFunction : IValue
	{
		private IValue[] _mParams;
		private System.Func<double[], double> _mDelegate;
		private string _mName;
		public double Value
		{
			get
			{
			    if (_mParams == null)
			    {
			        return _mDelegate(null);
			    }
				return _mDelegate(_mParams.Select(p => p.Value).ToArray());
			}
		}

		public CustomFunction(string aName, System.Func<double[], double> aDelegate, params IValue[] aValues)
		{
			_mDelegate = aDelegate;
			_mParams = aValues;
			_mName = aName;
		}

		public override string ToString()
		{
		    if (_mParams == null)
		    {
		        return _mName;
		    }
			return _mName + "( " + string.Join(", ", _mParams.Select(v => v.ToString()).ToArray()) + " )";
		}
	}

	/// <summary>
	/// 参数
	/// </summary>
	public class Parameter : Number
	{
		public string Name { get; private set; }
		public override string ToString()
		{
			return Name + "[" + base.ToString() + "]";
		}

		public Parameter(string aName) : base(0)
		{
			Name = aName;
		}
	}

	/// <summary>
	/// 表达式
	/// </summary>
	public class Expression : IValue
	{
		public Dictionary<string, Parameter> Parameters = new Dictionary<string, Parameter>();
		public IValue ExpressionTree { get; set; }

		public double Value => ExpressionTree.Value;

	    public double[] MultiValue
		{
			get
			{
			    if (ExpressionTree is MultiParameterList t)
				{
					var res = new double[t.Parameters.Length];
				    for (var i = 0; i < res.Length; i++)
				    {
				        res[i] = t.Parameters[i].Value;
				    }
					return res;
				}
				return null;
			}
		}

		public override string ToString()
		{
			return ExpressionTree.ToString();
		}

		public ExpressionDelegate ToDelegate(params string[] aParamOrder)
		{
			var parameters = new List<Parameter>(aParamOrder.Length);
			for (var i = 0; i < aParamOrder.Length; i++)
			{
			    if (Parameters.ContainsKey(aParamOrder[i]))
			    {
			        parameters.Add(Parameters[aParamOrder[i]]);
			    }
			    else
			    {
			        parameters.Add(null);
			    }
			}
			var parameters2 = parameters.ToArray();

			return (p) => Invoke(p, parameters2);
		}
		public MultiResultDelegate ToMultiResultDelegate(params string[] aParamOrder)
		{
			var parameters = new List<Parameter>(aParamOrder.Length);
			for (var i = 0; i < aParamOrder.Length; i++)
			{
			    if (Parameters.ContainsKey(aParamOrder[i]))
			    {
			        parameters.Add(Parameters[aParamOrder[i]]);
			    }
			    else
			    {
			        parameters.Add(null);
			    }
			}
			var parameters2 = parameters.ToArray();

			return (p) => InvokeMultiResult(p, parameters2);
		}

		double Invoke(double[] aParams, Parameter[] aParamList)
		{
			var count = System.Math.Min(aParamList.Length, aParams.Length);
			for (var i = 0; i < count; i++)
			{
			    if (aParamList[i] != null)
			    {
			        aParamList[i].Value = aParams[i];
			    }
			}
			return Value;
		}

		double[] InvokeMultiResult(double[] aParams, Parameter[] aParamList)
		{
			var count = System.Math.Min(aParamList.Length, aParams.Length);
			for (var i = 0; i < count; i++)
			{
			    if (aParamList[i] != null)
			    {
			        aParamList[i].Value = aParams[i];
			    }
			}
			return MultiValue;
		}

		public static Expression Parse(string aExpression)
		{
			return new ExpressionParser().EvaluateExpression(aExpression);
		}

		public class ParameterException : System.Exception { public ParameterException(string aMessage) : base(aMessage) { } }
	}

	public delegate double ExpressionDelegate(params double[] aParams);
	public delegate double[] MultiResultDelegate(params double[] aParams);

	/// <summary>
	/// 表达式剖析器
	/// </summary>
	public class ExpressionParser
	{
		private List<string> _mBracketHeap = new List<string>();
		private Dictionary<string, System.Func<double>> _mConsts = new Dictionary<string, System.Func<double>>();
		private Dictionary<string, System.Func<double[], double>> _mFuncs = new Dictionary<string, System.Func<double[], double>>();
		private Expression _mContext;

		public ExpressionParser()
		{
			var rnd = new System.Random();
			_mConsts.Add("PI", () => System.Math.PI);
			_mConsts.Add("e", () => System.Math.E);
			_mFuncs.Add("sqrt", (p) => System.Math.Sqrt(p.FirstOrDefault()));
			_mFuncs.Add("abs", (p) => System.Math.Abs(p.FirstOrDefault()));
			_mFuncs.Add("ln", (p) => System.Math.Log(p.FirstOrDefault()));
			_mFuncs.Add("floor", (p) => System.Math.Floor(p.FirstOrDefault()));
			_mFuncs.Add("ceiling", (p) => System.Math.Ceiling(p.FirstOrDefault()));
			_mFuncs.Add("round", (p) => System.Math.Round(p.FirstOrDefault()));

			_mFuncs.Add("sin", (p) => System.Math.Sin(p.FirstOrDefault()));
			_mFuncs.Add("cos", (p) => System.Math.Cos(p.FirstOrDefault()));
			_mFuncs.Add("tan", (p) => System.Math.Tan(p.FirstOrDefault()));

			_mFuncs.Add("asin", (p) => System.Math.Asin(p.FirstOrDefault()));
			_mFuncs.Add("acos", (p) => System.Math.Acos(p.FirstOrDefault()));
			_mFuncs.Add("atan", (p) => System.Math.Atan(p.FirstOrDefault()));
			_mFuncs.Add("atan2", (p) => System.Math.Atan2(p.FirstOrDefault(), p.ElementAtOrDefault(1)));
			//System.Math.Floor
			_mFuncs.Add("min", (p) => System.Math.Min(p.FirstOrDefault(), p.ElementAtOrDefault(1)));
			_mFuncs.Add("max", (p) => System.Math.Max(p.FirstOrDefault(), p.ElementAtOrDefault(1)));
			_mFuncs.Add("rnd", (p) => {
			    if (p.Length == 2)
			    {
			        return p[0] + rnd.NextDouble() * (p[1] - p[0]);
			    }

			    if (p.Length == 1)
			    {
			        return rnd.NextDouble() * p[0];
			    }
				return rnd.NextDouble();
			});
		}

		public void AddFunc(string aName, System.Func<double[], double> aMethod)
		{
		    if (_mFuncs.ContainsKey(aName))
		    {
		        _mFuncs[aName] = aMethod;
		    }
		    else
		    {
		        _mFuncs.Add(aName, aMethod);
		    }
		}

		public void AddConst(string aName, System.Func<double> aMethod)
		{
		    if (_mConsts.ContainsKey(aName))
		    {
		        _mConsts[aName] = aMethod;
		    }
		    else
		    {
		        _mConsts.Add(aName, aMethod);
		    }
		}

	    public void RemoveFunc(string aName)
	    {
	        if (_mFuncs.ContainsKey(aName))
	        {
	            _mFuncs.Remove(aName);
	        }
	    }

	    public void RemoveConst(string aName)
	    {
	        if (_mConsts.ContainsKey(aName))
	        {
	            _mConsts.Remove(aName);
	        }
	    }

	    int FindClosingBracket(ref string aText, int aStart, char aOpen, char aClose)
	    {
	        var counter = 0;
	        for (var i = aStart; i < aText.Length; i++)
	        {
	            if (aText[i] == aOpen)
	            {
	                counter++;
	            }

	            if (aText[i] == aClose)
	            {
	                counter--;
	            }

	            if (counter == 0)
	            {
	                return i;
	            }
	        }

	        return -1;
	    }

	    void SubstitudeBracket(ref string aExpression, int aIndex)
	    {
			var closing = FindClosingBracket(ref aExpression, aIndex, '(', ')');
			if (closing > aIndex + 1)
			{
				var inner = aExpression.Substring(aIndex + 1, closing - aIndex - 1);
				_mBracketHeap.Add(inner);
				var sub = "&" + (_mBracketHeap.Count - 1) + ";";
				aExpression = aExpression.Substring(0, aIndex) + sub + aExpression.Substring(closing + 1);
			} else throw new ParseException("Bracket not closed!");
		}

		IValue Parse(string aExpression)
		{
			aExpression = aExpression.Trim();
			var index = aExpression.IndexOf('(');
			while (index >= 0)
			{
				SubstitudeBracket(ref aExpression, index);
				index = aExpression.IndexOf('(');
			}
			if (aExpression.Contains(','))
			{
				var parts = aExpression.Split(',');
				var exp = new List<IValue>(parts.Length);
				for (var i = 0; i < parts.Length; i++)
				{
					var s = parts[i].Trim();
				    if (!string.IsNullOrEmpty(s))
				    {
				        exp.Add(Parse(s));
				    }
				}
				return new MultiParameterList(exp.ToArray());
			}
			else if (aExpression.Contains('+'))
			{
				var parts = aExpression.Split('+');
				var exp = new List<IValue>(parts.Length);
				for (var i = 0; i < parts.Length; i++)
				{
					var s = parts[i].Trim();
				    if (!string.IsNullOrEmpty(s))
				    {
				        exp.Add(Parse(s));
				    }
				}

			    if (exp.Count == 1)
			    {
			        return exp[0];
			    }
				return new OperationSum(exp.ToArray());
			}
			else if (aExpression.Contains('-'))
			{
				var parts = aExpression.Split('-');
				var exp = new List<IValue>(parts.Length);
			    if (!string.IsNullOrEmpty(parts[0].Trim()))
			    {
			        exp.Add(Parse(parts[0]));
			    }
				for (var i = 1; i < parts.Length; i++)
				{
					var s = parts[i].Trim();
				    if (!string.IsNullOrEmpty(s))
				    {
				        exp.Add(new OperationNegate(Parse(s)));
				    }
				}

			    if (exp.Count == 1)
			    {
			        return exp[0];
			    }
				return new OperationSum(exp.ToArray());
			}
			else if (aExpression.Contains('*'))
			{
				var parts = aExpression.Split('*');
				var exp = new List<IValue>(parts.Length);
				for (var i = 0; i < parts.Length; i++)
				{
					exp.Add(Parse(parts[i]));
				}

			    if (exp.Count == 1)
			    {
			        return exp[0];
			    }
				return new OperationProduct(exp.ToArray());
			}
			else if (aExpression.Contains('/'))
			{
				var parts = aExpression.Split('/');
				var exp = new List<IValue>(parts.Length);
			    if (!string.IsNullOrEmpty(parts[0].Trim()))
			    {
			        exp.Add(Parse(parts[0]));
			    }
				for (var i = 1; i < parts.Length; i++)
				{
					var s = parts[i].Trim();
				    if (!string.IsNullOrEmpty(s))
				    {
				        exp.Add(new OperationReciprocal(Parse(s)));
				    }
				}
				return new OperationProduct(exp.ToArray());
			}
			else if (aExpression.Contains('^'))
			{
				var pos = aExpression.IndexOf('^');
				var val = Parse(aExpression.Substring(0, pos));
				var pow = Parse(aExpression.Substring(pos + 1));
				return new OperationPower(val, pow);
			}
			var pPos = aExpression.IndexOf("&");
			if (pPos > 0)
			{
				var fName = aExpression.Substring(0, pPos);
				foreach (var M in _mFuncs)
				{
					if (fName == M.Key)
					{
						var inner = aExpression.Substring(M.Key.Length);
						var param = Parse(inner);
						var multiParams = param as MultiParameterList;
						IValue[] parameters;
					    if (multiParams != null)
					    {
					        parameters = multiParams.Parameters;
					    }
					    else
					    {
					        parameters = new IValue[] { param };
					    }
						return new CustomFunction(M.Key, M.Value, parameters);
					}
				}
			}
			foreach (var C in _mConsts)
			{
				if (aExpression == C.Key)
				{
					return new CustomFunction(C.Key, (p) => C.Value(), null);
				}
			}
			var index2a = aExpression.IndexOf('&');
			var index2b = aExpression.IndexOf(';');
			if (index2a >= 0 && index2b >= 2)
			{
				var inner = aExpression.Substring(index2a + 1, index2b - index2a - 1);
			    if (int.TryParse(inner, out var bracketIndex) && bracketIndex >= 0 && bracketIndex < _mBracketHeap.Count)
				{
					return Parse(_mBracketHeap[bracketIndex]);
				}
				else
				{
				    throw new ParseException("Can't parse substitude token");
				}
			}
			double doubleValue;
			if (double.TryParse(aExpression, out doubleValue))
			{
				return new Number(doubleValue);
			}
			if (ValidIdentifier(aExpression))
			{
			    if (_mContext.Parameters.ContainsKey(aExpression))
			    {
			        return _mContext.Parameters[aExpression];
			    }
				var val = new Parameter(aExpression);
				_mContext.Parameters.Add(aExpression, val);
				return val;
			}

			throw new ParseException("Reached unexpected end within the parsing tree");
		}

		private bool ValidIdentifier(string aExpression)
		{
			aExpression = aExpression.Trim();
		    if (string.IsNullOrEmpty(aExpression))
		    {
		        return false;
		    }

		    if (aExpression.Length < 1)
		    {
		        return false;
		    }

		    if (aExpression.Contains(" "))
		    {
		        return false;
		    }

		    if (!"abcdefghijklmnopqrstuvwxyz§$".Contains(char.ToLower(aExpression[0])))
		    {
		        return false;
		    }

		    if (_mConsts.ContainsKey(aExpression))
		    {
		        return false;
            }

		    if (_mFuncs.ContainsKey(aExpression))
		    {
		        return false;
		    }
			return true;
		}

		public Expression EvaluateExpression(string aExpression)
		{
			var val = new Expression();
			_mContext = val;
			val.ExpressionTree = Parse(aExpression);
			_mContext = null;
			_mBracketHeap.Clear();
			return val;
		}

	    public double Evaluate(string aExpression)
	    {
	        return EvaluateExpression(aExpression).Value;
	    }

	    public static double Eval(string aExpression)
	    {
	        return new ExpressionParser().Evaluate(aExpression);
	    }

	    /// <summary>
	    /// 转换异常
	    /// </summary>
	    public class ParseException : System.Exception
	    {
	        public ParseException(string aMessage) : base(aMessage)
	        {

	        }
	    }
	}
}