using System;
using System.Collections.Generic;

namespace CrudProduto.Models
{
    public partial class Fornecedore
    {
        public Fornecedore()
        {
            Produtos = new HashSet<Produto>();
        }

        public int IdFornecedores { get; set; }
        public string? Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
