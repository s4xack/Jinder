# Jinder

![.NET Core](https://github.com/s4xack/Jinder/workflows/.NET%20Core/badge.svg)

## Описание

**Jinder** - распределенное клиент серверное приложения для поиска вакансий специалистами и кандидатов компаниями. Основной задачей является упрощение и ускорения процесса взаимодействия компаний и специалистов.

### Базовый функционал

* Авторизация пользователей
* Создание профиля вакансси/резюме 
* Подбор кандидатов согласно вакансии(и наоборот)
* Механизм оценки вакансси/резюме 
  * отказ: навсегда скрыть предложение из подборки
  * отложить: временно убирает предложение с последующим возвращением в подборку
  * одобрение: взаимное одобрение вызовет "матч"
* Матчинг и обмен контактами

## Бэкeнд (ASP.NET Core)
2 Web API сервиса отвечающие за бизнес логику приложения и авторизацию. 
![Architecture](https://i.ibb.co/mhYyyxv/1.jpg)

Для проектирование сервисов был использован паттерн *Repository-Service*.
![RS Pattern](https://i.ibb.co/Ph378xw/2.jpg)

## БД (MS SQL + EF Core)
![DB](https://i.ibb.co/R3t4mMD/Annotation-2020-09-08-190035.jpg)

## UI (WPF)
Десктоп клиент ~из говна и палок~ на WPF с использованием MVVM. 

*В клиенте запилены не все функции, которые есть в API.*
