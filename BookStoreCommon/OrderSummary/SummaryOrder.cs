using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BookStoreCommon.Book;
using BookStoreCommon.OrderPlaced;
using BookStoreCommon.Cart;

namespace BookStoreCommon.OrderSummary
{
    public class SummaryOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SummaryId { get; set; }

        [Required(ErrorMessage = "OrderId is null")]
        public int OrderId { get; set; }

        public virtual Books Book { get; set; }

        public virtual Carts Cart { get; set; }

        public virtual PlaceOrder OrderPlaced { get; set; }
    }
}
