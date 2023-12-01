using BookStoreCommon.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.OrderPlaced
{
    public class PlaceOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "CustomerId is null")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "CartId is null")]
        public int CartId { get; set; }
        public virtual Carts Cart { get; set; }
    }
}
