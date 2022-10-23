# Cybersecurity

## Zadanie do zrealiwoania ##

### I. Napisz program, który implementuje następujące zasady dla systemu bezpieczeństwa: ###

> OK 1. Program powinien zapewniać pracę w dwóch rolach: administratora (użytkownik o stałej nazwie ADMIN) oraz zwykłego użytkownika. 

2. W rolę administratora program musi zawierać następującefunkcje:

> OK - zmienianie  hasła dla  konta  administratora(jeśli  stare  hasło  zostało  wprowadzone poprawnie);

> OK - modyfikować szczegóły konta, np. pełną nazwę użytkownika czy hasło;

> OK - dodawać nowych użytkowników.

> OK - przeglądać listęużytkowników.

> OK - blokować konta użytkowników oraz 
-blokować ograniczenie wybranych haseł;

> OK - usuwać konta użytkowników;

 - włączyć /wyłączyć  ograniczenia  haseł  wybranych  przez  użytkownika  (zgodnie  z zadaniemindywidualnym). Rys.1–przykładprototypowania.;

 - ustawić ważność  hasła  użytkownika.  Po wygaśnięcia  hasła  (np. po  upływie  dni ustalonych),  użytkownik  podaje  nowe  hasło,  które  musi  różnić  się  od  wszystkich poprzednich.Rys.1–przykładprototypowania.

 - zakończenie pracy z programem.;

> OK 3. W roli użytkownika program powinien zawierać tylko funkcje zmiany hasła użytkownika (jeśli stare hasło jest wpisane poprawnie) i zakończenia pracy. 

> OK 4. Po uruchomieniu program powinien poprosić użytkownika podać swój identyfikator i hasło do kontaw specjalnym oknie logowania. Wprowadzając hasło, jego znaki należy zawsze zastąpić wyświetlanym na ekranie symbolem ”*”.

> OK 5. Komunikat w przypadku wprowadzenienie popranego identyfikatoralub hasła: „Login lub Hasło niepoprawny”.

6. Przy  pierwszym  logowaniu  system powinien prosić o zmianę hasła dostępu ustalonego przez  administratora  na  hasło  własne,  znane  tylko  użytkownikowi,  utworzone  według zadaniaindywidualnego.Nowe hasło należy podać oraz powtórzyć.

7. Należystosować bezpieczny algorytm hashujący do przechowywania haseł.

### II. Zadania indywidualne ###

Ograniczenia dotyczące haseł użytkowników:
 
 - Hasło musi zawierać co najmniej 8 znaków,  co najmniej jedną wielką literę, co najmniej jeden znak specjalny;

