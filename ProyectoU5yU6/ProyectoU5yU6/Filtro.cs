using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ProyectoU5yU6
{
    public static class Filtro
    {
        public static Dictionary<string, string> filtro = new Dictionary<string, string>();
        public static int Contador = 0;

        public static bool ChecarComentario(string texto)
        {
            for (int i = 0; i <= filtro.Count - 1; i++)
            {
                if (texto.ToLower().Contains(filtro[i.ToString()].ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        public static void AñadirMalaPalabra(string palabra)
        {
            StreamWriter textOut = null;
            try
            {
                List<string> words = LeerFiltro(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Filtro.txt");
                textOut = new StreamWriter(new FileStream(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Filtro.txt", FileMode.Open, FileAccess.Write));
                foreach (var c in words)
                {
                    textOut.WriteLine(c);
                }
                textOut.WriteLine(palabra);
                textOut.Close();
                filtro.Clear();
                Contador = 0;
                CargarDiccionario(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Filtro.txt");
                Console.WriteLine("Palabra añadida con exito");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error en el formato del filtro" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar la palabra" + e.Message);
            }
            finally
            {
                if (textOut != null)
                    textOut.Close();
            }
        }
        static List<string> LeerFiltro(string path)
        {
            List<string> words = new List<string>();
            StreamReader textIn = null;
            try
            {
                if (File.Exists(path))
                {
                    textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                    while (textIn.Peek() != -1)    // Leer hasta el final
                    {
                        string row = textIn.ReadLine();
                        words.Add(row);
                    }

                    textIn.Close();

                    return words;
                }
                else
                {

                    textIn = new StreamReader(new FileStream(path, FileMode.Create));
                    textIn.Close();
                    return words;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (textIn != null)
            {
                textIn.Close();
            }
            return words;
        }
        public static void CargarDiccionario(string path)
        {
            StreamReader charge = null;
            try
            {
                if (filtro.Count != 0)
                {
                    Contador = 0;
                    filtro.Clear();
                }
                if (File.Exists(path))
                {
                    charge = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                    /// Palabra1
                    /// Palabra2
                    while (charge.Peek() != -1)    // Leer hasta el final
                    {
                        string row = charge.ReadLine();
                        filtro.Add(Contador++.ToString(), row);
                    }
                    charge.Close();
                }
                else
                {
                    File.Create(path);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error al cargar filtro" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al cargar filtro" + e.Message);
            }
            finally
            {
                if (charge != null)
                    charge.Close();
            }
        }
    }
}
