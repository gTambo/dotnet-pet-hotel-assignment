using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetAll() {
            // return new List<Pet>();
            return _context.PetTable.Include(pet => pet.petOwner);
        }

        // Get by Id
        [HttpGet("{id}")]
        public Pet GetById(int id)
        {
            return _context.PetTable.Include(pet => pet.petOwner)
                    .SingleOrDefault(pet => pet.id == id);

        }
        // POST
        [HttpPost]
        public Pet Post(Pet pet) 
        {
            _context.Add(pet);
            _context.SaveChanges();
            return pet;
        }

        // PUT 
        [HttpPut("{id}")]
        public Pet Put(int id, Pet pet)
        {
            // DB context needs to know the id of the pet owner to update
            pet.id = id;

            // Tell the DB context about the updated pet owner object
            _context.Update(pet);
            // _context.Update(PetOwner.petCount++)

            // and save the pet owner object to the DB
            _context.SaveChanges();

            // respond back with the created pet owner object            
            return pet;

        }

           // delete route
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // Find the petowner by ID
            Pet pet = _context.PetTable.SingleOrDefault(pet => pet.id == id);
            // Tell the DB we want to remove this pet owner
            _context.PetTable.Remove(pet);
            // Save changes to the database
            _context.SaveChanges();
        }

    
    }
    

      
        
}


    // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }