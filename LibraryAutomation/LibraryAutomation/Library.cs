using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAutomation
{
    internal class Library
    {
        //Tüm Kütüphane İşlemleri
        public void Manage()
        {
            
            if (Program.yetki != 2)
            {
                return;
            }//Eğer Yönetici Değilsen Giremezsin
            MSConnection connection = new MSConnection();
        baslangic:
            Console.Clear();
            Console.WriteLine("\r\n           _      ____            _      _____ ____  _____            _______     __\r\n     /\\   | |    |  _ \\   /\\     | |    |_   _|  _ \\|  __ \\     /\\   |  __ \\ \\   / /\r\n    /  \\  | |    | |_) | /  \\    | |      | | | |_) | |__) |   /  \\  | |__) \\ \\_/ / \r\n   / /\\ \\ | |    |  _ < / /\\ \\   | |      | | |  _ <|  _  /   / /\\ \\ |  _  / \\   /  \r\n  / ____ \\| |____| |_) / ____ \\  | |____ _| |_| |_) | | \\ \\  / ____ \\| | \\ \\  | |   \r\n /_/    \\_\\______|____/_/    \\_\\ |______|_____|____/|_|  \\_\\/_/    \\_\\_|  \\_\\ |_|   \r\n                                                                                    \r\n                                                                                    \r\n");
            Console.WriteLine(" Alba Library Kitaplık Yönetim Sistemine Hoşgeldin\n");
            Console.WriteLine(" İşlemler\n -------------------\n 1->Kitapları Yönet\n 2->Yazarları Yönet\n 3->Kitap Türlerini Yönet\n 4->Çevirmenleri Yönet\n 5->Geri Dön");
        islem_baslangic:
            switch (Console.ReadLine())
            {
                case "1":
                islem_bir_baslangic:
                    Console.Clear();

                    connection.connect();
                    connection.query_string = "select * from books INNER JOIN authors ON books.Author_ID = authors.Author_ID INNER JOIN translators ON books.Translator_ID = translators.Translator_ID INNER JOIN genres ON books.Book_Genre_ID = genres.Book_Genre_ID";
                    connection.execute(true);
                    Console.WriteLine("\n");
                    while (connection.dr.Read())
                    {
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine(" ID->" + (int)connection.dr["Book_ID"]);
                        Console.WriteLine(" Kitap İsmi->" + connection.dr["Book_Name"].ToString());
                        Console.WriteLine(" Yazar İsmi->" + connection.dr["Author_Name_Surname"].ToString());
                        Console.WriteLine(" Kitap Türü->" + connection.dr["Book_Genre_Name"].ToString());
                        Console.WriteLine(" Çevirmen İsmi->" + connection.dr["Translator_Name_Surname"].ToString());
                        Console.WriteLine(" Eklenme Tarihi->" + connection.dr["Book_Added_Date"].ToString());
                        Console.WriteLine("---------------------------------");
                    }
                    connection.connect_close();//Tüm Kitapları Listeliyorum
                    Console.WriteLine("\n İşlemler\n -------------------\n 1->Kitap Oluştur\n 2->Kitap Sil\n 3->Geri Dön");
                kitap_baslangic:
                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine(" Yazarlar\n -------------------");
                            connection.connect();
                            connection.query_string = "select * from authors";
                            connection.execute(true);
                            while (connection.dr.Read())
                            {
                                Console.WriteLine(" ID->" + (int)connection.dr["Author_ID"] + " Yazar İsmi -> " + connection.dr["Author_Name_Surname"].ToString());
                            }
                            connection.connect_close();
                            Console.WriteLine("\n Türler\n -------------------");
                            connection.connect();
                            connection.query_string = "select * from genres";
                            connection.execute(true);
                            while (connection.dr.Read())
                            {
                                Console.WriteLine(" ID->" + (int)connection.dr["Book_Genre_ID"] + " Tür İsmi -> " + connection.dr["Book_Genre_Name"].ToString());
                            }
                            connection.connect_close();
                            Console.WriteLine("\n Çevirmenler\n -------------------");
                            connection.connect();
                            connection.query_string = "select * from translators";
                            connection.execute(true);
                            while (connection.dr.Read())
                            {
                                Console.WriteLine(" ID->" + (int)connection.dr["Translator_ID"] + " Çevirmen İsmi -> " + connection.dr["Translator_Name_Surname"].ToString());
                            }
                            connection.connect_close();

                            //Kitap Oluşturmak İçin Yazar Çevirmen Ve Kitap Türlerini Döndürüyorum.
                            Console.WriteLine("\n Kitap İsmi Giriniz");
                        kitap_adi:
                            string kitap_adi = Console.ReadLine();
                            if (kitap_adi == "")
                            {
                                Console.WriteLine("\n Kitap Adı Boş Bırakılamaz.Lütfen Kitap Adı Giriniz.");
                                goto kitap_adi;
                            }
                            Console.WriteLine("\n Yazar ID'si Giriniz");
                        yazar_id:
                            string yazar_id = Console.ReadLine();
                            if (yazar_id == "")
                            {
                                Console.WriteLine("\n Yazar ID'si Boş Bırakılamaz.Lütfen Yazar ID'si Giriniz.");
                                goto yazar_id;
                            }
                            Console.WriteLine("\n Kitap Türü ID'si Giriniz");
                        kitap_turu:
                            string kitap_turu = Console.ReadLine();
                            if (kitap_turu == "")
                            {
                                Console.WriteLine("\n Kitap Türü Boş Bırakılamaz.Lütfen Kitap Türü ID'si Giriniz.");
                                goto kitap_turu;
                            }
                            Console.WriteLine("\n Çevirmen ID'si Giriniz");
                        cevirmen_id:
                            string cevirmen_id = Console.ReadLine();
                            if (kitap_turu == "")
                            {
                                Console.WriteLine("\n Çevirmen ID'si Boş Bırakılamaz.Lütfen Çevirmen ID'si Giriniz.");
                                goto cevirmen_id;
                            }

                            //Kitap Adı ve Yazar,Tür ve çevirmen ID'lerini Alıp veri tabanına aktarıyoruz.

                            connection.query_string = "INSERT INTO books(Book_Name,Author_ID,Book_Genre_ID,Translator_ID) VALUES ('" + kitap_adi + "','" + yazar_id + "','" + kitap_turu + "','" + cevirmen_id + "');";
                            connection.execute();
                            goto islem_bir_baslangic;

                        case "2":

                            //Klasik Silme İşlemi
                            Console.WriteLine("Silinecek Kitap ID'sini Giriniz.");
                            string ID = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "DELETE FROM books WHERE Book_ID = '" + ID + "';";
                            connection.execute();
                            goto islem_bir_baslangic;


                        case "3":

                            goto baslangic;


                        default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto yazar_baslangic;
                    }


                    goto baslangic;




                case "2":
                islem_iki_baslangic:
                    Console.Clear();
                    //Yazarları Ekrana Basıyoruz
                    connection.connect();
                    connection.query_string = "select * from authors";
                    connection.execute(true);
                    Console.WriteLine("\n");
                    while (connection.dr.Read())
                    {
                        Console.WriteLine(" ID->" + (int)connection.dr["Author_ID"] + " Yazar İsmi -> " + connection.dr["Author_Name_Surname"].ToString());
                    }
                    connection.connect_close();
                    Console.WriteLine("\n İşlemler\n -------------------\n 1->Yazar Oluştur\n 2->Yazar Sil\n 3->Yazar Düzenle\n 4->Geri Dön");
                yazar_baslangic:
                    switch (Console.ReadLine())
                    {
                        case "1":
                            //Yazar Ekleme İşlemi
                            Console.WriteLine("Yazar İsmi Giriniz");
                        yazar_adi:
                            string yazar_adi = Console.ReadLine();
                            if (yazar_adi == "")
                            {
                                Console.WriteLine("Yazar Adı Boş Bırakılamaz.Lütfen Yazar Adı Giriniz.");
                                goto yazar_adi;
                            }

                            connection.query_string = "INSERT INTO authors(Author_Name_Surname) VALUES ('" + yazar_adi + "');";
                            connection.execute();
                            goto islem_iki_baslangic;

                        case "2":
                            //Yazar Silme İşlemi
                            Console.WriteLine("Silinecek Yazar ID'sini Giriniz.");
                            string ID = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "DELETE FROM authors WHERE Author_ID = '" + ID + "';";
                            connection.execute();
                            goto islem_iki_baslangic;

                        case "3":
                            //Yazar Düzenleme İşlemi
                            Console.WriteLine("Düzenlenecek Yazar ID'sini Giriniz.");
                            string cID = Console.ReadLine();
                            Console.WriteLine("Düzenlenecek Yazarın Adını Giriniz");
                            string dyaz_ad = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "UPDATE authors SET Author_Name_Surname = '" + dyaz_ad + "' WHERE Author_ID = '" + cID + "';";
                            connection.execute();
                            goto islem_iki_baslangic;

                        case "4":

                            goto baslangic;


                        default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto yazar_baslangic;
                    }


                    goto baslangic;



                case "3":
                islem_uc_baslangic:
                    Console.Clear();
                    //Kitap Türlerini Ekraa Basıyoruz
                    connection.connect();
                    connection.query_string = "select * from genres";
                    connection.execute(true);
                    Console.WriteLine("\n");
                    while (connection.dr.Read())
                    {
                        Console.WriteLine(" ID->" + (int)connection.dr["Book_Genre_ID"] + " Tür İsmi -> " + connection.dr["Book_Genre_Name"].ToString());
                    }
                    connection.connect_close();
                    Console.WriteLine("\n İşlemler\n -------------------\n 1->Tür Oluştur\n 2->Tür Sil\n 3->Tür Düzenle\n 4->Geri Dön");
                tur_baslangic:
                    switch (Console.ReadLine())
                    {

                        case "1":
                            //Kitap Türü Ekliyoruz
                            Console.WriteLine("Kitap Türü Giriniz");
                        kitap_tur:
                            string tur_adi = Console.ReadLine();
                            if (tur_adi == "")
                            {
                                Console.WriteLine("Tür Adı Boş Bırakılamaz.Lütfen Tür Adı Giriniz.");
                                goto kitap_tur;
                            }

                            connection.query_string = "INSERT INTO genres(Book_Genre_Name) VALUES ('" + tur_adi + "');";
                            connection.execute();
                            goto islem_uc_baslangic;

                        case "2":
                            //Kitap Türü Siliyoruz.
                            Console.WriteLine("Silinecek Tür ID'sini Giriniz.");
                            string ID = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "DELETE FROM genres WHERE Book_Genre_ID = '" + ID + "';";
                            connection.execute();
                            goto islem_uc_baslangic;

                        case "3":
                            //Kitap Türü Düzenleme İşlemi
                            Console.WriteLine("Düzenlenecek Tür ID'sini Giriniz.");
                            string cID = Console.ReadLine();
                            Console.WriteLine("Düzenlenecek Türün Adını Giriniz");
                            string dtur_ad = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "UPDATE genres SET Book_Genre_Name = '" + dtur_ad + "' WHERE Book_Genre_ID = '" + cID + "';";
                            connection.execute();
                            goto islem_uc_baslangic;

                        case "4":

                            goto baslangic;


                        default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto tur_baslangic;
                    }


                    goto baslangic;




                case "4":
                islem_dort_baslangic:
                    Console.Clear();
                    //Çevirmenleri Ekrana Basıyoruz
                    connection.connect();             
                    connection.query_string = "select * from translators";
                    connection.execute(true);
                    Console.WriteLine("\n");
                    while (connection.dr.Read())
                    {
                        Console.WriteLine(" ID->" + (int)connection.dr["Translator_ID"] + " Çevirmen İsmi -> " + connection.dr["Translator_Name_Surname"].ToString());
                    }
                    connection.connect_close();
                    Console.WriteLine("\n İşlemler\n -------------------\n 1->Çevirmen Oluştur\n 2->Çevirmen Sil\n 3->Çevirmen Düzenle\n 4->Geri Dön");
                cevirmen_baslangic:
                    switch (Console.ReadLine())
                    {
                        case "1":
                            //Çevirmen Ekliyrouz
                            Console.WriteLine("Çevirmen Ad Ve Soyadı Giriniz");
                        cevirmen_ad:
                            string cevirmen_ad_soyad = Console.ReadLine();
                            if (cevirmen_ad_soyad == "")
                            {
                                Console.WriteLine("Çevirmen Ad ve Soyadı Boş Bırakılamaz.Lütfen Ad Ve Soyad Giriniz.");
                                goto cevirmen_ad;
                            }

                            connection.query_string = "INSERT INTO translators(Translator_Name_Surname) VALUES ('" + cevirmen_ad_soyad + "');";
                            connection.execute();
                            goto islem_dort_baslangic;

                        case "2":
                            //Çevirmen Silme İşlemi
                            Console.WriteLine("Silinecek Çevirmenin ID'sini Giriniz.");
                            string ID = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "DELETE FROM translators WHERE Translator_ID = '" + ID + "';";
                            connection.execute();
                            goto islem_dort_baslangic;

                        case "3":
                            //Çevirmen Düzenleme İşlemi
                            Console.WriteLine("Düzenlenecek Çevirmenin ID'sini Giriniz.");
                            string cID = Console.ReadLine();
                            Console.WriteLine("Düzenlenecek Çevirmenin Ad ve Soyadının Giriniz");
                            string dcevirmen_ad = Console.ReadLine();
                            connection.connect();
                            connection.query_string = "UPDATE translators SET Translator_Name_Surname = '" + dcevirmen_ad + "' WHERE Translator_ID = '" + cID + "';";
                            connection.execute();
                            goto islem_dort_baslangic;

                        case "4":

                            goto baslangic;


                        default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto cevirmen_baslangic;
                    }


                    goto baslangic;






                case "5": return;


                default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto islem_baslangic;
            }


        }

        public void List()
        {
            //Kitapları Listeliyoruz.
            membership uyelik = new membership();
            MSConnection connection = new MSConnection();
        baslangic:
            Console.Clear();

            connection.connect();
            connection.query_string = "select * from books INNER JOIN authors ON books.Author_ID = authors.Author_ID INNER JOIN translators ON books.Translator_ID = translators.Translator_ID INNER JOIN genres ON books.Book_Genre_ID = genres.Book_Genre_ID";
            connection.execute(true);
            Console.WriteLine("\n");
            while (connection.dr.Read())
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine(" ID->" + (int)connection.dr["Book_ID"]);
                Console.WriteLine(" Kitap İsmi->" + connection.dr["Book_Name"].ToString());
                Console.WriteLine(" Yazar İsmi->" + connection.dr["Author_Name_Surname"].ToString());
                Console.WriteLine(" Kitap Türü->" + connection.dr["Book_Genre_Name"].ToString());
                Console.WriteLine(" Çevirmen İsmi->" + connection.dr["Translator_Name_Surname"].ToString());
                Console.WriteLine(" Eklenme Tarihi->" + connection.dr["Book_Added_Date"].ToString());
                Console.WriteLine("---------------------------------");
            }
            connection.connect_close();
            //Tüm Kitapları Listeliyoruz

            Console.WriteLine("\n İşlemler\n -------------------\n 1->Kitap Kirala\n 2->Geri Dön");
        islem_baslangic:
            switch (Console.ReadLine())
            {
                case "1":
                    if (Program.giris != true)
                    {//Eğer Giriş Yapmadıysa Kitap Kiralayamaz
                        Console.WriteLine(" Kitap Kiralama Sistemi için Giriş Yapmalısınız.\n\n İşlemler\n -------------------\n 1->Giriş Yap \n 2->Kayıt Ol \n 3->Geri Dön");
                    ikinci_islem_baslangic:
                        switch (Console.ReadLine())
                        {
                            case "1": Console.Clear(); uyelik.login(); goto baslangic;
                            case "2": Console.Clear(); uyelik.sign_up(); goto baslangic;
                            case "3": goto baslangic;

                            default:
                                Console.Write("Girilen İşlem ID'si Geçersizdir."); goto ikinci_islem_baslangic;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" Lütfen Kiralamak İstediğiniz Kitabın ID'sini Giriniz");

                    kitap_id:
                        string kitap_id = Console.ReadLine();
                        if (kitap_id == "")
                        {
                            Console.WriteLine("\n Kitap ID'si Boş Bırakılamaz.Lütfen Kitap ID'si Giriniz.");
                            goto kitap_id;
                        }

                        connection.query_string = "Select * from leased_users where User_ID = '" + Program.id + "' AND Book_ID = '" + kitap_id + "'";

                        connection.execute(true);

                        if (connection.dr.Read())
                        {
                            Console.WriteLine("Bu Kitap Zaten Kiralanmıştır.");//Eğer Kitap Zaten Kiralanmışsa Kontrol Edip Hata Döndürüyoruz Eklenmediyse İşleme Devam Ediyoruz.
                            Console.ReadLine
                                ();
                        }
                        else
                        {
                            connection.connect_close();
                            connection.query_string = "INSERT INTO  leased_users(User_ID,Book_ID) VALUES ('" + Program.id + "','" + kitap_id + "');";
                            connection.execute();
                            Console.WriteLine("Kitap Kiralama İşlemi Başarılıdır.Devam Etmek İçin ENTER Tuşuna Basın.");
                            Console.ReadLine();
                            goto baslangic;
                        }







                    }
                    break;


                case "2": return;
                default:
                    Console.Write("Girilen İşlem ID'si Geçersizdir."); goto islem_baslangic;

            }
        }

        public void my_books()
        {

            //Bu Method kiralanan kitapları gösteren sayfa.. 
            MSConnection connection = new MSConnection();
           baslangic:
            Console.Clear();

            connection.connect();
            connection.query_string = "select * from leased_users  INNER JOIN books ON leased_users.Book_ID = books.Book_ID INNER JOIN authors ON books.Author_ID = authors.Author_ID INNER JOIN translators ON books.Translator_ID = translators.Translator_ID INNER JOIN genres ON books.Book_Genre_ID = genres.Book_Genre_ID WHERE User_ID = '"+Program.id+"'";
            connection.execute(true);
            Console.WriteLine("\n");
            while (connection.dr.Read())
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine(" ID->" + (int)connection.dr["Book_ID"]);
                Console.WriteLine(" Kitap İsmi->" + connection.dr["Book_Name"].ToString());
                Console.WriteLine(" Yazar İsmi->" + connection.dr["Author_Name_Surname"].ToString());
                Console.WriteLine(" Kitap Türü->" + connection.dr["Book_Genre_Name"].ToString());
                Console.WriteLine(" Çevirmen İsmi->" + connection.dr["Translator_Name_Surname"].ToString());
                Console.WriteLine(" Eklenme Tarihi->" + connection.dr["Book_Added_Date"].ToString());
                Console.WriteLine("---------------------------------");
            }
            connection.connect_close();
            //Kullanıcının Kiraladığı Kitapları Veritabanından Sorguyla Çekip Ekrana Basıyoruz

            Console.WriteLine("\n İşlemler\n -------------------\n 1->Kitabı İade Et \n 2->Geri Dön");

            islem_baslangic:
            switch (Console.ReadLine())
            {
                case "1":
                    //Eğer İade Etmişse Veri Tabanından Siliyoruz.
                    Console.WriteLine("İade Edilecek Kitap ID'sini Giriniz.");
                    string ID = Console.ReadLine();
                    connection.connect();
                    connection.query_string = "DELETE FROM leased_users WHERE User_ID = '" + Program.id + "' AND Book_ID = '"+ID+"';";
                    connection.execute();
                    goto baslangic;

                case "2":return;

                default:
                    Console.WriteLine("Girilen İşlem ID'si Geçersizdir."); goto islem_baslangic;
                    break;
            }
            Console.ReadLine();




        }



    }
}
