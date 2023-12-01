using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        [Required(ErrorMessage = "BookName is null")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "BookDescription is null")]
        public string BookDescription { get; set; }
        [Required(ErrorMessage = "BookAuthor is null")]
        public string BookAuthor { get; set; }
        [Required(ErrorMessage = "BookImage is null")]
        public string BookImage { get; set; }
        [Required(ErrorMessage = "BookCount is null")]
        public int BookCount { get; set; }
        [Required(ErrorMessage = "BookPrice is null")]
        public int BookPrice { get; set; }
        [Required(ErrorMessage = "Rating is null")]
        public int Rating { get; set; }

    }
}
