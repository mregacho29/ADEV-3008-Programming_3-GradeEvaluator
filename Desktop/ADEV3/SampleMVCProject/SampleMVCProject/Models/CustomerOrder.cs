using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMVCProject.Models
{
	public class Customer
	{
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Please enter your first name")]
        [Display(Name ="First")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last")]
        public string LastName { get; set; }

        [Display(Name ="Name")]
        public string FullName 
        { 
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        public ICollection<Order> Order { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }

        //navigation property
        public Customer Customer { get; set; }
    }
    
    public class Item
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double UnitPrice { get; set; }
    } 
    
}

