<h1 align="center">"ЕЛЕКТРОННА СИСТЕМА ЗА УПРАВЛЕНИЕ НА ВЕРИГА ОТ АПТЕКИ"</h1>
<h6 align="center">Дипломен проект за държавен изпит по професия "Приложен програмист"</h6>
<br>

## Обща информация
Главната цел на проекта е да се разработи система, която да подпомага дейността на аптеките и складовете, които се занимават с лекарства. Той представлява WEB базирана система, която работи на основата "клиент-сървър". За клиента се използва JavaScript фреймуърка "React", а за сървъра (Web API) - ASP.NET Web API. За бази данни се използва Microsoft SQL Server. Приложение проектът може да намери във всички аптеки и складове, на които главната дейност е свързана с лекарства.

## Архитектура
Архитектурата е трислойна, като презентационния слой е написан на "React" и той прави "HTTP" заявки към API-я, като по този начин клиента и сървъра комуникират помежду си.

## Използвани технологии
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![React](https://img.shields.io/badge/react-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB)
![TypeScript](https://img.shields.io/badge/typescript-%23007ACC.svg?style=for-the-badge&logo=typescript&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)
![MUI](https://img.shields.io/badge/MUI-%230081CB.svg?style=for-the-badge&logo=mui&logoColor=white)
![Git](https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white)

## Инсталация
За да работи проекта е нужно да имате Microsoft SQL Server на вашата машина, както и да имате изтеглен проекта.
Стъпките са следните:
  * За Web API-я трябва да се актуализира базата данни. Може чрез Package Manager конзолата на Visual Studio, като се използва командата "Update-Database" или през терминала, като сте в директорията "server", като се използва командата "dotnet ef database update".
  * Важно е да се уверите, че connection string-а в "appsettings.json" файла е конфигуриран спрямо вашите настройки за "SQL Server".
  * За клиента ("React") трябва да нивигирате до директорията "client" и да изпълните следните две команди в точния ред: "npm install" и "npm run dev".
  * Когато и Web API-я, и клиента са стартирали, както и базата вече е създадена, може да започнете работа в системата.
