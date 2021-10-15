using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantaShop.Core.Interfaces;
using Microsoft.Data.SqlClient;
using SantaShop.Core.TablesModels;
using SantaShop.Core;
using Microsoft.Extensions.Configuration;
using SantaShop.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SantaShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BehaviorsController : ControllerBase
    {
        private readonly ISantaShopService _santaService;
        private readonly IConfiguration _configuration;

        public BehaviorsController(IEnumerable<ISantaShopService> santaServices, IConfiguration configuration)
        {
            _santaService = santaServices.SingleOrDefault(p => p.GetType().Name == typeof(BehaviorService).Name);
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _santaService.DbConnection = new SqlConnection(configuration.GetConnectionString("SantaBD"));
        }


        // GET: api/<BehaviorsController>
        [HttpGet]
        public IEnumerable<Behavior> Get()
        {
            return _santaService.PesquisarTodos<Behavior>();
        }

        // GET api/<BehaviorsController>/5
        [HttpGet("{id}")]
        public dynamic Get(long id)
        {
            return _santaService.Pesquisar<Behavior>(id);
        }

        // POST api/<BehaviorsController>
        [HttpPost]
        public Behavior Post([FromBody] Behavior behavior)
        {
            long newID = _santaService.Inserir<Behavior> (behavior);

            if (newID > 0)
                return _santaService.Pesquisar<Behavior>(newID);

            return null;
        }

        // PUT api/<BehaviorsController>/5
        [HttpPut("{id}")]
        public Behavior Put(long id, [FromBody] Behavior behavior)
        {
            if (_santaService.Actualizar<Behavior> (behavior))
                return _santaService.Pesquisar<Behavior>(id);

            return null;
        }

        // DELETE api/<BehaviorsController>/5
        [HttpDelete("{id}")]
        public DeletedStatusEnum Delete(long id)
        {
            return _santaService.Eliminar<Behavior>(id);
        }
    }
}
