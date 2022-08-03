using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Simplify
{
    public static class DFS
    {
        internal static bool Find = false;

        public static (bool, List<T>) Search<T>(List<T> startList, Func<T, IEnumerable<T>> nextNodesGetter, Predicate<T> finishPredicate, int maxDepth = 10)
        {
            Find = false;
            var last = startList[startList.Count - 1];
            SearchRecursion(startList, last, nextNodesGetter, finishPredicate, 1, maxDepth);
            return (Find, startList);
        }

        public static (bool, List<T>) Search<T>(T startNode, Func<T, IEnumerable<T>> nextNodesGetter, Predicate<T> finishPredicate, int maxDepth = 10)
        {
            Find = false;
            var result = new List<T>();
            SearchRecursion(result, startNode, nextNodesGetter, finishPredicate, 1, maxDepth);
            return (Find, result);
        }

        internal static void SearchRecursion<T>(List<T> result, T currentNode, Func<T, IEnumerable<T>> nextNodesGetter, Predicate<T> finishPredicate, int depth, int maxDepth)
        {
            if (!result.Contains(currentNode))
            {
                result.Add(currentNode);
            }
            
            if (Find) return;
            if (depth >= maxDepth) return;
            if (finishPredicate(currentNode))
            {
                Find = true;
                return;
            }

            var nextItems = nextNodesGetter(currentNode);
            foreach (var nextItem in nextItems)
            {
                if (result.Contains(nextItem)) continue;
                SearchRecursion(result, nextItem, nextNodesGetter, finishPredicate, depth + 1, maxDepth);
                if (Find) return;
            }

            result.RemoveAt(result.Count - 1);
        }
    }

    public static class Loop
    {
        #region For

        public static void For(int count, Action action)
        {
            For(0, count - 1, action);
        }

        public static void For(int count, Action<int> action)
        {
            For(0, count - 1, action);
        }

        public static void For(int from, int to, Action action)
        {
            For(from, to, 1, action);
        }

        public static void For(int from, int to, int step, Action action)
        {
            for (var i = from; i <= to; i += step)
            {
                action();
            }
        }

        public static void For(int from, int to, Action<int> action)
        {
            For(from, to, 1, action);
        }

        public static void For(int from, int to, int step, Action<int> action)
        {
            for (var i = from; i <= to; i += step)
            {
                action(i);
            }
        }

        #endregion

        #region For^2

        public static void For(int count, Action<int, int> action)
        {
            For(0, count - 1, i => { For(0, count - 1, j => { action(i, j); }); });
        }

        public static void For(int from, int to, Action<int, int> action)
        {
            For(from, to, i => { For(from, to, j => { action(i, j); }); });
        }

        #endregion

        #region While

        public static void While(Func<bool> condition, Action action)
        {
            var i = 0;
            while (condition())
            {
                action();
                i++;
            }
        }

        public static void While(Func<int, bool> condition, Action action)
        {
            var i = 0;
            while (condition(i))
            {
                action();
                i++;
            }
        }

        public static void While(Func<bool> condition, Action<int> action)
        {
            var i = 0;
            while (condition())
            {
                action(i);
                i++;
            }
        }

        public static void While(Func<int, bool> condition, Action<int> action)
        {
            var i = 0;
            while (condition(i))
            {
                action(i);
                i++;
            }
        }

        #endregion

        #region Do

        public static void Do(Func<bool> condition, Action action)
        {
            var i = 0;
            do
            {
                action();
                i++;
            } while (condition());
        }

        public static void Do(Func<int, bool> condition, Action action)
        {
            var i = 0;
            do
            {
                action();
                i++;
            } while (condition(i));
        }

        public static void Do(Func<bool> condition, Action<int> action)
        {
            var i = 0;
            do
            {
                action(i);
                i++;
            } while (condition());
        }

        public static void Do(Func<int, bool> condition, Action<int> action)
        {
            var i = 0;
            do
            {
                action(i);
                i++;
            } while (condition(i));
        }

        #endregion

        #region Foreach

        public static void Foreach<T>(Action<T> action, params IEnumerable<T>[] sources)
        {
            foreach (var source in sources)
            {
                foreach (var item in source)
                {
                    action(item);
                }
            }
        }

        #endregion
    }
}
