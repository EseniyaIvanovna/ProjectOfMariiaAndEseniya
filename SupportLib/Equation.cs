using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMath;
using System.IO;


namespace SupportLib
{
    public class Equation
    {
        public static string CreateEquationFirstVariant(string Latex)
        {
            const string fileName = @"Equation1Var.PNG";
            var parser = new TexFormulaParser();
            var formula = parser.Parse(Latex);
            var pngByte = formula.RenderToPng(80.0, 0.0, 0.0, "Arial");
            File.WriteAllBytes(fileName, pngByte);
            return fileName;
        }
        public static string CreateEquationSecondVariant(string Latex)
        {
            const string fileName = @"Equation2Var.PNG";
            var parser = new TexFormulaParser();
            var formula = parser.Parse(Latex);
            var pngByte = formula.RenderToPng(80.0, 0.0, 0.0, "Arial");
            File.WriteAllBytes(fileName, pngByte);
            return fileName;
        }
    }
}
