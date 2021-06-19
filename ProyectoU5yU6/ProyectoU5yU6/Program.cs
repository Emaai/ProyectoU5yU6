using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace ProyectoU5yU6
{
    public partial class Program
    {
        enum Estados
        {
            Respuesta, Normal
        }

        static void Main(string[] args)
        {
            void AgregarComentarios(List<Comentario> comments)
            {
                try
                {
                    if (comments == null)
                    {

                    }
                    else
                    {
                        foreach (var comment in comments)
                        {
                            Console.WriteLine();

                            // Se imprime el id de la publicacion
                            Console.WriteLine("ID: " + comment.id);
                            
                            // Se imprime el autor
                            Console.WriteLine("Autor: " + comment.autor);

                            // Se imprime el comentario
                            Console.WriteLine("-------------------------------");
                            Console.WriteLine();
                            Console.WriteLine(comment.comentario);
                            Console.WriteLine();
                            Console.WriteLine("-------------------------------");

                            // Se imprime el numero de likes
                            Console.WriteLine("Likes: " + comment.likes);

                            // Se imprime la fecha
                            Console.WriteLine("Fecha: " + comment.fecha_publi);

                            Console.WriteLine();
                            Console.WriteLine();
                        }
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Error al mostrar comentario: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al mostrar comentario: " + e.Message);
                }
            }
            List<Comentario> Reescribe()
            {
                List<Comentario> comments;
                List<Comentario> inapropiate;
                try
                {
                    comments = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                    inapropiate = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt");
                    if (inapropiate.Count == 0 || inapropiate.FindLast(x => x == null) == null)
                    {
                        if (inapropiate.Count != 0)
                        {
                            inapropiate.RemoveAll(x => x == null);
                        }
                        if (inapropiate.Count == 0)
                        {
                            for (int i = 0; i <= comments.Count - 1; i++)
                            {
                                if (Filtro.ChecarComentario(comments[i].comentario))
                                {
                                    inapropiate.Add(comments[i]);
                                    ComentariosDB.SaveToFile(inapropiate[inapropiate.Count - 1], System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt", true);
                                }
                            }
                        }
                    }
                    int conteo = inapropiate.Count;
                    for (int i = 0; i <= comments.Count - 1; i++)
                    {

                        for (int j = 0; j <= inapropiate.Count - 1; j++)
                        {
                            if (Filtro.ChecarComentario(comments[i].comentario) && inapropiate.FindIndex(x => x.id == comments[i].id) == -1)
                            {
                                inapropiate.Add(comments[i]);
                            }
                            comments.RemoveAll(x => x == null);
                            comments.RemoveAll(x => x.id == inapropiate[j].id);
                        }
                    }
                    if (conteo != inapropiate.Count) //Evita que se guarde nuevamente los mismos comentarios
                    {
                        List<Comentario> aux = new List<Comentario>();
                        Comentario auxiliar = new Comentario();
                        List<Comentario> aux2 = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                        try
                        {
                            for (int i = conteo; i <= inapropiate.Count - 1; i++)
                            {
                                auxiliar = aux2.Find(x => x.id == inapropiate[i].id);
                                aux.Add(auxiliar);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {

                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                        catch (ArgumentException)
                        {

                        }
                        catch (Exception)
                        {

                        }
                        ComentariosDB.SaveToFile(aux, System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt", true);
                    }
                    else
                    {

                    }
                    comments.Sort();
                    return comments;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Error al cargar comentarios: " + e.Message + e.StackTrace);
                    return comments = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al cargar comentarios: " + e.Message + e.StackTrace);
                    return comments = null;
                }
            }

            string Estado;
            string auxautor = "";
            int id = 0;
            Filtro.CargarDiccionario(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Filtro.txt");
            Estado = Estados.Normal.ToString();
            string publi = "";
            bool salir = false;

            do
            {
                bool subsalir = false;
                Console.Clear();
                Console.WriteLine("Inserta tu nombre");
                string nombre = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Que es lo que desea hacer? \r\n " +
                    "1. Ver publicaciones \r\n " +
                    "2. Publicar algo \r\n " +
                    "3. Ver malas palabras \r\n " +
                    "4. Agregar mala palabra \r\n " +
                    "5. Salir");
                Console.WriteLine();
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1: Console.WriteLine("\r\n"); // Si pulsa el numero 1: Ver publicaciones
                        
                        do
                        {
                            AgregarComentarios(Reescribe());
                            Console.WriteLine();
                            Console.WriteLine("Acciones: \r\n" +
                                "1. Responder \r\n" +
                                "2. Dar Like \r\n" +
                                "3. Marcar como inapropiado \r\n" +
                                "4. Salir");
                            Console.WriteLine();
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.D1:
                                    Console.WriteLine(); // Responder

                                    Console.WriteLine();
                                    Console.WriteLine("ID de la publicacion: ");
                                    id = int.Parse(Console.ReadLine());
                                    Console.WriteLine();
                                    Console.WriteLine("Respuesta: ");
                                    string resp = Console.ReadLine();
                                    try
                                    {
                                        PublicarComentario(nombre + " en respuesta a " + Convert.ToString(id), resp);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;

                                case ConsoleKey.D2:
                                    Console.WriteLine(); // Dar Like

                                    Console.WriteLine();
                                    Console.WriteLine("Id de la publicacion: ");
                                    id = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        Comentario like = ComentariosDB.GetComment(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", id);
                                        List<Comentario> comentarios = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                                        int h = comentarios.FindIndex(x => x.id == like.id);
                                        like.likes = like.likes + 1;
                                        comentarios[h] = like;
                                        ComentariosDB.ChangeALine(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", comentarios);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;

                                case ConsoleKey.D3:
                                    Console.WriteLine();

                                    Console.WriteLine();
                                    Console.WriteLine("Id de la publicacion: ");
                                    id = int.Parse(Console.ReadLine());

                                    try
                                    {
                                        Comentario inap = ComentariosDB.GetComment(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", id);
                                        List<Comentario> comentarios = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");
                                        int h = comentarios.FindIndex(x => x.id == inap.id);
                                        inap.inapropiado = inap.inapropiado + 1;
                                        comentarios[h] = inap;
                                        if (comentarios[h].inapropiado > 10)
                                        {
                                            List<Comentario> inapropiates = ComentariosDB.ReadFromFile(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt");
                                            inapropiates.Add(comentarios[h]);
                                            inapropiates.Sort();
                                            ComentariosDB.SaveToFile(inapropiates, System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt", true);
                                        }
                                        ComentariosDB.ChangeALine(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", comentarios);
                                    }
                                    catch (FormatException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    break;

                                case ConsoleKey.D4: subsalir = true;
                                    break;
                            }
                            Console.Clear();
                        } while (subsalir == false);

                        break;

                    case ConsoleKey.D2: Console.WriteLine("\r\n"); // Si pulsa el numero 2: Publicar algo

                        Console.WriteLine("Escriba lo que quiere publicar: ");
                        publi = Console.ReadLine();
                        PublicarComentario(nombre, publi); //Publica el comentario
                        Console.WriteLine("Publicado! Pulse cualquier tecla para continuar");
                        Console.ReadKey();

                        break;

                    case ConsoleKey.D3: Console.WriteLine("\r\n"); // Si pulsa el numero 3: Ver malas palabras

                        Console.WriteLine("----------------------");
                        Console.WriteLine();

                        foreach (var filter in Filtro.filtro)
                        {
                            Console.WriteLine(filter.Key + ". " + filter.Value + "\r\n");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Pulse cualquier boton para salir");
                        Console.ReadKey();

                        break;

                    case ConsoleKey.D4: Console.WriteLine(); // Agregar mala palabra

                        Console.WriteLine("Escriba la nueva mala palabra: ");
                        string malo = Console.ReadLine();
                        Filtro.AñadirMalaPalabra(malo);
                        Console.WriteLine();
                        Console.WriteLine("Palabra agregada. Pulse cualquier tecla para salir");
                        Console.ReadKey();

                        break;

                    case ConsoleKey.D5: Console.WriteLine("\r\n"); // Si pulsa el numero 5: Salir

                        Console.WriteLine("Has salido del programa.");
                        salir = true;
                        Console.WriteLine();

                        break;
                }

                
            } while (salir == false);

            void PublicarComentario(string nombre, string texto)
            {
                try
                {
                    if (Filtro.ChecarComentario(texto)) //Checa que no contenga ninguna mala palabra
                    {

                        string date = DateTime.Today.ToShortDateString(); //Se obtiene la fecha de publicacion
                        int last = ComentariosDB.GetLastID(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt"); //Consigue el ultimo comentario publicado
                        Comentario c = new Comentario(last + 1, nombre, date, texto, "198.192.0.0", 0, 0);//Se define el comentario
                        Console.WriteLine("Tu mensaje no pudo ser publicado debido a que contiene palabras obscenas.");
                        ComentariosDB.SaveToFile(c, System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", false);//Guardado en archivo
                        ComentariosDB.SaveToFile(c, System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\ComentariosInapropiados.txt", true); //Guardado en archivo
                        if (Estado == Estados.Respuesta.ToString())
                        {
                            Estado = Estados.Normal.ToString();
                            nombre = auxautor;
                        }
                    }
                    else
                    {

                        string date = DateTime.Today.ToShortDateString();//Se obtiene la fecha de publicacion
                        int last = ComentariosDB.GetLastID(System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt");//Consigue el ultimo comentario publicado
                        Comentario c = new Comentario(last + 1, nombre, date, texto, "198.192.0.0", 0, 0);//Se define el comentario
                        ComentariosDB.SaveToFile(c, System.IO.Directory.GetCurrentDirectory() + @"\BaseDatos\Comentarios.txt", false);//Guardado en archivo
                        if (Estado == Estados.Respuesta.ToString())
                        {
                            Estado = Estados.Normal.ToString();
                            nombre = auxautor;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
