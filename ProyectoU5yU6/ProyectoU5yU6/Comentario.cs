using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU5yU6
{
    public class Comentario : IComparable
    {
        public int id { get; set; }
        public string autor { get; set; }
        public string fecha_publi { get; set; }
        public string comentario { get; set; }
        public string dir_ip { get; set; }
        public int likes { get; set; }
        public int inapropiado { get; set; }

        public int CompareTo(object obj)
        {
            return this.id.CompareTo((obj as Comentario).id);
        }
        public override string ToString()
        {
            return string.Format($"{id},{autor},{fecha_publi},{comentario},{dir_ip},{inapropiado},{likes}");
        }
        public Comentario()
        {

        }

        public Comentario(int id, string autor, string fecha_publi, string comentario, string dir_ip, int likes, int inapropiado)
        {
            this.id = id;
            this.autor = autor;
            this.fecha_publi = fecha_publi;
            this.comentario = comentario;
            this.dir_ip = dir_ip;
            this.likes = likes;
            this.inapropiado = inapropiado;
        }
    }
}
