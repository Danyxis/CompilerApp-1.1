using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerApp
{
    public static class LaunchActions // Класс для реализации пункта меню "Пуск"
    {
        public static void Launch(MainMenuForm form) // Запуск компилятора
        {
            // Получаем объекты области ввода и вывода
            var inputArea = form.GetInputArea();
            var outputTable = form.GetOutputTable();

            string codeText = inputArea.Text; // Получаем текст из поля ввода

            Lexer scanner = new Lexer(codeText); // Создаем объект лексического анализатора (сканера)

            List<Token> tokens = scanner.Analyse(); // Анализируем текст

            outputTable.Rows.Clear(); // Очищаем таблицу перед выводом новых данных

            // Заполняем таблицу результатами анализа
            foreach (var token in tokens)
            {
                outputTable.Rows.Add(token.TypeCode, token.Name, token.Value, token.Position);
            }
        }
    }
}
