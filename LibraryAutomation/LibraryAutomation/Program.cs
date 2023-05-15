using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryAutomation
{
    internal class Program
    {

        //Sisteme Giriş Yapıldığında veya kayıt olunduğunda Gerekli Bilgileri Hafızada Tutmak İçin ve her yerde kullanabilmek için public static değişkene tanımlıyoruz Webdeki SESSION Mantığı.
        public static int yetki; //2 çeşit yetki vardır. Bunlar Veritabanındaki User_Auth Bölümünde Tutulur. 1 -> Normal Üye 2-> Yönetici(Kitap Ekleme Silme Üye Ekleme Silme vb.)
        public static int id; // Kitap Kiralama İşlemlerinde Kullanılması İçin 
        public static string isim;
        public static string soyisim;
        public static string eposta_adresi;
        public static bool giris = false;// Gerekli Yerlerde Sorgulamak için 

        public static void Main(string[] args)
        {


            membership uyelik = new membership(); // Üyelik İle İlgili Tüm İşlemler Bu Classda.
            Library kutuphane = new Library(); // Kütüphane ve kitaplar ile ilgili tüm işlemler bu classda
            baslangic:
            Console.Clear();
            Console.WriteLine("\r\n           _      ____            _      _____ ____  _____            _______     __\r\n     /\\   | |    |  _ \\   /\\     | |    |_   _|  _ \\|  __ \\     /\\   |  __ \\ \\   / /\r\n    /  \\  | |    | |_) | /  \\    | |      | | | |_) | |__) |   /  \\  | |__) \\ \\_/ / \r\n   / /\\ \\ | |    |  _ < / /\\ \\   | |      | | |  _ <|  _  /   / /\\ \\ |  _  / \\   /  \r\n  / ____ \\| |____| |_) / ____ \\  | |____ _| |_| |_) | | \\ \\  / ____ \\| | \\ \\  | |   \r\n /_/    \\_\\______|____/_/    \\_\\ |______|_____|____/|_|  \\_\\/_/    \\_\\_|  \\_\\ |_|   \r\n                                                                                    \r\n                                                                                    \r\n");       
            Console.WriteLine(" Alba Kütüphane Sistemine Hoşgeldiniz.");
            if (giris == true)//Eğer Giriş Yapıldıysa
            {
                Console.WriteLine("\n Giriş Yapılmıştır.Giriş Yapan Kullanıcı = " + isim + " " + soyisim);//İsim Soyisim Basıyoruz.
         
                Console.WriteLine("\n\n İşlemler\n -------------------\n 1->Kitapları Listele\n 2->Kitaplarım\n 3->Çıkış Yap\n 4->Programı Sonlandır");//Üye Olan Kullanıcılara Özel 
                if (yetki == 2)
                {
                    Console.WriteLine(" 5->Kitaplığı Yönet\n 6->Kullanıcılar");//Yöneticilere Özel
                   
                }

                Console.WriteLine(" Yapılacak İşlemin Numarasını Giriniz : \n");

            }
            else
            {
                Console.WriteLine("\n\n İşlemler\n -------------------\n 1->Kitapları Listele\n 2->Giriş Yap\n 3->Kayıt Ol\n 4->Programı Sonlandır\n Yapılacak İşlemin Numarasını Giriniz : \n");//Üye Olmayan Kullanıcılar İçin
            }
           

            switch (Console.ReadLine())//Burda Switch Case Yapısında Case'ler break; ile direkt bitirilir. Burdaki goto baslangic ile kod hiçbir şekilde break'a gelmeyeceği için yazılmıyor.  
            {
                case "1": kutuphane.List();goto baslangic;   //Kitapların Listeleme Sayfası                   
                case "2": 
                  
                    if (giris == false)
                    {
                        Console.Clear(); uyelik.login(); goto baslangic; // Giriş Yapmadıysa Giriş Yap Sayfası
                    }
                    else
                    {
                        Console.Clear();kutuphane.my_books(); goto baslangic; //Giriş Yaptıysa Kiralanmış Kitapların Bulunduğu Sayfa
                    }

                    break;
                case "3":
                    if (giris == false)
                    {
                        Console.Clear(); uyelik.sign_up(); goto baslangic;// Giriş Yapmadıysa Kayıt Ol Sayfası
                    }
                    else
                    {

                        Console.Clear(); uyelik.logout(); goto baslangic; //Giriş Yaptıysa Çıkış Yap Sayfası
                    }          
                    
                   break;

                case "4": Environment.Exit(0); break; // Direkt Olarak Her Şeyi Kapatır.
                case "5":
                    if (yetki==2)
                    {
                        Console.Clear(); kutuphane.Manage(); goto baslangic; // Eğer Yönetici İseniz Buraya girebilirsiniz Kitap Ekleme Çıkarma Silme vb. .

                    }
                    else
                    {
                        Console.WriteLine("Girilen İşlem ID'si Geçersizdir.");  goto baslangic;
                    }
                    break; 
                case "6":
                    if (yetki==2)
                    {
                        Console.Clear(); uyelik.Users(); goto baslangic;

                    }
                    else
                    {
                        Console.WriteLine("Girilen İşlem ID'si Geçersizdir."); goto baslangic;
                    }
                    break;
                default: Console.WriteLine("Girilen İşlem ID'si Geçersizdir.");  goto baslangic; // Burda Verilen İşlem ID'leri harici bir ID'girilirse default olarak tekrardan ID isticektir.

            }
            goto baslangic;



        }




        



    }
}
