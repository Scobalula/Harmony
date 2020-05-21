using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Harmony
{
    internal class CSV
    {
        /// <summary>
        /// Parses lines from a CSV File
        /// </summary>
        /// <param name="path">Path of the CSV File</param>
        /// <returns>Each row</returns>
        public static IEnumerable<string[]> LoadFile(string path)
        {
            using var parser = new TextFieldParser(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                TextFieldType = FieldType.Delimited
            };

            parser.SetDelimiters(",");
            parser.CommentTokens = new string[] { "#" };

            while (!parser.EndOfData)
                yield return parser.ReadFields();
        }
    }
}
