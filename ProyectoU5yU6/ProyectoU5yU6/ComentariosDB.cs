using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ProyectoU5yU6
{
    public class ComentariosDB
    {
        public static void SaveToFile(Comentario comment, string path, bool inapropiate)
        {
            StreamWriter textOut = null;
            try
            {
                if (File.Exists(path))
                {
                    if (!inapropiate)
                    {
                        List<Comentario> comments = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                        textOut = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Write));
                        // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                        foreach (var c in comments)
                        {
                            textOut.Write(c.id + "|");
                            textOut.Write(c.autor + "|");
                            textOut.Write(c.fecha_publi + "|");
                            textOut.Write(c.dir_ip + "|");
                            textOut.Write(c.comentario + "|");
                            textOut.Write(c.likes + "|");
                            textOut.WriteLine(c.inapropiado);
                        }
                        textOut.Write(comment.id + "|");
                        textOut.Write(comment.autor + "|");
                        textOut.Write(comment.fecha_publi + "|");
                        textOut.Write(comment.dir_ip + "|");
                        textOut.Write(comment.comentario + "|");
                        textOut.Write(comment.likes + "|");
                        textOut.WriteLine(comment.inapropiado);
                        textOut.Close();
                    }
                    else
                    {
                        List<Comentario> comments = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt");
                        textOut = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Write));
                        // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                        if (comments.Count != 0 && comments[0] != null)
                        {
                            foreach (var c in comments)
                            {
                                textOut.Write(c.id + "|");
                                textOut.Write(c.autor + "|");
                                textOut.Write(c.fecha_publi + "|");
                                textOut.Write(c.dir_ip + "|");
                                textOut.Write(c.comentario + "|");
                                textOut.Write(c.likes + "|");
                                textOut.WriteLine(c.inapropiado);
                            }
                        }
                        textOut.Write(comment.id + "|");
                        textOut.Write(comment.autor + "|");
                        textOut.Write(comment.fecha_publi + "|");
                        textOut.Write(comment.dir_ip + "|");
                        textOut.Write(comment.comentario + "|");
                        textOut.Write(comment.likes + "|");
                        textOut.WriteLine(comment.inapropiado);
                        textOut.Close();
                    }
                }
                else
                {
                    textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                    // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                    textOut.Write(comment.id + "|");
                    textOut.Write(comment.autor + "|");
                    textOut.Write(comment.fecha_publi + "|");
                    textOut.Write(comment.dir_ip + "|");
                    textOut.Write(comment.comentario + "|");
                    textOut.Write(comment.likes + "|");
                    textOut.WriteLine(comment.inapropiado);
                    textOut.Close();
                }


            }

            catch (IOException)
            {
                Console.WriteLine("Ya existe el archivo");
            }

            catch (Exception)
            {
                Console.WriteLine("Error");
            }

            finally
            {
                if (textOut != null)
                    textOut.Close();
            }


        }
        public static void SaveToFile(List<Comentario> commentaries, string path, bool inapropiate)
        {
            StreamWriter textOut = null;
            try
            {
                if (File.Exists(path))
                {
                    if (!inapropiate)
                    {
                        List<Comentario> comments = ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                        textOut = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Write));
                        // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                        foreach (var c in comments)
                        {
                            textOut.Write(c.id + "|");
                            textOut.Write(c.autor + "|");
                            textOut.Write(c.fecha_publi + "|");
                            textOut.Write(c.dir_ip + "|");
                            textOut.Write(c.comentario + "|");
                            textOut.Write(c.likes + "|");
                            textOut.WriteLine(c.inapropiado);
                        }
                        foreach (var comment in commentaries)
                        {
                            textOut.Write(comment.id + "|");
                            textOut.Write(comment.autor + "|");
                            textOut.Write(comment.fecha_publi + "|");
                            textOut.Write(comment.dir_ip + "|");
                            textOut.Write(comment.comentario + "|");
                            textOut.Write(comment.likes + "|");
                            textOut.WriteLine(comment.inapropiado);
                        }
                        textOut.Close();
                    }
                    else
                    {
                        List<Comentario> comentarios = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt");
                        textOut = new StreamWriter(new FileStream(path, FileMode.Open, FileAccess.Write)); // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                        if (comentarios.Count != 0 || comentarios.Count != 0 && comentarios[0] != null)
                        {
                            comentarios.RemoveAll(x => x == null);
                            foreach (var c in comentarios)
                            {
                                textOut.Write(c.id + "|");
                                textOut.Write(c.autor + "|");
                                textOut.Write(c.fecha_publi + "|");
                                textOut.Write(c.dir_ip + "|");
                                textOut.Write(c.comentario + "|");
                                textOut.Write(c.likes + "|");
                                textOut.WriteLine(c.inapropiado);
                            }
                        }
                        else
                        {
                            if (comentarios.Count != 0)
                            {
                                comentarios.RemoveAt(0);
                            }
                        }
                        foreach (var comment in commentaries)
                        {
                            textOut.Write(comment.id + "|");
                            textOut.Write(comment.autor + "|");
                            textOut.Write(comment.fecha_publi + "|");
                            textOut.Write(comment.dir_ip + "|");
                            textOut.Write(comment.comentario + "|");
                            textOut.Write(comment.likes + "|");
                            textOut.WriteLine(comment.inapropiado);
                        }
                        textOut.Close();
                    }
                }
                else
                {
                    textOut = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                    // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                    foreach (var comment in commentaries)
                    {
                        textOut.Write(comment.id + "|");
                        textOut.Write(comment.autor + "|");
                        textOut.Write(comment.fecha_publi + "|");
                        textOut.Write(comment.dir_ip + "|");
                        textOut.Write(comment.comentario + "|");
                        textOut.Write(comment.likes + "|");
                        textOut.WriteLine(comment.inapropiado);
                    }
                    textOut.Close();
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

            finally
            {
                if (textOut != null)
                    textOut.Close();
            }


        }
        public static List<Comentario> ReadFromFile(string path)
        {
            List<Comentario> comments = new List<Comentario>();
            StreamReader textIn = null;
            try
            {
                if (File.Exists(path))
                {
                    textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
                    while (textIn.Peek() != -1)    // Leer hasta el final
                    {
                        string row = textIn.ReadLine();  // row =  "1000|Prueba|13/Junio/2021|198.192.0.1|Esto es una prueba|3|0"
                        if (row != "")
                        {
                            string[] columns = row.Split('|'); // ["1000","Prueba","13/Junio/2021","Esto es una prueba","3","0"]; 
                            Comentario c = new Comentario();
                            c.id = int.Parse(columns[0]);
                            c.autor = columns[1];
                            c.fecha_publi = columns[2];
                            c.dir_ip = columns[3];
                            c.comentario = columns[4];
                            c.likes = int.Parse(columns[5]);
                            c.inapropiado = int.Parse(columns[6]);
                            comments.Add(c);
                        }
                        else
                        {
                            comments.Add(null);
                        }
                    }
                    textIn.Close();
                    return comments;
                }
                else
                {

                    textIn = new StreamReader(new FileStream(path, FileMode.Create));
                    textIn.Close();
                    return comments;
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
            return comments;
        }
        public static int GetLastID(string path)
        {
            try
            {
                List<Comentario> comments = ReadFromFile(path);


                var filtro_comentario = from c in comments
                                        select c;
                return filtro_comentario.Last().id;
            }
            catch (ArgumentException e)
            {
                return 999;
            }
            catch (Exception e)
            {
                return 999;
            }
        }
        public static Comentario GetComment(string path, int id)
        {
            try
            {
                List<Comentario> comments = ReadFromFile(path);


                var filtro_comentario = from c in comments
                                        where c.id == id
                                        select c;
                return filtro_comentario.First();
            }
            catch (ArgumentException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static void ChangeALine(string path, List<Comentario> comments)
        {
            StreamWriter textOut = null;
            try
            {
                if (File.Exists(path))
                {
                    if (comments != null)
                    {
                        File.Delete(path);
                        textOut = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write));

                        foreach (var comment in comments)
                        {
                            textOut.Write(comment.id + "|");
                            textOut.Write(comment.autor + "|");
                            textOut.Write(comment.fecha_publi + "|");
                            textOut.Write(comment.dir_ip + "|");
                            textOut.Write(comment.comentario + "|");
                            textOut.Write(comment.likes + "|");
                            textOut.WriteLine(comment.inapropiado);
                        }
                        textOut.Close();

                    }
                    else
                    {
                        Console.WriteLine("La lista mandada como parametro esta definida como null, por lo que no se realizo ningun cambio en el documento");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (textOut != null)
                {
                    textOut.Close();
                }
            }
        }
    }
}
