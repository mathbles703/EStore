using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eStore.Models
{
    public partial class Car
    {
        public Car()
        {
        }

        [StringLength(15)]
        public string Id { get; set; }
        [Required]
        public int CarClassId { get; set; }
        [ForeignKey("CarClassId")]
        public CarClass CarClass { get; set; }
        [Required]
        [StringLength(20)]
        public string Manufacturer { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int NumofSeats { get; set; }
        [Required]
        public int NumOfDoors { get; set; }
        [Required]
        public int NumOfCylinders { get; set; }
        [Required]
        public int SafetyRating { get; set; }
        [Required]
        public double GasolineCapacity { get; set; }
        [Required]
        public int HorsePower { get; set; }
        [Required]
        public double MPG { get; set; }
        [Required]
        [StringLength(10)]
        public string Transmission { get; set; }


        [Required]
        [StringLength(20)]
        public string GraphicName { get; set; }
        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal MSRP { get; set; }
        public int QtyOnHand { get; set; }
        public int QtyOnBackOrder { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
    }
}
