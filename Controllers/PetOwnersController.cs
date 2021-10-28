using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetAll() 
        {
            return _context.PetOwnerTable;
            // return new List<PetOwner>();
        }

        // get by id
        [HttpGet("{id}")]
        public PetOwner GetById(int id)
        {
            return _context.PetOwnerTable
                    .SingleOrDefault(petOwner => petOwner.id == id);

        }
        [HttpPost]
        public PetOwner Post(PetOwner petOwner) 
        {
            _context.Add(petOwner);
            _context.SaveChanges();
            return petOwner;
        }
    
        // put route /api/PetOwners/:id
        [HttpPut("{id}")]
        public PetOwner Put(int id, PetOwner petOwner)
        {
            // DB context needs to know the id of the pet owner to update
            petOwner.id = id;

            // Tell the DB context about the updated pet owner object
            _context.Update(petOwner);

            // and save the pet owner object to the DB
            _context.SaveChanges();

            // respond back with the created pet owner object            
            return petOwner;

        }
        

        // delete route
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Find the petowner by ID
            PetOwner petowner = _context.PetOwnerTable.Find(id);
            // Tell the DB we want to remove this pet owner
            _context.PetOwnerTable.Remove(petowner);
            // Save changes to the database
            _context.SaveChanges();
        }

    }

}
