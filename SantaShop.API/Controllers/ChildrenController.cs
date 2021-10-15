using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SantaShop.Core;
using SantaShop.Core.Interfaces;
using SantaShop.Core.Services;
using SantaShop.Core.TablesModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SantaShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController : ControllerBase
    {

        private readonly ISantaShopService _santaService;
        private readonly IConfiguration _configuration;

        public ChildrenController(IEnumerable<ISantaShopService> santaServices, IConfiguration configuration)
        {
            _santaService = santaServices.SingleOrDefault(p => p.GetType().Name == typeof(ChildrenService).Name);
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _santaService.DbConnection = new SqlConnection(configuration.GetConnectionString("SantaBD"));
        }


        // GET: api/<ChildrenController>
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            return _santaService.PesquisarTodos<dynamic>();
        }

        // GET api/<ChildrenController>/5
        [HttpGet("{id}")]
        public dynamic Get(long id)
        {
            return _santaService.Pesquisar<dynamic>(id);
        }

        // POST api/<ChildrenController>
        [HttpPost]
        public Child Post([FromBody] Child child)
        {
            long newID = _santaService.Inserir<Child>(child);

            if (newID > 0)
                return _santaService.Pesquisar<Child>(newID);

            return null;
        }

        // PUT api/<ChildrenController>/5
        [HttpPut("{id}")]
        public Child Put(long id, [FromBody] Child child)
        {
            if (_santaService.Actualizar<Child>(child))
                return _santaService.Pesquisar<Child>(id);

            return null;
        }

        // DELETE api/<ChildrenController>/5
        [HttpDelete("{id}")]
        public DeletedStatusEnum Delete(long id)
        {
            return _santaService.Eliminar<Child>(id);
        }
    }
}
