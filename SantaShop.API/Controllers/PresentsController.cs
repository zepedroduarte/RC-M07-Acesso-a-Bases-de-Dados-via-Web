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
    public class PresentsController : ControllerBase
    {
        private readonly ISantaShopService _santaService;
        private readonly IConfiguration _configuration;
        public PresentsController(IEnumerable<ISantaShopService> santaServices, IConfiguration configuration)
        {
            _santaService = santaServices.SingleOrDefault(p => p.GetType().Name == typeof(PresentsService).Name);
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _santaService.DbConnection = new SqlConnection(configuration.GetConnectionString("SantaBD"));
        }


        // GET: api/<PresentsController>
        [HttpGet]
        public IEnumerable<Present> Get()
        {
            return _santaService.PesquisarTodos<Present>();
        }

        // GET api/<PresentsController>/5
        [HttpGet("{id}")]
        public Present Get(long id)
        {
            return _santaService.Pesquisar<Present>(id);
        }

        // POST api/<PresentsController>
        [HttpPost]
        public Present Post([FromBody] Present present)
        {
            long newID = _santaService.Inserir<Present>(present);

            if (newID > 0)
                return _santaService.Pesquisar<Present>(newID);

            return null;
        }

        // PUT api/<PresentsController>/5
        [HttpPut("{id}")]
        public Present Put(long id, [FromBody] Present present)
        {
            if (_santaService.Actualizar<Present>(present))
                return _santaService.Pesquisar<Present>(id);

            return null;
        }

        // DELETE api/<PresentsController>/5
        [HttpDelete("{id}")]
        public DeletedStatusEnum Delete(long id)
        {
            return _santaService.Eliminar<Present>(id);
        }
    }
}
