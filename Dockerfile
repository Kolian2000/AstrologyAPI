# Используем базовый образ PostgreSQL
FROM postgres:latest

# Определяем переменные окружения для настройки базы данных
ENV POSTGRES_DB taro
ENV POSTGRES_USER roza
ENV POSTGRES_PASSWORD Kolian1232329

# Копируем скрипты инициализации в директорию контейнера
COPY your-backup-script.sql /docker-entrypoint-initdb.d/

# Устанавливаем права доступа для скриптов


