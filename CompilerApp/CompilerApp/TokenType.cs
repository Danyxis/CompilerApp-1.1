using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    internal enum TokenType // Условные типы и коды лексем
    {
        // Ключевые слова
        Int = 1,          // "int"
        Float = 2,        // "float"
        Char = 3,         // "char"
        String = 4,       // "string"
        Bool = 5,         // "bool"

        Identifier = 6,   // Идентификатор (названия переменных, функций, параметров)

        Space = 7,            // Разделитель (пробел) " "
        OpenParenthesis = 8,  // Открывающая скобка "("
        CloseParenthesis = 9, // Закрывающая скобка ")"
        Comma = 10,           // Запятая ","
        Semicolon = 11,       // Конец оператора ";"

        Invalid = 666         // Недопустимый символ (ошибка)
    }
}
