using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADEV3008Windows
{
    public partial class LinqExample : Form
    {
        public LinqExample()
        {
            InitializeComponent();
        }

        //given
        //Arrays used in the code examples that follow
        String[] cities = { "Toronto", "Ottawa", "Winnipeg", "Montreal", "Calgary" };
        String[] countries = { "Canada", "USA", "Mexico", "Argentina", "Brazil" };
        int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
        int[] fives = { 5, 10, 15, 20, 25, 30, 35 };



        /// <summary>
        /// For Lecture:  select example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            var query =
                from results
                in cities
                select results;



            foreach (String city in query)
                lblValue.Text += city + "\r";

        }

        /// <summary>
        /// For Lecture:  Where example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWhere_Click(object sender, EventArgs e)
        {
            var query =
                 from results
                 in primes
                 where results > 10
                 //this where applies to result set of previous where
                 where results / 2 > 7
                 select results;


            foreach (int prime in query)
                lblValue.Text += prime + "\r";


        }

        /// <summary>
        /// For Lecture:  Join Example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoin_Click(object sender, EventArgs e)
        {
            ////var query =
            ////    from results
            ////    in primes
            ////    from moreResults
            ////    in fives
            ////    where results == moreResults
            ////    select results;

            ////foreach (int item in query)
            ////    lblValue.Text += item + ",";


            var query =
                from results in primes
                join moreResults in fives
                on results equals moreResults
                select results;


            foreach (int item in query)
                lblValue.Text += item + "\r";


        }

        /// <summary>
        /// For Lecture:  Let example - all primes + 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLet_Click(object sender, EventArgs e)
        {
            var query =
                from result in primes
                let sum = result + 100
                select sum;

            foreach (int item in query)
                lblValue.Text += item + "\r";


        }

        /// <summary>
        /// For Lecture:  Lambda example
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLambda_Click(object sender, EventArgs e)
        {
            var query = cities.Where(value => value.Length == 8);

            foreach (String city in query)
                lblValue.Text += city + "\r";

            //Result:  
            //Winnipeg
            //Montreal


        }

        /// <summary>
        /// given - clears the feedback label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lblValue.Text = "";
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            //THE FOLLOWING MUST BE COMMENTED OUT UNTIL
            //1.  A REFERENCE HAS BEEN CREATED TO MVC PROJECT
            //2.  THE APP.CONFIG FILE IS UPDATED WITH AN 
            //APPROPRIATE CONNECTIONSTRING

            lblValue.Text = "follow instructions in code-behind to execute the script.";

            //DataContextClass db = new DataContextClass();

            //TYPE??? allAuthors = from authors in db.Authors
            //                                select authors;

            //TYPE??? allAuthorLastNames = from authors in db.Authors
            //                                        select authors.LastName;

            //TYPE??? lastName = (from author in db.Authors
            //                   where author.AuthorId == 2
            //                   select author.LastName).SingleOrDefault();

            //TYPE??? singleAuthor = from author in db.Authors
            //                      where author.AuthorId == 2
            //                      select author;
        }
    }
}
