# Test_66bit
Test for 66bit

Добрый день

Приложение работает с БД, которая создается с помощью скрипта из папки SQL. В приложении реализован подход Code First (таблицы в БД создаются с помощью миграций)

Известные недоработки: 
1. У записей, добавленных через SignalR (в реальном времени), на втором клиенте не работают кнопки изменения и удаления (неверный id)
2. Дата у записей, добавленных через SignalR, отображается некорректно (ошибка форматирования)
3. На втором клиенте не обновляются (в реальном времени) добавленные команды

На создание было затрачено 2 дня