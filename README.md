# Binary RSA
### najwer23@live.com

# Własności i możliwośći 
- liczby binarne dodatnie i ujemne, dowolny zakres bitów (długość jest ograniczona przez wielkość liczby int)
- dodawanie, odejmowanie, mnożenie, dzielenie z resztą, operacje modulo (dzielenie) na całkowitych
- Binarna wersja NWD (binary GCD)
- Rozszerzony algorytm Euklidesa (Extended Euclidean algorithm)
- Szybkie potęgowanie modulo (Modular exponentiation)
- Test pseudo-pierwszości Millera-Rabina
- Liczba U2

# Algorytm RSA 

## Generowanie kluczy
1. Losuj 2 liczby 
2. Sprawdź czy są pierwsze // MR
3. P = liczba pierwsza
4. Q = liczba pierwsza
5. N = P * Q
6. Tocjetn = Phi = (P-1)(Q-1)
7. Zniszczyć P i Q
8. E = względnie pierwsza z Phi // GCD
9. D = E^-1 mod N // EEA
10. Klucz prywatny (E,N)
11. Klucz publiczny (D,N)

## Szyfrowanie

C = M^E mod N, M = wiadomość, C = szyfrogram

## Deszyfrowanie

M = C^D mod N

## Uwagi 

### Najstarszy bit wiadmości musi być na mniejszej pozycji, niż najstarszy bit N

- DOBRZE 

Wiadomość: 0000011110010101

N        : 0111110011010010

- ŹLE 

Wiadomość: 0110101010010101

N        : 0000000110101011

### Jak rozumieć długość liczby binarnej

Ustawiamy długość liczby binarnej na 100. 

Czyli 50 bitów na liczby dodatnie i 50 na ujemne. Ze względu, że mnożenie potrzebuje dwukrotność liczby.

Ostatecznie możemy używać liczb 24 bitowych + znak. Bezpiecznie użyć mniejszych