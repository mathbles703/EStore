﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eStore.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int Qty { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
    }
}
