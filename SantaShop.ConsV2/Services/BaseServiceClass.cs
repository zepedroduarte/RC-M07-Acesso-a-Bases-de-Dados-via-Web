using Dapper;
using Dapper.Contrib.Extensions;

using SantaShop.ConsV2.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;

namespace SantaShop.ConsV2.Services
{
    public class BaseServiceClass : ISantaShopService
    {

        public string BaseSelect { get; set; }
        public IDbConnection DbConnection { get; set; }

        public BaseServiceClass() {}

        public BaseServiceClass(IDbConnection dbConnection)
        {
            DbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }


        public IEnumerable<T> PesquisarTodos<T>()
        {
            return DbConnection.Query<T>(BaseSelect);
        }

        public IEnumerable<T> Pesquisar<T>(string Campo, string Criterio)
        {

            var queryParam = new
            {
                CriterioPesquisa = $"%{Criterio}%"
            };


            string sql = $"{BaseSelect} where {Campo} like @CriterioPesquisa";

            return DbConnection.Query<T>(sql, queryParam);
        }

        public T Pesquisar<T>(long ID) where T : class
        {
            return DbConnection.Get<T>(ID);
        }

        public DeletedStatusEnum Eliminar<T>(long ID) where T : class
        {

            T item = DbConnection.Get<T>(ID);

            if (item != null)
            {
                if (DbConnection.Delete<T>(item))
                    return DeletedStatusEnum.Deleted;

                return DeletedStatusEnum.NotDeleted;
            }

            return DeletedStatusEnum.NotFound;

        }

        public long Inserir<T>(T Item) where T : class
        {
            return DbConnection.Insert<T>(Item);
        }


        public bool Actualizar<T>(T Item) where T : class
        {
            return DbConnection.Update<T>(Item);
        }
    }
}
