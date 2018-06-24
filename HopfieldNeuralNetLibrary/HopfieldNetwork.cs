using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopfieldNeuralNetLibrary.MatrixOperations;

namespace HopfieldNeuralNetLibrary
{
    public class HopfieldNetwork
    {
        /// <summary>
        /// The weight matrix.
        /// </summary>
        private Matrix weightMatrix;

        /// <summary>
        /// The weight matrix for this neural network. A Hopfield neural network is a
        /// single layer, fully connected neural network.
        /// 
        /// The inputs and outputs to/from a Hopfield neural network are always
        /// boolean values.
        /// </summary>
        public Matrix LayerMatrix
        {
            get
            {
                return weightMatrix;
            }
        }

        /// <summary>
        /// The number of neurons.
        /// </summary>
        public int Size
        {
            get
            {
                return weightMatrix.Rows;
            }
        }

        /// <summary>
        /// Construct a Hopfield neural network of the specified size.
        /// </summary>
        /// <param name="size">The number of neurons in the network.</param>
        public HopfieldNetwork(int size)
        {
            weightMatrix = new Matrix(size, size);

        }

        /// <summary>
        /// Present a pattern to the neural network and receive the result.
        /// </summary>
        /// <param name="pattern">The pattern to be presented to the neural network.</param>
        /// <returns>The output from the neural network.</returns>
        public bool[] Present(bool[] pattern)
        {

            bool[] output = new bool[pattern.Length];

            // convert the input pattern into a matrix with a single row.
            // also convert the boolean values to bipolar(-1=false, 1=true)
            Matrix inputMatrix = Matrix.CreateRowMatrix(BiPolarUtil
                    .Bipolar2double(pattern));

            // Process each value in the pattern
            for (int col = 0; col < pattern.Length; col++)
            {
                Matrix columnMatrix = weightMatrix.GetCol(col);
                columnMatrix = MatrixMath.Transpose(columnMatrix);

                // The output for this input element is the dot product of the
                // input matrix and one column from the weight matrix.
                double dotProduct = MatrixMath.DotProduct(inputMatrix,
                        columnMatrix);

                // Convert the dot product to either true or false.
                if (dotProduct > 0)
                {
                    output[col] = true;
                }
                else
                {
                    output[col] = false;
                }
            }

            return output;
        }


        /// <summary>
        /// Train the neural network for the specified pattern. The neural network
        /// can be trained for more than one pattern. To do this simply call the
        /// train method more than once. 
        /// </summary>
        /// <param name="pattern">The pattern to train on.</param>
        public void Train(bool[] pattern)
        {
            if (pattern.Length != weightMatrix.Rows)
            {
                throw new Exception("Can't train a pattern of size "
                        + pattern.Length + " on a hopfield network of size "
                        + weightMatrix.Rows);
            }

            // Create a row matrix from the input, convert boolean to bipolar
            Matrix m2 = Matrix.CreateRowMatrix(BiPolarUtil
                    .Bipolar2double(pattern));
            // Transpose the matrix and multiply by the original input matrix
            Matrix m1 = MatrixMath.Transpose(m2);
            Matrix m3 = MatrixMath.Multiply(m1, m2);

            // matrix 3 should be square by now, so create an identity
            // matrix of the same size.
            Matrix identity = MatrixMath.Identity(m3.Rows);

            // subtract the identity matrix
            Matrix m4 = MatrixMath.Subtract(m3, identity);

            // now add the calculated matrix, for this pattern, to the
            // existing weight matrix.
            weightMatrix = MatrixMath.Add(weightMatrix, m4);

        }
    }
}
