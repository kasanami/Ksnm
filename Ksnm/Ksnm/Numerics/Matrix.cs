using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 任意サイズの行列
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public class Matrix<TValue> :
        IAdditionOperators<Matrix<TValue>, Matrix<TValue>, Matrix<TValue>>,
        ISubtractionOperators<Matrix<TValue>, Matrix<TValue>, Matrix<TValue>>,
        IMultiplyOperators<Matrix<TValue>, Matrix<TValue>, Matrix<TValue>>
        where TValue : INumber<TValue>
    {
        #region フィールド
        /// <summary>
        /// 1次元配列
        /// </summary>
        private TValue[,] _array = new TValue[0, 0];
        #endregion フィールド

        #region プロパティ
        public int RowLength = 0;
        public int ColumnLength = 0;
        public int ArrayLength => RowLength * ColumnLength;
        public TValue this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _array[row, column];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                _array[row, column] = value;
            }
        }
        #endregion プロパティ

        #region コンストラクタ
        public Matrix() { }

        public Matrix(int rowLength, int columnLength)
        {
            RowLength = rowLength;
            ColumnLength = columnLength;
            _array = new TValue[rowLength, columnLength];
        }

        public Matrix(Matrix<TValue> other) : this(other.RowLength, other.ColumnLength)
        {
            Array.Copy(other._array, _array, ArrayLength);
        }

        public Matrix(Matrix4x4 other)
        {
            _array = new TValue[4, 4];
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    _array[r, c] = TValue.CreateChecked(other[r, c]);
                }
            }
        }

        public Matrix(TValue[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            Array.Copy(array, _array, ArrayLength);
        }
        #endregion コンストラクタ

        #region Get
        public IEnumerable<TValue> GetColumnItems(int column)
        {
            for (int r = 0; r < RowLength; r++)
            {
                yield return _array[r, column];
            }
        }
        public IEnumerable<TValue> GetRowItems(int row)
        {
            for (int c = 0; c < ColumnLength; c++)
            {
                yield return _array[row, c];
            }
        }
        /// <summary>
        /// 転置した値を取得
        /// </summary>
        /// <returns></returns>
        public Matrix<TValue> GetTranspose()
        {
            Matrix<TValue> temp = new(ColumnLength, RowLength);
            for (int r = 0; r < RowLength; r++)
            {
                for (int c = 0; c < ColumnLength; c++)
                {
                    temp._array[c, r] = _array[r, c];
                }
            }
            return temp;
        }
        #endregion Get

        #region 型変更
        public static implicit operator Matrix<TValue>(TValue[,] array)
        {
            return new Matrix<TValue>(array);
        }
        public static explicit operator TValue[,](Matrix<TValue> value)
        {
            return value._array;
        }
        public TValue[,] AsPrimitive()
        {
            return _array;
        }
        #endregion 型変更

        #region operators
        public static Matrix<TValue> operator +(Matrix<TValue> left, Matrix<TValue> right)
        {
            var rowLength = System.Math.Min(left.RowLength, right.RowLength);
            var columnLength = System.Math.Min(left.ColumnLength, right.ColumnLength);
            var temp = new Matrix<TValue>(rowLength, columnLength);

            for (int r = 0; r < rowLength; r++)
            {
                for (int c = 0; c < columnLength; c++)
                {
                    temp._array[r, c] = left[r, c] + right[r, c];
                }
            }
            return temp;
        }

        public static Matrix<TValue> operator -(Matrix<TValue> left, Matrix<TValue> right)
        {
            var rowLength = System.Math.Min(left.RowLength, right.RowLength);
            var columnLength = System.Math.Min(left.ColumnLength, right.ColumnLength);
            var temp = new Matrix<TValue>(rowLength, columnLength);

            for (int r = 0; r < rowLength; r++)
            {
                for (int c = 0; c < columnLength; c++)
                {
                    temp._array[r, c] = left[r, c] - right[r, c];
                }
            }
            return temp;
        }

        public static Matrix<TValue> operator *(Matrix<TValue> left, Matrix<TValue> right)
        {
            var rowLength = left.RowLength;
            var columnLength = right.ColumnLength;
            var temp = new Matrix<TValue>(rowLength, columnLength);

            for (int r = 0; r < rowLength; r++)
            {
                var leftRowItems = left.GetRowItems(r);
                for (int c = 0; c < columnLength; c++)
                {
                    var rightColumnItems = right.GetColumnItems(c);
                    var count = Min(leftRowItems.Count(), rightColumnItems.Count());
                    TValue tempValue = TValue.Zero;
                    for (int i = 0; i < count; i++)
                    {
                        tempValue += leftRowItems.ElementAt(i) * rightColumnItems.ElementAt(i);
                    }
                    temp._array[r, c] = tempValue;
                }
            }
            return temp;
        }
        #endregion operators

        #region object
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            for (int r = 0; ;)
            {
                stringBuilder.Append("{");
                for (int c = 0; ;)
                {
                    stringBuilder.Append(_array[r, c].ToString());
                    // 次へ
                    c++;
                    if (c < ColumnLength)
                    {
                        stringBuilder.Append(",");
                    }
                    else
                    {
                        break;
                    }
                }
                stringBuilder.Append("}");
                r++;
                if (r < RowLength)
                {
                    stringBuilder.AppendLine(",");
                }
                else
                {
                    break;
                }
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Equals((Matrix<TValue>)obj);
        }
        public bool Equals(Matrix<TValue> other)
        {
            if (other == null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (_array.Length != other._array.Length)
            {
                return false;
            }
            if (RowLength != other.RowLength)
            {
                return false;
            }
            if (ColumnLength != other.ColumnLength)
            {
                return false;
            }
            for (int r = 0; r < RowLength; r++)
            {
                for (int c = 0; c < ColumnLength; c++)
                {
                    if (_array[r, c] != other._array[r, c])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            foreach (var item in _array)
            {
                hashCode = hashCode ^ item.GetHashCode();
            }
            return hashCode;
        }
        #endregion object

        #region private
        private static int Min(int a, int b)
        {
            return System.Math.Min(a, b);
        }
        #endregion private

    }
}