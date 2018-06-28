using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopfieldNeuralNetLibrary.MatrixOperations
{
    /// <summary>
    /// MatrixMath: This class can perform many different mathematical
    /// operations on matrixes.
    /// </summary>
    public class MatrixMath
    {
        // Add two matrixes together
        public static Matrix Add(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows)
            {
                throw new Exception(
                        "To add the matrixes they must have the same number of rows and columns.  Matrix a has "
                                + a.Rows
                                + " rows and matrix b has "
                                + b.Rows + " rows.");
            }

            if (a.Cols != b.Cols)
            {
                throw new Exception(
                        "To add the matrixes they must have the same number of rows and columns.  Matrix a has "
                                + a.Cols
                                + " cols and matrix b has "
                                + b.Cols + " cols.");
            }

            double[,] result = new double[a.Rows, a.Cols];

            for (int resultRow = 0; resultRow < a.Rows; resultRow++)
            {
                for (int resultCol = 0; resultCol < a.Cols; resultCol++)
                {
                    result[resultRow, resultCol] = a[resultRow, resultCol]
                            + b[resultRow, resultCol];
                }
            }

            return new Matrix(result);
        }

        // Copy the source matrix to the target matrix. Both matrixes must have the same dimensions.
        public static void Copy(Matrix source, Matrix target)
        {
            for (int row = 0; row < source.Rows; row++)
            {
                for (int col = 0; col < source.Cols; col++)
                {
                    target[row, col] = source[row, col];
                }
            }

        }

        // Compute the dot product for two matrixes
        public static double DotProduct(Matrix a, Matrix b)
        {
            if (!a.IsVector() || !b.IsVector())
            {
                throw new Exception(
                        "To take the dot product, both matrixes must be vectors.");
            }

            Double[] aArray = a.ToPackedArray();
            Double[] bArray = b.ToPackedArray();

            if (aArray.Length != bArray.Length)
            {
                throw new Exception(
                        "To take the dot product, both matrixes must be of the same length.");
            }

            double result = 0;
            int length = aArray.Length;

            for (int i = 0; i < length; i++)
            {
                result += aArray[i] * bArray[i];
            }

            return result;
        }

        // Create an identiry matrix, of the specified size.  An identity matrix is always square.
        public static Matrix Identity(int size)
        {
            if (size < 1)
            {
                throw new Exception("Identity matrix must be at least of size 1.");
            }

            Matrix result = new Matrix(size, size);

            for (int i = 0; i < size; i++)
            {
                result[i, i] = 1;
            }

            return result;
        }

        // Multiply every cell in the matrix by the specified value.
        public static Matrix Multiply(Matrix a, double b)
        {
            double[,] result = new double[a.Rows, a.Cols];
            for (int row = 0; row < a.Rows; row++)
            {
                for (int col = 0; col < a.Cols; col++)
                {
                    result[row, col] = a[row, col] * b;
                }
            }
            return new Matrix(result);
        }

        // Multiply two matrixes.
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Cols != b.Rows)
            {
                throw new Exception(
                        "To use ordinary matrix multiplication the number of columns on the first matrix must mat the number of rows on the second.");
            }

            double[,] result = new double[a.Rows, b.Cols];

            for (int resultRow = 0; resultRow < a.Rows; resultRow++)
            {
                for (int resultCol = 0; resultCol < b.Cols; resultCol++)
                {
                    double value = 0;

                    for (int i = 0; i < a.Cols; i++)
                    {

                        value += a[resultRow, i] * b[i, resultCol];
                    }
                    result[resultRow, resultCol] = value;
                }
            }

            return new Matrix(result);
        }

        // Subtract one matrix from another.  The two matrixes must have the same number of rows and columns.
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a.Rows != b.Rows)
            {
                throw new Exception(
                        "To subtract the matrixes they must have the same number of rows and columns.  Matrix a has "
                                + a.Rows
                                + " rows and matrix b has "
                                + b.Rows + " rows.");
            }

            if (a.Cols != b.Cols)
            {
                throw new Exception(
                        "To subtract the matrixes they must have the same number of rows and columns.  Matrix a has "
                                + a.Cols
                                + " cols and matrix b has "
                                + b.Cols + " cols.");
            }

            double[,] result = new double[a.Rows, a.Cols];

            for (int resultRow = 0; resultRow < a.Rows; resultRow++)
            {
                for (int resultCol = 0; resultCol < a.Cols; resultCol++)
                {
                    result[resultRow, resultCol] = a[resultRow, resultCol]
                            - b[resultRow, resultCol];
                }
            }

            return new Matrix(result);
        }

        // Transpose a matrix.
        public static Matrix Transpose(Matrix input)
        {
            double[,] inverseMatrix = new double[input.Cols, input
                    .Rows];

            for (int r = 0; r < input.Rows; r++)
            {
                for (int c = 0; c < input.Cols; c++)
                {
                    inverseMatrix[c, r] = input[r, c];
                }
            }

            return new Matrix(inverseMatrix);
        }

        private MatrixMath()
        {
        }

    }
}
