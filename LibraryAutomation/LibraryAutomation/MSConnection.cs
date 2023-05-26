using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAutomation
{
    internal class MSConnection


        //Burası SQL Server ile konuştuğumuz Class
    {
        static string con_string = "Server=LAPTOP-7U5PUCF6;Database=AlbaLibrary;Trusted_Connection=True;";//Static Connection String
        public string query_string { get; set; }

        SqlConnection Baglanti = new SqlConnection();

        SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr { get; set; }

        public int LastID { get; set; }
        public void connect()
        {

            Baglanti.ConnectionString = con_string;


        }

        public void connect_close()
        {

            Baglanti.Close();
        }



        public void execute(bool ExecuteReader = false,bool return_id = false)
        {
            Baglanti.Open();
            cmd.Connection = Baglanti;//SQL Connection.
            cmd.CommandText = query_string;//Yolladığımız queryi alıyoruz
            if (ExecuteReader == false)//İstenilen veri üzerinde değişiklik yapılacak mı; orn INSERT DELETE vb için False, SELECT için TRUE
            {
                if (return_id == false)//Geriye ID DONSUN MU 
                {

                    cmd.ExecuteNonQuery();//Geriye Hiçbir şey döndürmez

                }
                else
                {
                    LastID = (Int32)cmd.ExecuteScalar(); //Burda Query'de istediğimiz OUTPUT'U DÖNDÜRÜR
                     
                }
                Baglanti.Close();

            }
            else
            {
                cmd.ExecuteScalar();//Geriye Data Reader döndürüyoruz. Bununla birlikte verileri ekrana basabiliyoruz.
                dr = cmd.ExecuteReader();
              


            }
           
        

        }



    }
}
