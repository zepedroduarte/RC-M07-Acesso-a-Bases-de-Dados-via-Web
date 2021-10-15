using System.Collections.Generic;
using System.Data;

namespace SantaShop.ConsV2.Interfaces
{
    public interface ISantaShopService
    {
        public IDbConnection DbConnection{ get; set; }
        public IEnumerable<T> PesquisarTodos<T>();
        public T Pesquisar<T>(long ID) where T : class;
        public IEnumerable<T> Pesquisar<T>(string Campo, string Criterio);
        public long Inserir<T>(T Item) where T : class;
        public DeletedStatusEnum Eliminar<T>(long ID) where T : class;
        public bool Actualizar<T>(T item) where T : class;

    }
}
