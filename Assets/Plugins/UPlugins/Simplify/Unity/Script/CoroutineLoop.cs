using System;
using System.Collections;

namespace Aya.Simplify
{
    public static class CoroutineLoop
    {
        #region For

        public static IEnumerator For(int count, Action action, object returnValue = null)
        {
            return For(0, count - 1, action, returnValue);
        }

        public static IEnumerator For(int count, Action<int> action, object returnValue = null)
        {
            return For(0, count - 1, action, returnValue);
        }

        public static IEnumerator For(int from, int to, Action action, object returnValue = null)
        {
            return For(from, to, 1, action, returnValue);
        }

        public static IEnumerator For(int from, int to, int step, Action action, object returnValue = null)
        {
            for (var i = from; i <= to; i += step)
            {
                action();
                yield return returnValue;
            }
        }

        public static IEnumerator For(int from, int to, Action<int> action, object returnValue = null)
        {
            return For(from, to, 1, action, returnValue);
        }

        public static IEnumerator For(int from, int to, int step, Action<int> action, object returnValue = null)
        {
            for (var i = from; i <= to; i += step)
            {
                action(i);
                yield return returnValue;
            }
        }

        #endregion

        #region For^2

        public static IEnumerator For(int count, Action<int, int> action, object returnValue = null)
        {
            return For(0, count - 1, i => { For(0, count - 1, j => { action(i, j); }, returnValue); }, returnValue);
        }

        public static IEnumerator For(int from, int to, Action<int, int> action, object returnValue = null)
        {
            return For(from, to, i => { For(from, to, j => { action(i, j); }, returnValue); }, returnValue);
        }

        #endregion

        #region While

        public static IEnumerator While(Func<bool> condition, Action action, object returnValue = null)
        {
            var i = 0;
            while (condition())
            {
                action();
                i++;
                yield return returnValue;
            }
        }

        public static IEnumerator While(Func<int, bool> condition, Action action, object returnValue = null)
        {
            var i = 0;
            while (condition(i))
            {
                action();
                i++;
                yield return returnValue;
            }
        }

        public static IEnumerator While(Func<bool> condition, Action<int> action, object returnValue = null)
        {
            var i = 0;
            while (condition())
            {
                action(i);
                i++;
                yield return returnValue;
            }
        }

        public static IEnumerator While(Func<int, bool> condition, Action<int> action, object returnValue = null)
        {
            var i = 0;
            while (condition(i))
            {
                action(i);
                i++;
                yield return returnValue;
            }
        }

        #endregion

        #region Do

        public static IEnumerator Do(Func<bool> condition, Action action, object returnValue = null)
        {
            var i = 0;
            do
            {
                action();
                i++;
                yield return returnValue;
            } while (condition());
        }

        public static IEnumerator Do(Func<int, bool> condition, Action action, object returnValue = null)
        {
            var i = 0;
            do
            {
                action();
                i++;
                yield return returnValue;
            } while (condition(i));
        }

        public static IEnumerator Do(Func<bool> condition, Action<int> action, object returnValue = null)
        {
            var i = 0;
            do
            {
                action(i);
                i++;
                yield return returnValue;
            } while (condition());
        }

        public static IEnumerator Do(Func<int, bool> condition, Action<int> action, object returnValue = null)
        {
            var i = 0;
            do
            {
                action(i);
                i++;
                yield return returnValue;
            } while (condition(i));
        }

        #endregion
    }
}