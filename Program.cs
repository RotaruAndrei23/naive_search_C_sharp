using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics; 
using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;

namespace WordSearchUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Introdu calea folderului/partiției (ex: C:\\TestFolder): ");
            string rootPath = Console.ReadLine();

            Console.Write("Introdu cuvântul pe care vrei să îl cauți: ");
            string searchWord = Console.ReadLine();

            if (Directory.Exists(rootPath) && !string.IsNullOrEmpty(searchWord))
            {
                Console.WriteLine($"\nÎncep căutarea pentru '{searchWord}' în {rootPath}...\n");
                
                Stopwatch timer = Stopwatch.StartNew(); 

                TraverseAndSearch(rootPath, searchWord);
                
                timer.Stop(); 

                Console.WriteLine("\nCăutare finalizată.");
                Console.WriteLine($"Timp total de execuție: {timer.Elapsed.TotalSeconds:F2} secunde."); 
            }
            else
            {
                Console.WriteLine("Calea specificată nu există sau cuvântul este invalid.");
            }
            
            Console.ReadLine();
        }

        static void TraverseAndSearch(string directory, string word)
        {
            try
            {
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    ProcessFile(file, word);
                }

                string[] subDirectories = Directory.GetDirectories(directory);
                foreach (string subDir in subDirectories)
                {
                    TraverseAndSearch(subDir, word);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Ignore directories we don't have access to
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la accesarea {directory}: {ex.Message}");
            }
        }

        static void ProcessFile(string filePath, string word)
        {
            try
            {
                string extension = Path.GetExtension(filePath).ToLower();
                string content = string.Empty;

                if (extension == ".txt" || extension == ".html" || extension == ".xml" || extension == ".json")
                {
                    content = File.ReadAllText(filePath);
                }
                else if (extension == ".pdf")
                {
                    content = ExtractTextFromPdf(filePath);
                }
                else if (extension == ".docx")
                {
                    content = ExtractTextFromDocx(filePath);
                }

                if (!string.IsNullOrEmpty(content))
                {
                    List<int> occurrences = NaiveStringSearch(content, word);
                    
                    if (occurrences.Count > 0)
                    {
                        Console.WriteLine($"[GĂSIT] Fișier: {filePath} -> {occurrences.Count} apariții.");
                    }
                }
            }
            catch (IOException)
            {
                // Ignore IO errors (e.g., file in use)
            }
            catch (Exception)
            {
                // Ignore other exceptions for robustness
            }
        }

        static string ExtractTextFromPdf(string filePath)
        {
            try
            {
                using (PdfDocument document = PdfDocument.Open(filePath))
                {
                    string text = "";
                    foreach (var page in document.GetPages())
                    {
                        text += page.Text + " ";
                    }
                    return text;
                }
            }
            catch
            {
                return string.Empty; 
            }
        }

        static string ExtractTextFromDocx(string filePath)
        {
            try
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false))
                {
                    var body = wordDocument.MainDocumentPart.Document.Body;
                    return body.InnerText;
                }
            }
            catch
            {
                return string.Empty; 
            }
        }

        static List<int> NaiveStringSearch(string text, string pattern)
        {
            List<int> occurrences = new List<int>();
            int n = text.Length;
            int m = pattern.Length;

            for (int i = 0; i <= n - m; i++)
            {
                int j;
                for (j = 0; j < m; j++)
                {
                    if (char.ToLower(text[i + j]) != char.ToLower(pattern[j]))
                    {
                        break; 
                    }
                }

                if (j == m)
                {
                    occurrences.Add(i);
                }
            }

            return occurrences;
        }
    }
}