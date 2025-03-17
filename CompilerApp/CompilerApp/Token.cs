using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    internal class Token // Класс лексемы
    {
        public int TypeCode { get; }       // Код лексемы (числовое значение)
        public string Name { get; }        // Тип лексемы (ключевое слово, идентификатор и т.д.)
        public string Value { get; }       // Значение лексемы (сама строка)
        public string Position { get; }    // Позиция в исходном коде

        public Token(TokenType type, string name, string value, string position)
        {
            TypeCode = (int)type;
            Name = name;
            Value = value;
            Position = position;
        }
    }
}
