using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace MovieApplication
{
    public class BusinessManager
    {
        public DataTable dt;
       
        
        public BusinessManager()
        {
            SqlConnection connection=new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from MovieApplicationData", connection);
            DataSet da = new DataSet();
            connection.Open();
            sqlDataAdapter.Fill(da);
            dt= da.Tables[0];
            connection.Close();


        }
        //public bool Add(string name, Desciption description, Movie movie)
        //{
        //    MovieName m = new MovieName(name,  description,  movie);
        //    movies[index] = m;
        //    index++;
        //    return true;
        //}
        //public int searchMovie(string name)
        //{
        //    int inx = -1;
        //    for(int i =0; i<index; i++)
        //    {
        //        if (movies[i].getName() == name)
        //        {
        //            inx = i;
        //            break;
        //        }
        //    }
        //    return inx;
        //}

        //public string listMovies()
        //{
        //    string allMovies = "";
        //    for(int i =0; i<index;i++)
        //    {
        //        allMovies+= movies[i].getName() + " - " +
        //            movies[i].description.getCountry()+ " - "+
        //            movies[i].movie.getProducerName();
        //    }
        //    return allMovies;
        //}
        //public int deleteMovie(string name)
        //{
        //    int inx = -1;
        //    for(int i =0; i<index;i++)
        //    {

        //        if (movies[i].getName()==name)
        //        {
        //            inx = i;
        //            for (int j = i + 1; j < index; j++)
        //            {
        //                movies[j - 1] = movies[j];
        //            }
        //            index--;
        //            movies[index] = null;
        //            break;
        //        }
        //    }
        //    return inx;
        //}

    }
}
