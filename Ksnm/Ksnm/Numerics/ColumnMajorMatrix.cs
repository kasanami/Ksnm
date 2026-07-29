using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksnm.Numerics
{
    /// <summary>
    /// 列優先行列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColumnMajorMatrix<T> where T : struct
    {
        private T[] _values = new T[0];
        public T[] Values => _values;
        public int RowCount { get; } = 0;
        public int ColumnCount { get; } = 0;
        public int ValuesCount { get; } = 0;

        public ColumnMajorMatrix() { }

        public ColumnMajorMatrix(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            ValuesCount = rowCount * columnCount;
            _values = new T[ValuesCount];
        }

        public ColumnMajorMatrix(int rowCount, int columnCount, T value)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            ValuesCount = rowCount * columnCount;
            _values = new T[ValuesCount];
            for (int i = 0; i < ValuesCount; i++)
            {
                _values[i] = value;
            }
        }

        public ColumnMajorMatrix(int rowCount, int columnCount, ReadOnlySpan<T> values)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            ValuesCount = rowCount * columnCount;
            _values = new T[ValuesCount];
            for (int i = 0; i < ValuesCount; i++)
            {
                _values[i] = values[i % values.Length];
            }
        }
        public T this[int row, int col]
        {
            get => _values[row * ColumnCount + col];
            set => _values[row * ColumnCount + col] = value;
        }
        /// <summary>
        /// 列優先行列を行優先行列に変換する
        /// </summary>
        public static explicit operator RowMajorMatrix<T>(ColumnMajorMatrix<T> matrix)
        {
            var rowMajorMatrix = new RowMajorMatrix<T>(matrix.RowCount, matrix.ColumnCount);
            for (int row = 0; row < matrix.RowCount; row++)
            {
                for (int col = 0; col < matrix.ColumnCount; col++)
                {
                    rowMajorMatrix[row, col] = matrix[row, col];
                }
            }
            return rowMajorMatrix;
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    sb.Append(this[row, col]).Append("\t");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
