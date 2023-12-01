using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BookStoreCommon.Book;

namespace BookStoreCommon.Wishlist
{
    public class Wishlist
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int WishlistId { get; set; }
      
        //[Required(ErrorMessage = "UserId is null")]
        //public int UserId { get; set; }
       
        //[Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "IsAvailable is null")]
        public bool IsAvailable { get; set; }

        public virtual Books book  { get; set; }
        
    }
}
