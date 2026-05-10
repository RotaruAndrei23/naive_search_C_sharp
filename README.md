# Utilitar de Căutare a Cuvintelor (Word Search Utility)

Acest proiect este o aplicație de tip consolă (CLI) dezvoltată în **C# (.NET 10)** pentru disciplina **Sisteme de Operare (SO)**. Utilitarul scanează recursiv o structură de directoare sau o partiție întreagă și numără aparițiile unui cuvânt specificat, utilizând **Algoritmul Naiv** de căutare a șirurilor de caractere.

## Caracteristici principale

* **Parcurgere recursivă:** Caută în toate subdirectoarele pornind de la o cale de bază. Include mecanisme de toleranță la erori care ignoră automat folderele cu acces restricționat (ex. fișiere de sistem) pentru a preveni blocajele.
* **Algoritmul Naiv:** Implementează algoritmul de tip *brute-force* pentru *string matching*, cu suport case-insensitive (ignoră majusculele/minusculele).
* **Suport multi-format:**
  * Fișiere plain-text: `.txt`, `.html`, `.xml`, `.json`
  * Documente complexe: `.pdf` și `.docx`
* **Profiling integrat:** Calculează și afișează timpul total de execuție al căutării.

## Tehnologii și Librării utilizate

* **Framework:** .NET 10
* **[UglyToad.PdfPig](https://github.com/UglyToad/PdfPig):** Pentru deconstrucția paginilor PDF și extragerea textului.
* **[DocumentFormat.OpenXml](https://www.nuget.org/packages/DocumentFormat.OpenXml):** Pentru accesarea arhivei și extragerea textului brut din documentele Microsoft Word moderne.

## Cerințe de sistem

Pentru a compila și rula acest proiect, este necesar să aveți instalat:
* **[.NET 10 SDK](https://dotnet.microsoft.com/download)**

## Cum se instalează și rulează

1. Clonați acest repository sau descărcați și dezarhivați codul sursă:
   ```bash
   git clone https://github.com/RotaruAndrei23/naive_search_C_sharp.git
2. Deschideți un terminal în folderul rădăcină al proiectului (unde se află fișierul .csproj).

3. Executați comanda:
    ```bash
    dotnet run
## Instrucțiuni de utilizare
După pornirea aplicației, urmați instrucțiunile din consolă:
1. Introduceți calea folderului: (ex. C:\Users\Nume\Desktop\FisiereTest)
2. Introduceți cuvântul: (ex. algoritm)

Aplicația va procesa fișierele și va afișa în timp real rezultatele, urmate de timpul total de execuție la final.