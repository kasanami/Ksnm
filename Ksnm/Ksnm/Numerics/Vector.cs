﻿using Ksnm.MachineLearning.Clustering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ksnm.Numerics
{
    public struct Vector<T> :
        IList<T>,
        IAdditionOperators<Vector<T>, Vector<T>, Vector<T>>,
        ISubtractionOperators<Vector<T>, Vector<T>, Vector<T>>,
        IMultiplyOperators<Vector<T>, Vector<T>, Vector<T>>,
        IMultiplyOperators<Vector<T>, T, Vector<T>>,
        IDivisionOperators<Vector<T>, Vector<T>, Vector<T>>,
        IDivisionOperators<Vector<T>, T, Vector<T>>
        where T : INumber<T>
    {
        #region フィールド
        /// <summary>
        /// 1次元配列
        /// </summary>
        T[] _values = [];
        #endregion フィールド
        /// <summary>
        /// 次元数
        /// </summary>
        public int Count => _values.Length;
        public T MagnitudePow2
        {
            get
            {
                T sum = T.Zero;
                for (int i = 0; i < _values.Length; i++)
                {
                    sum += _values[i] * _values[i];
                }
                return sum;
            }
        }
        public T Magnitude => Math.Sqrt(MagnitudePow2);

        public bool IsReadOnly => ((ICollection<T>)_values).IsReadOnly;

        public T this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        #region コンストラクタ
        public Vector()
        {
        }

        public Vector(int count)
        {
            _values = new T[count];
        }

        public Vector(T[] values) : this(values.Length)
        {
            Array.Copy(values, _values, values.Length);
        }

        public Vector(IList<T> values) : this(values.ToArray())
        {
        }

        public Vector(Vector<T> vector) : this(vector._values)
        {
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator Vector<T>(T[] array)
        {
            return new Vector<T>(array);
        }
        public static implicit operator Vector<T>(List<T> array)
        {
            return new Vector<T>(array);
        }
        public static explicit operator T[](Vector<T> value)
        {
            return value._values;
        }
        public T[] AsPrimitive()
        {
            return _values;
        }
        #endregion 型変更

        #region object
        public override int GetHashCode()
        {
            return HashCode.Combine(_values);
        }
        public override string ToString()
        {
            var sb = new StringBuilder(Count * 8);
            sb.Append("{");
            for (int i = 0; i < Count; i++)
            {
                if (i != 0) { sb.Append(","); }
                sb.Append($"{_values[i]}");
            }
            sb.Append("}");
            return sb.ToString();
        }
        #endregion object

        #region operations
        public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] + right[i];
            }
            return result;
        }
        public static Vector<T> operator -(Vector<T> left, Vector<T> right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] - right[i];
            }
            return result;
        }
        public static Vector<T> operator *(Vector<T> left, Vector<T> right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] * right[i];
            }
            return result;
        }
        public static Vector<T> operator *(Vector<T> left, T right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] * right;
            }
            return result;
        }
        public static Vector<T> operator /(Vector<T> left, Vector<T> right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] / right[i];
            }
            return result;
        }
        public static Vector<T> operator /(Vector<T> left, T right)
        {
            Vector<T> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] / right;
            }
            return result;
        }
        #endregion operations

        #region
        public static T DistancePow2(Vector<T> vector1, Vector<T> vector2)
        {
            return (vector1 - vector2).MagnitudePow2;
        }

        public static T Distance(Vector<T> vector1, Vector<T> vector2)
        {
            return (vector1 - vector2).Magnitude;
        }

        public static Vector<T> Lerp(Vector<T> from, Vector<T> to, T t)
        {
            var count = from.Count;
            Vector<T> result = new(count);
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Lerp(from[i], to[i], t);
            }
            return result;
        }

        public static Vector<T> Sum(IEnumerable<Vector<T>> vectors)
        {
            Vector<T> sum = new();
            if (vectors.Count() == 0) { return sum; }
            sum = new(vectors.ElementAt(0).Count);
            foreach (var vector in vectors)
            {
                sum += vector;
            }
            return sum;
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_values).IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)_values).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)_values).RemoveAt(index);
        }

        public void Add(T item)
        {
            ((ICollection<T>)_values).Add(item);
        }

        public void Clear()
        {
            ((ICollection<T>)_values).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_values).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_values).CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)_values).Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
        #endregion
    }
}