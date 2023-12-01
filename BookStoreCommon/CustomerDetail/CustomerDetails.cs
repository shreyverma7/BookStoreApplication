using BookStoreCommon.Book;
using BookStoreCommon.TypeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.CustomerDetail
{
    public class CustomerDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }

      

        [Required(ErrorMessage = "FullName is null")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "MobileNumber is null")]
        public int MobileNumber { get; set; }

        [Required(ErrorMessage = "Address is null")]
        public string Address { get; set; }

        [Required(ErrorMessage = "CityOrTown is null")]
        public string CityOrTown { get; set; }

        [Required(ErrorMessage = "State is null")]
        public string State { get; set; }
        [Required(ErrorMessage = "TypeId is null")]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypesModel TypeModel { get; set; }

    }
}
