using System;
using System.Collections.Generic;

namespace CrudProduto.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }

        public int IdCategoria { get; set; }
        public string? Categoria1 { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
