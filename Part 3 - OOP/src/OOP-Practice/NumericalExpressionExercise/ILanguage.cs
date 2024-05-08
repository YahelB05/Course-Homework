using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Practice.NumericalExpressionExercise
{
    internal interface ILanguage
    {
        string ToLiteral(long value);
    }
}
