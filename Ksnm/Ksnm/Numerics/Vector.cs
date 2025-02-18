using Ksnm.MachineLearning.Clustering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    public struct Vector<T> :
        IAdditionOperators<Vector<T>, Vector<T>, Vector<T>>,
        ISubtractionOperators<Vector<T>, Vector<T>, Vector<T>>,
        IMultiplyOperators<Vector<T>, Vector<T>, Vector<T>>,
        IMultiplyOperators<Vector<T>, T, Vector<T>>,
        IDivisionOperators<Vector<T>, Vector<T>, Vector<T>>,
        IDivisionOperators<Vector<T>, T, Vector<T>>
        where T : INumber<T>
    {
        T[] _values = [];
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

        public Vector(Vector<T> vector) : this(vector._values)
        {
        }
        #endregion コンストラクタ

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
        #endregion
    }
}