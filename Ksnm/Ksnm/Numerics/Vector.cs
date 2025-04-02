using Ksnm.Units;
using System.Collections;
using System.Numerics;
using System.Text;

namespace Ksnm.Numerics
{
    /// <summary>
    /// ベクトル
    /// </summary>
    /// <typeparam name="TValue">値型</typeparam>
    public class Vector<TValue> :
        IList<TValue>,
        IAdditionOperators<Vector<TValue>, Vector<TValue>, Vector<TValue>>,
        ISubtractionOperators<Vector<TValue>, Vector<TValue>, Vector<TValue>>,
        IMultiplyOperators<Vector<TValue>, Vector<TValue>, Vector<TValue>>,
        IMultiplyOperators<Vector<TValue>, TValue, Vector<TValue>>,
        IDivisionOperators<Vector<TValue>, Vector<TValue>, Vector<TValue>>,
        IDivisionOperators<Vector<TValue>, TValue, Vector<TValue>>,
        IEquatable<Vector<TValue>>
        where TValue : INumber<TValue>, IRootFunctions<TValue>
    {
        #region フィールド
        /// <summary>
        /// 1次元配列
        /// </summary>
        TValue[] _values = [];
        #endregion フィールド
        /// <summary>
        /// 次元数
        /// </summary>
        public int Count => _values.Length;
        public TValue MagnitudePow2
        {
            get
            {
                TValue sum = TValue.Zero;
                for (int i = 0; i < _values.Length; i++)
                {
                    sum += _values[i] * _values[i];
                }
                return sum;
            }
        }
        public TValue Magnitude => TValue.Sqrt(MagnitudePow2);

        public bool IsReadOnly => ((ICollection<TValue>)_values).IsReadOnly;

        public TValue this[int index]
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
            _values = new TValue[count];
        }

        public Vector(TValue[] values) : this(values.Length)
        {
            Array.Copy(values, _values, values.Length);
        }

        public Vector(IList<TValue> values) : this(values.ToArray())
        {
        }

        public Vector(Vector<TValue> vector) : this(vector._values)
        {
        }
        #endregion コンストラクタ

        #region 型変更
        public static implicit operator Vector<TValue>(TValue[] array)
        {
            return new Vector<TValue>(array);
        }
        public static implicit operator Vector<TValue>(List<TValue> array)
        {
            return new Vector<TValue>(array);
        }
        public static explicit operator TValue[](Vector<TValue> value)
        {
            return value._values;
        }
        public TValue[] AsPrimitive()
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
        public static Vector<TValue> operator +(Vector<TValue> left, Vector<TValue> right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] + right[i];
            }
            return result;
        }
        public static Vector<TValue> operator -(Vector<TValue> left, Vector<TValue> right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] - right[i];
            }
            return result;
        }
        public static Vector<TValue> operator *(Vector<TValue> left, Vector<TValue> right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] * right[i];
            }
            return result;
        }
        public static Vector<TValue> operator *(Vector<TValue> left, TValue right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] * right;
            }
            return result;
        }
        public static Vector<TValue> operator /(Vector<TValue> left, Vector<TValue> right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] / right[i];
            }
            return result;
        }
        public static Vector<TValue> operator /(Vector<TValue> left, TValue right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] / right;
            }
            return result;
        }
        public static Vector<TValue> operator %(Vector<TValue> left, Vector<TValue> right)
        {
            Vector<TValue> result = new(left._values.Length);
            for (int i = 0; i < left._values.Length; i++)
            {
                result[i] = left[i] % right[i];
            }
            return result;
        }
        #endregion operations

        #region operation 代替関数
        public Vector<TValue> AddAssign(Vector<TValue> other)
        {
            var length = int.Min(_values.Length, other._values.Length);
            for (int i = 0; i < length; i++)
            {
                _values[i] += other._values[i];
            }
            return this;
        }
        public Vector<TValue> SubtractAssign(Vector<TValue> other)
        {
            var length = int.Min(_values.Length, other._values.Length);
            for (int i = 0; i < length; i++)
            {
                _values[i] -= other._values[i];
            }
            return this;
        }
        public Vector<TValue> MultiplyAssign(Vector<TValue> other)
        {
            var length = int.Min(_values.Length, other._values.Length);
            for (int i = 0; i < length; i++)
            {
                _values[i] *= other._values[i];
            }
            return this;
        }
        public Vector<TValue> DivideAssign(Vector<TValue> other)
        {
            var length = int.Min(_values.Length, other._values.Length);
            for (int i = 0; i < length; i++)
            {
                _values[i] /= other._values[i];
            }
            return this;
        }
        public Vector<TValue> ModulusAssign(Vector<TValue> other)
        {
            var length = int.Min(_values.Length, other._values.Length);
            for (int i = 0; i < length; i++)
            {
                _values[i] %= other._values[i];
            }
            return this;
        }
        #endregion operation 代替関数

        #region
        public static TValue DistancePow2(Vector<TValue> vector1, Vector<TValue> vector2)
        {
            return (vector1 - vector2).MagnitudePow2;
        }

        public static TValue Distance(Vector<TValue> vector1, Vector<TValue> vector2)
        {
            return (vector1 - vector2).Magnitude;
        }

        public static Vector<TValue> Lerp(Vector<TValue> from, Vector<TValue> to, TValue t)
        {
            var count = from.Count;
            Vector<TValue> result = new(count);
            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Lerp(from[i], to[i], t);
            }
            return result;
        }

        public static Vector<TValue> Sum(IEnumerable<Vector<TValue>> vectors)
        {
            Vector<TValue> sum = new();
            if (vectors.Count() == 0) { return sum; }
            sum = new(vectors.ElementAt(0).Count);
            foreach (var vector in vectors)
            {
                sum += vector;
            }
            return sum;
        }

        public int IndexOf(TValue item)
        {
            return ((IList<TValue>)_values).IndexOf(item);
        }

        public void Insert(int index, TValue item)
        {
            ((IList<TValue>)_values).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<TValue>)_values).RemoveAt(index);
        }

        public void Add(TValue item)
        {
            var list = new List<TValue>(_values);
            list.Add(item);
            _values = list.ToArray();
        }

        public void Clear()
        {
            ((ICollection<TValue>)_values).Clear();
        }

        public bool Contains(TValue item)
        {
            return ((ICollection<TValue>)_values).Contains(item);
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            ((ICollection<TValue>)_values).CopyTo(array, arrayIndex);
        }

        public bool Remove(TValue item)
        {
            return ((ICollection<TValue>)_values).Remove(item);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return ((IEnumerable<TValue>)_values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
        #endregion

        #region IEquatable
        public bool Equals(Vector<TValue>? other)
        {
            return _values.SequenceEqual(other._values);
        }
        #endregion IEquatable
    }
}