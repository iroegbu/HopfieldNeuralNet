﻿using HopfieldNeuralNetLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopfieldNeuralNetCLI
{
    /// <summary>
    /// Chapter 3: Using a Hopfield Neural Network
    ///
    /// ConsoleHopfield: Simple console application that shows how to
    /// use a Hopfield Neural Network.
    /// </summary>
    public class Program
    {
        // Convert a boolean array to the form [T,T,F,F]
        public static String FormatBoolean(bool[] b)
        {
            StringBuilder result = new StringBuilder();
            result.Append('[');
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i])
                {
                    result.Append("T");
                }
                else
                {
                    result.Append("F");
                }
                if (i != b.Length - 1)
                {
                    result.Append(",");
                }
            }
            result.Append(']');
            return (result.ToString());
        }

        /**
         * A simple main method to test the Hopfield neural network.
         * 
         * @param args
         *            Not used.
         */
        static void Main(string[] args)
        {

            // Create the neural network.
            HopfieldNetwork network = new HopfieldNetwork(4);
            // This pattern will be trained
            bool[] TrainingPattern = { true, true, false, false };
            // This pattern will be presented
            bool[] PresentedPattern = { true, false, false, false };
            bool[] result;

            // train the neural network with pattern1        
            Console.WriteLine("Training Hopfield network with: "
                    + FormatBoolean(TrainingPattern));
            network.Train(TrainingPattern);
            // present pattern1 and see it recognized
            result = network.Present(TrainingPattern);
            Console.WriteLine("Presenting pattern:" + FormatBoolean(TrainingPattern)
                    + ", and got " + FormatBoolean(result));
            // Present pattern2, which is similar to pattern 1. Pattern 1
            // should be recalled.
            result = network.Present(PresentedPattern);
            Console.WriteLine("Presenting pattern:" + FormatBoolean(PresentedPattern)
                    + ", and got " + FormatBoolean(result));
            Console.ReadLine();
        }
    }
}
