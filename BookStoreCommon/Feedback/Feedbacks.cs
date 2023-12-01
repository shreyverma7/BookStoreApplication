using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookStoreCommon.Feedback
{
    public class Feedbacks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "UserId is null")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "BookId is null")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "CustomerDescription is null")]
        public string CustomerDescription { get; set; }

        [Required(ErrorMessage = "Rating is null")]
        public int Rating { get; set; }
    }
}
