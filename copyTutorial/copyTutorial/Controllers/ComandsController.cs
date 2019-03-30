using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using copyTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace copyTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandsController : ControllerBase
    {
        private readonly CommandContext _context;

        public ComandsController(CommandContext context) => _context = context;

        //Get : api/comands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        //Get : api/comands/n

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id) {
            var commandItem = _context.CommandItems.Find(id);

            if (commandItem == null)
            {
                return NotFound();
            }
            return commandItem;

        }

        //POST: api/comands

        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }


        //PUT: api/comands/n

        [HttpPut("{id}")]
        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id) {
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();

        }

        //DELETE: api/comands/n

        [HttpDelete("{id}")]

        public ActionResult<Command> DeleteCommand(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }

            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;
        }


    }
}
