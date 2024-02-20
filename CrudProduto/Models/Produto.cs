using System;
using System.Collections.Generic;

namespace CrudProduto.Models
{
    public partial class Produto
    {
        public int IdProduto { get; set; }
        public string? Descricao { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdFornecedores { get; set; }

        public virtual Categoria? IdCategoriaNavigation { get; set; }
        public virtual Fornecedore? IdFornecedoresNavigation { get; set; }
    }
}
