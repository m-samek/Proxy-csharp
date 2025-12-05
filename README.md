# Proxy Design Patterns – C# Solution

Repozytorium prezentuje trzy przykłady wykorzystania wzorca projektowego **Proxy (Pełnomocnik)** w C#.  
Solution nazywa się **Proxy** i zawiera trzy osobne projekty:

- Virtual`Proxy`
- `RemoteProxy`
- `ProtectionProxy`

Każdy projekt ilustruje inny typ Proxy i osobny scenariusz użycia.

---

## Struktura rozwiązania
```
Proxy.sln
├─ VirtualProxy
│ └─ Program.cs // Virtual Proxy z opóźnionym ładowaniem obrazu
├─ RemoteProxy
│ └─ Program.cs // Remote Proxy z symulacją serwisu zewnętrznego i cache
└─ ProtectionProxy
└─ Program.cs // Protection Proxy z kontrolą dostępu do dokumentu
```

---

## VirtualProxy

**Cel:** Opóźnione (leniwe) tworzenie kosztownego obiektu.

Projekt Virtual`Proxy` zawiera:

- `IImage` – interfejs z metodą `Display()`.
- `RealImage` – klasa reprezentująca „ciężki” obraz, który ładuje się w konstruktorze i wypisuje komunikat `Loading image <filename>`.
- `ImageProxy` – proxy, które przechowuje nazwę pliku i tworzy `RealImage` dopiero przy pierwszym wywołaniu `Display()`; kolejne wywołania korzystają z już załadowanego obiektu.

**Uruchomienie:**  
Po uruchomieniu projektu w `Main` tworzony jest `ImageProxy`, a obraz ładuje się dopiero, gdy zostanie wywołane `Display()`.

---

## RemoteProxy

**Cel:** Symulacja dostępu do zewnętrznego serwisu oraz cache’owanie odpowiedzi.

Projekt `RemoteProxy` zawiera:

- `IRemoteService` – interfejs z metodą `GetData()`.
- `RealRemoteService` – „prawdziwy” serwis zdalny, który w konstruktorze wyświetla `Connecting to remote service...` oraz w `GetData()` np. `Fetching data from remote service...`.
- `RemoteServiceProxy` – proxy, które:
    - przy pierwszym wywołaniu `GetData()` tworzy `RealRemoteService` i pobiera dane,
    - zapisuje wynik w prywatnym polu cache,
    - przy kolejnych wywołaniach zwraca dane z cache, wypisując np. `Returning data from cache...`.

**Uruchomienie:**  
W `Main` tworzony jest `RemoteServiceProxy`, a wielokrotne wywołania `GetData()` pokazują różnicę między pierwszym połączeniem a kolejnymi odczytami z cache.

---

## ProtectionProxy

**Cel:** Kontrola dostępu do chronionego zasobu na podstawie uprawnień użytkownika.

Projekt `ProtectionProxy` zawiera:

- `User` – klasa z właściwością `HasAccess` (informacja, czy użytkownik ma uprawnienia).
- `IDocument` – interfejs z metodą `ReadContent()`.
- `SecureDocument` – implementacja `IDocument`, która symuluje odczyt dokumentu, wyświetlając `Reading secure content...`.
- `AccessProxy` – proxy implementujące `IDocument`, które:
    - w konstruktorze przyjmuje obiekt `User`,
    - w `ReadContent()` sprawdza `HasAccess`,
    - jeśli użytkownik ma dostęp, deleguje wywołanie do `SecureDocument`,
    - w przeciwnym wypadku wypisuje komunikat typu `Access denied.`.

**Uruchomienie:**  
W `Main` tworzeni są użytkownicy z i bez dostępu, a następnie obiekty `AccessProxy`; wywołania `ReadContent()` pokazują, że tylko użytkownik z uprawnieniami może czytać zawartość.

---

## Jak uruchomić

1. Otwórz solution `Proxy.sln` w Visual Studio / Rider.
2. Ustaw jako projekt startowy:
    - Virtual`Proxy`, albo
    - `RemoteProxy`, albo
    - `ProtectionProxy`.
3. Uruchom aplikację (`Run` / `Start Debugging`), aby zobaczyć zachowanie danego typu Proxy w konsoli.

Możesz modyfikować `Main` w każdym projekcie, aby testować inne scenariusze, np. wielokrotne wywołania metod, różne wartości uprawnień użytkownika czy nazwy plików.