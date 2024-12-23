using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using statements for data annotations
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BITPublishing.Models
{
    /// <summary>
    /// Genre Model - to represent Genres table in database
    /// </summary>
    public class Genre
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public String Description { get; set; }
    }

    /// <summary>
    /// Author Model - to represent the Authors table in the database
    /// </summary>
    public class Author
    {
        //annotation to have database generate next available pkey value
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }


        //FirstName has 3 annotations
        [Required]
        [Display(Name ="First\nName")]
        [StringLength(35,MinimumLength =1)]
        public String FirstName { get; set; }


        [Required]
        [Display(Name ="Last\nName")]
        [StringLength(35, MinimumLength =1)]
        public String LastName { get; set; }

        [Required]
        [StringLength(300)]
        public String Address { get; set; }

        [Required]
        public String City { get; set; }

        //assignment will require regex to limit user
        //to only Canadian province codes
        [Required]
        [RegularExpression("[A-Z][A-Z]")]
        public String Province { get; set; }

        //assignment will require valid Canadian Postal Code values
        [Required]
        [Display(Name ="Postal\nCode")]
        public String PostalCode { get; set; }

        public String Notes { get; set; }

        [Display(Name ="Name")]
        public String FullName 
        { 
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name ="Address")]
        public String FullAddress 
        { 
            get 
            {
                return String.Format("{0} {1}, {2} {3}", Address, City, Province, PostalCode);
            } 
        }

        //represents a many relationship
        //as defined on the class diagram
        public virtual ICollection<Book> Book { get; set; }


    }

    /// <summary>
    /// Book Model - to represent the Books table in the database
    /// </summary>
    public class Book
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        //foreign key name must match the name of the corresponding
        //navigation property
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        [Required]
        public int ISBN { get; set; }

        [Required]
        public String Title { get; set; }


        //use "{0:c}" for currency and "{0:p}" for pecent formatting
        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DatePublished { get; set; }

        public String  Notes { get; set; }

        //represents a 1 or 0 on class diagram
        public virtual Genre Genre { get; set; }

        public virtual Author Author { get; set; }
    }
}