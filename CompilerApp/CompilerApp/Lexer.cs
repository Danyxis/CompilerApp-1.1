using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CompilerApp
{
    internal class Lexer // Класс лексического анализатора (сканера)
    {
        private string CodeText; // Исходный текст
        private List<Token> tokens = new List<Token>(); // Список найденных токенов

        // Словарь ключевых слов
        private readonly Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>
        {
            { "int", TokenType.Int },
            { "float", TokenType.Float },
            { "char", TokenType.Char },
            { "string", TokenType.String },
            { "bool", TokenType.Bool }
        };

        public Lexer(string text)
        {
            this.CodeText = text; // Инициализация исходного текста
        }

        public List<Token> Analyse() // Анализ исходного текста
        {
            tokens.Clear(); // Очистка списка токенов перед анализом
            int status = 0; // Переменная для хранения состояния автомата
            string word = ""; // Переменная для накопления идентификаторов
            int position = 0; // Текущая позиция в строке
            int beginPosition = 0; // Начальная позиция текущего слова

            while (position < CodeText.Length)
            {
                char symbol = CodeText[position]; // Текущий символ

                switch (status)
                {
                    case 0:
                        if (char.IsLetter(symbol)) // Если символ - буква, начинаем идентификатор или ключевое слово
                        {
                            word += symbol;
                            beginPosition = position;
                            position++;
                            status = 1;
                        }
                        else if (char.IsWhiteSpace(symbol)) // Если пробел - игнорируем
                        {
                            position++;
                        }
                        else if (symbol == '(')
                        {
                            status = 3;
                        }
                        else if (symbol == ')')
                        {
                            status = 4;
                        }
                        else if (symbol == ',')
                        {
                            status = 5;
                        }
                        else if (symbol == ';')
                        {
                            status = 6;
                        }
                        else // Если недопустимый символ
                        {
                            status = 7;
                        }
                        break;

                    case 1: // Состояние обработки идентификаторов и ключевых слов
                        if (char.IsLetterOrDigit(symbol) || symbol == '_') // Продолжаем накапливать символы идентификатора
                        {
                            word += symbol;
                            position++;
                        }
                        else // Если символ больше не подходит
                        {
                            TokenType type;
                            if (keywords.ContainsKey(word)) // Проверяем, является ли слово ключевым
                            {
                                type = keywords[word];
                            }
                            else
                            {
                                type = TokenType.Identifier; // В противном случае - идентификатор
                            }

                            string description;
                            if (type == TokenType.Identifier)
                            {
                                description = "идентификатор";
                            }
                            else
                            {
                                description = "ключевое слово";
                            }

                            // Добавляем найденный токен в список
                            tokens.Add(new Token(type, description, word, $"с {beginPosition + 1} по {position} символ"));

                            word = ""; // Очищаем временную переменную
                            status = 0;

                            // Если текущий символ - пробел, переходим в соответствующее состояние
                            if (position < CodeText.Length && char.IsWhiteSpace(symbol))
                            {
                                status = 2;
                            }
                        }
                        break;

                    case 2: // Обработка пробелов
                        tokens.Add(new Token(TokenType.Space, "разделитель", "пробел", $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    case 3: // Обработка открывающей скобки
                        tokens.Add(new Token(TokenType.OpenParenthesis, "открывающая скобка", symbol.ToString(), $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    case 4: // Обработка закрывающей скобки
                        tokens.Add(new Token(TokenType.CloseParenthesis, "закрывающая скобка", symbol.ToString(), $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    case 5: // Обработка запятой
                        tokens.Add(new Token(TokenType.Comma, "запятая", symbol.ToString(), $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    case 6: // Обработка точки с запятой
                        tokens.Add(new Token(TokenType.Semicolon, "конец оператора", symbol.ToString(), $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    case 7: // Обработка недопустимого символа
                        tokens.Add(new Token(TokenType.Invalid, "недопустимый символ", symbol.ToString(), $"{position + 1} символ"));
                        status = 0;
                        position++;
                        break;

                    default:
                        break;
                }
            }

            return tokens; // Возвращаем список найденных токенов
        }
    }
}
