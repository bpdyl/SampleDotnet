using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Component.DataAnnotation;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class CarController: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        //constructor for dependency injection
        public CarController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //viewing the list of cars from database
        public async Task<IActionResult> Index()
        {
            var car = await _dbContext.Cars.ToListAsync();
            return View(car);
        }

        //get method car add garni form dekhauna ko laagi
        public IActionResult AddCars()
        {
            return View();
        }
        //post method car add garna ko lagi to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCars(Cars c)
        {
            //check if the modelstate is valid
            if(ModelState.IsValid)
            {
                _dbContext.Cars.Add(c);
                await _dbContext.SaveChangesAsync();
                return RedirecToAction(nameof(Index));
            }
            return View(c);
        }

        //for editing the car details
        [HttpGet]
        //get method for fetching the car for the particular id
        public async Task<IActionResult> EditCars(int? id)
        {
            if (id == null)
            {
                reutrn NotFound();
            }
            var c = await _dbContext.Cars.FindAsync(id);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //post method for car ko detail edit garera submit garna ko lagi
        public async Task<IActionResult> EditCars(Cars c)
        {
            var carfromdb = await _dbContext.Cars.SingleorDefaultAsync(car => car.carId == c.carId);
            if(carfromdb == null){
                return NotFound();
            }
            carfromdb.CarName = c.CarName;
            carfromdb.Description = c.Description;
            carfromdb.price = c.price;
            _dbContext.Cars.Update(carfromdb);
            await _dbContext.SaveChangesAsync();
            return RedirecToAction(nameof(Index)); 
        }
        //delete
        public async Task<IActionResult> DeleteCar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var car = await _dbContext.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}