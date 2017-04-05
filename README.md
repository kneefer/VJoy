EgzotechVJoy
============

### OPIS PROJEKTÓW SOLUCJI: 
- VJoyTCPService - aplikacja WCF, stawiana na localhoście na porcie 8529
- EgzotechVJoyService - usługa systemu Windows, która jest wrapperem powyższego webservice'u. Instalowana przez pliki *.bat, generowane w katalogach wyjściowych (nie wiem, czy we wszystkich systemach .NET ma odpowiednie ścieżki, jakie umieściłem w bacie).
- VJoyTestFeeder - testowy feeder

### SPOSÓB INSTALACJI (dla x64 - dla x86 tożsamo):
- uruchomić z prawami administratora plik \EgzotechVJoy\EgzotechVJoyService\bin\x64\install_x64.bat
- usługa powinna startować automatycznie ze startem systemu, żeby uniknąć ponownego uruchomienia, wejdź w Menadżer Zadań -> Usługi. Znajdź usługę "EgzotechVJoyService" naciśnij prawym i kliknij "Rozpocznij"

### INFORMACJE DOTYCZĄCE KOMPILACJI:
- Przed kompilacją trzeba wybrać jedną z konfiguracji x64/x86 (domyślnie VS daje na starcie Any CPU).
- Kompilacja w katalogu \EgzotechVJoy\EgzotechVJoyService\bin\[x86/x64] tworzy wszystkie, potrzebne do instalacji i deinstalacji pliki

### INFORMACJE DOTYCZĄCE UŻYWANIA:
- Feeder, jeśli połączył się prawidłowo, powinien wyświetlać ID joysticka, do którego się podłączył. Joysticki o ID=5 i ID=6 (są problemy z joystickami o niskich numerach ID) MUSZĄ istnieć w systemie przed uruchomieniem serwera. Tworzone są za pomocą vJoyConfig z opcjami domyślnymi.
- Feeder nie utrzymuje połączenia. Nie dodano jeszcze mechanizmu pingowania.
- Jeśli joystick nie zostanie odłączony przez Feeder odpowiednio (brak pingowania) to nie będzie on dostępny dla innych Feederów (pozostanie oznaczony jako "zajęty") i zwolnić go można wtedy tylko i wyłącznie restartując usługę.
