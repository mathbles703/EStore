using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace eStore.Models
{
    public class CarModel
    {
        /// <summary>
        ///  MenuItemModel - Model class representing a MenuItem
        ///     Author:     Evan Lauersen
        ///     Date:       Created: Feb 27, 2016
        ///     Purpose:    Model class to interface with DB and feed data to 
        ///                 Controller
        /// </summary>
        private AppDbContext _db;
        /// <summary>
        /// constructor should pass instantiated DbContext
        /// <summary>
        public CarModel(AppDbContext context)
        {
            _db = context;
        }
        public bool loadCategories(string rawJson)
        {
            bool loadedCars = false;
            try
            {
                // clear out the old rows
                _db.CarClasses.RemoveRange(_db.CarClasses);
                _db.SaveChanges();

                dynamic decodedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(rawJson);
                List<String> allCarClasses = new List<String>();

                foreach (var c in decodedJson)
                {
                    allCarClasses.Add(Convert.ToString(c["CARCLASS"]));  
                }

                // distinct will remove duplicates before we insert them into the db
                IEnumerable<String>carclasses = allCarClasses.Distinct<String>();

                foreach (string c in carclasses)
                {
                    CarClass cat = new CarClass();
                    cat.Name = c;
                    _db.CarClasses.Add(cat);
                    _db.SaveChanges();
                }
                loadedCars = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedCars;
        }
        public bool loadCars(string rawJson)
        {
            bool loadCars = false;
            try
            {
                List<CarClass> CarClasses = _db.CarClasses.ToList();
                // clear out the old
                _db.Cars.RemoveRange(_db.Cars);
                _db.SaveChanges();
                string decodedJsonStr = Decoder(rawJson);
                dynamic carJson = Newtonsoft.Json.JsonConvert.DeserializeObject(decodedJsonStr);
                foreach (var m in carJson)
                {
                    Car car = new Car();
                    car.Id = Convert.ToInt32(m["ID"]);
                    car.Manufacturer = Convert.ToString(m["MAN"]);
                    car.Model = Convert.ToString(m["MOD"]);
                    car.Year = Convert.ToInt32(m["YEAR"]);
                    car.NumOfDoors = Convert.ToInt32(m["DOOR"]);
                    car.NumOfCylinders = Convert.ToInt32(m["CYL"]);
                    car.SafetyRating = Convert.ToInt32(m["SAFE"]);
                    car.GasolineCapacity = Convert.ToDouble(m["GAS"]);
                    car.HorsePower = Convert.ToInt32(m["HP"]);
                    car.MPG = Convert.ToDouble(m["MPG"]);
                    car.Transmission = Convert.ToString(m["TRAN"]);
                    car.GraphicName = Convert.ToString(m["GRPH"]);
                    car.CostPrice = Convert.ToDecimal(m["COST"]);
                    car.MSRP = Convert.ToDecimal(m["MSRP"]);
                    car.QtyOnHand = Convert.ToInt32(m["HAND"]);
                    car.QtyOnBackOrder = Convert.ToInt32(m["BACK"]);                   

                    string CARCLASS = Convert.ToString(m["CARCLASS"]);

                    foreach (CarClass carClass in CarClasses)
                    {
                        if (carClass.Name == CARCLASS)
                            car.CarClassId = carClass.Id;
                    }

                    car.Description = Convert.ToString(m["DESC"]);

                    _db.Cars.Add(car);
                    _db.SaveChanges();
                }
                loadCars = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadCars;
        }
        public string Decoder(string value)
        {
            Regex regex = new Regex(@"\\u(?<Value>[a-zA-Z0-9]{4})", RegexOptions.Compiled);
            return regex.Replace(value, "");
        }
        public List<Car> GetAll()
        {
            return _db.Cars.ToList();
        }
        public List<Car> GetAllByCategory(int id)
        {
            return _db.Cars.Where(car => car.CarClassId == id).ToList();
        }
    }
}
