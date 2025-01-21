Для работы нужно иметь pgsql и чтобы данная строка подключения работала Host=localhost;Port=5432;Database=budget_calc;Username=mainclient;Password=12345678.
Для mainclient достаточно иметь прав на работу с данными во всех таблицах схемы public (или же только для таблиц user, transaction, transaction_category, transaction_type).
Для быстрого создания нужных таблиц достаточно запустить budget_calcbackup.sql из папки schemaDump, предварительно удалив схему public.
