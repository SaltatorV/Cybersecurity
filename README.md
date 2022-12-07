# Instalacja Bazy Danych

## docker pull postgres
## docker run --name pgsql-dev -e POSTGRES_PASSWORD=cybersecurity -p 5433:5432 -d postgres

Reszta jest już skonfigurowana w appsetting wystarczy zrobić update-database lub EntityFrameworkCore\Update-Database
# Cybersecurity

## Zadanie do zrealizowania - II ##

### I. Udoskonalenie projeku z ćwiczenia I: ###

- Identyfikacja użytkownika (uwierzytelnienie) na podstawie danych uwierzytelniających i wzależności od wyników identyfikacji dopuszczenie użytkownika do dalszych działań w systemie lub zabronienie dostępu.

- W przypadku pomyślnego uwierzytelnienia użytkownika i podaniu przez niego kodu operacji   na   obiekcie,   sprawdzenie autoryzacji  użytkownikai,  w  zależności  od wyników tego sprawdzenia,  dopuszczenie  do  wykonania  danej  operacji  na  wybranym obiekcie lub zabronienie jej wykonania.

Seba -> 1. Program  powinien monitorować  aktywności  użytkowników. Odnotowywane są takie zdarzenia jak: logowanie / wylogowanie do programu; tworzenie/usunięcie użytkownika; nadanie/odebranie uprawnień  użytkownika;  zmiana  hasla  użytkownika...  Gdzie wyszczególnia się następujące informacje: Nazwa użytkownika; Data i czas akcji; Opis akcji (sukces lub błąd zapisu/logowania/dodania użytkownika...)

2. W rolę administratora program musi zawierać następującefunkcje:
- Hasło  jednorazowe(przy  tworzeniu  nowego  użytkownika  bądź  przy  edycji  już istniejącego, administrator ma do wyboru wygenerowanie hasła jednorazowego według zadania indywidualnego oraz funkcji jednokierunkowej).

![image](https://user-images.githubusercontent.com/95255764/200166983-ed62b1bc-6ceb-4ea9-9707-fed91dac971a.png)

![image](https://user-images.githubusercontent.com/95255764/200166989-d66289c4-f45e-47e3-89ea-5bedda836e34.png)

- Sprawdzenia   logów   aktywności wszystkich  użytkowników  korzystających  z programu.

![image](https://user-images.githubusercontent.com/95255764/200167002-5fc217d9-6c77-4dc0-9696-a63e674f1842.png)


- Limit  błędnych  logowań(możliwość ustalenia limitu błędnych logowań, po których użytkownik zostanie zablokowany i nie będzie mógł się zalogować przez okres 15 minut do programu).

![image](https://user-images.githubusercontent.com/95255764/200167010-648840f8-3204-4731-920a-117729fa9f14.png)


- Czas sesji użytkownika(minimalny czas nieaktywności, po którym użytkownik zostanie wylogowany z programu).
![image](https://user-images.githubusercontent.com/95255764/200167016-2ea6a80b-0321-432b-b7b0-f10e0be977bb.png)



## Zadania indywidualne ##
![image](https://user-images.githubusercontent.com/95255764/200166753-36e4d1af-0243-4df3-859e-a4d60e327187.png)





## ---------------------------------------------------------- ##

## Zadanie do zrealizowania - I ##

### I. Napisz program, który implementuje następujące zasady dla systemu bezpieczeństwa: ###

> OK 1. Program powinien zapewniać pracę w dwóch rolach: administratora (użytkownik o stałej nazwie ADMIN) oraz zwykłego użytkownika. 

2. W rolę administratora program musi zawierać następującefunkcje:

> OK - zmienianie  hasła dla  konta  administratora(jeśli  stare  hasło  zostało  wprowadzone poprawnie);

> OK - modyfikować szczegóły konta, np. pełną nazwę użytkownika czy hasło;

> OK - dodawać nowych użytkowników.

> OK - przeglądać listęużytkowników.

> OK - blokować konta użytkowników oraz blokować ograniczenie wybranych haseł;

> OK - usuwać konta użytkowników;

> OK - włączyć /wyłączyć  ograniczenia  haseł  wybranych  przez  użytkownika  (zgodnie  z zadaniemindywidualnym). Rys.1–przykładprototypowania.;

> OK - ustawić ważność  hasła  użytkownika.  Po wygaśnięcia  hasła  (np. po  upływie  dni ustalonych),  użytkownik  podaje  nowe  hasło,  które  musi  różnić  się  od  wszystkich poprzednich.Rys.1–przykładprototypowania.

> OK - zakończenie pracy z programem.;

> OK 3. W roli użytkownika program powinien zawierać tylko funkcje zmiany hasła użytkownika (jeśli stare hasło jest wpisane poprawnie) i zakończenia pracy. 

> OK 4. Po uruchomieniu program powinien poprosić użytkownika podać swój identyfikator i hasło do kontaw specjalnym oknie logowania. Wprowadzając hasło, jego znaki należy zawsze zastąpić wyświetlanym na ekranie symbolem ”*”.

> OK 5. Komunikat w przypadku wprowadzenienie popranego identyfikatoralub hasła: „Login lub Hasło niepoprawny”.

> OK 6. Przy  pierwszym  logowaniu  system powinien prosić o zmianę hasła dostępu ustalonego przez  administratora  na  hasło  własne,  znane  tylko  użytkownikowi,  utworzone  według zadaniaindywidualnego.Nowe hasło należy podać oraz powtórzyć.

> OK 7. Należystosować bezpieczny algorytm hashujący do przechowywania haseł.

### II. Zadania indywidualne ###

Ograniczenia dotyczące haseł użytkowników:
 
 > OK  - Hasło musi zawierać co najmniej 8 znaków,  co najmniej jedną wielką literę, co najmniej jeden znak specjalny;

