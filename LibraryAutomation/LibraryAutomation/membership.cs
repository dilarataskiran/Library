using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryAutomation
{
    internal class membership : Program
    {
        //Bu Class Tüm Üyelik İşlemlerini Kapsar


        public void login() // Giriş Yapmak İçin
        {
            Console.WriteLine("\r\n           _      ____            _      _____ ____  _____            _______     __\r\n     /\\   | |    |  _ \\   /\\     | |    |_   _|  _ \\|  __ \\     /\\   |  __ \\ \\   / /\r\n    /  \\  | |    | |_) | /  \\    | |      | | | |_) | |__) |   /  \\  | |__) \\ \\_/ / \r\n   / /\\ \\ | |    |  _ < / /\\ \\   | |      | | |  _ <|  _  /   / /\\ \\ |  _  / \\   /  \r\n  / ____ \\| |____| |_) / ____ \\  | |____ _| |_| |_) | | \\ \\  / ____ \\| | \\ \\  | |   \r\n /_/    \\_\\______|____/_/    \\_\\ |______|_____|____/|_|  \\_\\/_/    \\_\\_|  \\_\\ |_|   \r\n                                                                                    \r\n                                                                                    \r\n");

            Console.WriteLine("\n Giriş Yapmak İçin İstenilen Bilgiler \n ------------------- \n 1->Eposta Adresi\n 2->Şifre\n\n\n İşlemler\n -------------------\n 1->Giriş İşlemine Başla\n 2->Geri Dön");
        giris_baslangic:
            switch (Console.ReadLine())
            {

                case "1":
                giris_girdileri:
                    Console.WriteLine(" Eposta Adresinizi Giriniz");
                    string eposta = Console.ReadLine();
                    Console.WriteLine(" Şifrenizi Giriniz");
                    string sifre = Console.ReadLine();
                   
                    MSConnection connection = new MSConnection();//Vanilla SQL için Connectin Class'ı
                    connection.connect();//Bağlantı Oluşturuyoruz
                    connection.query_string = "SELECT * FROM users where User_Mail='" + eposta + "' AND User_Password='" + sifre + "'";//Simple SQL Query
                    connection.execute(true);//Execute Ediyoruz. Bu Methodların detaylarını ilgili classda bulabilirsin.
                    if (connection.dr.Read())
                    {
                        //Eğer Başarılı İse Program classında bulunan public değişkenlere user bilgilerini aktarıyoruz.
                        Program.yetki = (int)connection.dr["User_Auth"];
                        Program.isim = connection.dr["User_Name"].ToString();
                        Program.soyisim = connection.dr["User_SurName"].ToString();
                        Program.eposta_adresi = connection.dr["User_Mail"].ToString();
                        Program.giris = true;
                        Program.id = (int)connection.dr["User_ID"];


                        Console.WriteLine(" Giriş İşlemi Başarılı. Ana Sayfaya Dönmek için ENTER Tuşuna Basınız.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(" Giriş İşlemi Başarısız.\n İşlemler\n 1->Tekrar Giriş Yap\n 2->Geri Dön");
                    islem_baslangic:
                        switch (Console.ReadLine())
                        {

                            case "1": goto giris_girdileri;
                            case "2": return;
                            default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto islem_baslangic;
                        }
                    }





                    break;
                case "2": return;

                default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto giris_baslangic;
            }



        }

        public void sign_up()//Kayıt Olma
        {


            Console.WriteLine("\r\n           _      ____            _      _____ ____  _____            _______     __\r\n     /\\   | |    |  _ \\   /\\     | |    |_   _|  _ \\|  __ \\     /\\   |  __ \\ \\   / /\r\n    /  \\  | |    | |_) | /  \\    | |      | | | |_) | |__) |   /  \\  | |__) \\ \\_/ / \r\n   / /\\ \\ | |    |  _ < / /\\ \\   | |      | | |  _ <|  _  /   / /\\ \\ |  _  / \\   /  \r\n  / ____ \\| |____| |_) / ____ \\  | |____ _| |_| |_) | | \\ \\  / ____ \\| | \\ \\  | |   \r\n /_/    \\_\\______|____/_/    \\_\\ |______|_____|____/|_|  \\_\\/_/    \\_\\_|  \\_\\ |_|   \r\n                                                                                    \r\n                                                                                    \r\n");

            Console.WriteLine("\n Kayıt Olmak İçin İstenilen Bilgiler \n ------------------- \n 1->İsim\n 2->Soyisim\n 3->E-Posta Adresi\n 4->Şifre\n\n\n İşlemler\n -------------------\n 1->Kayıt İşlemine Başla\n 2->Geri Dön");
        kayit_baslangic:
            switch (Console.ReadLine())
            {

                case "1":
                    Console.WriteLine(" İsminizi Giriniz");
                isim_tekrar:
                   //connection stringe kadar validation.
                    string isim = Console.ReadLine();
                    if (isim == "")
                    {
                        Console.WriteLine(" İsiminiz boş bırakılamaz. Lütfen İsim Giriniz.");
                        goto isim_tekrar;
                    }
                    Console.WriteLine(" Soyisminizi Giriniz");
                soyisim_tekrar:
                    string soyisim = Console.ReadLine();
                    if (soyisim == "")
                    {
                        Console.WriteLine(" Soyisminiz boş bırakılamaz. Lütfen Soyisminizi Giriniz.");
                        goto soyisim_tekrar;
                    }

                    Console.WriteLine(" E-posta Adresi Giriniz");
                email_tekrar:
                    string email = Console.ReadLine();
                    if (!EmailRegex(email))//Email Regexi hazır koddur
                    {
                        Console.WriteLine(" E-Posta Adresi Hatalıdır.Lütfen Doğru bir E-Posta Adresi Giriniz");
                        goto email_tekrar;
                    }
                    Console.WriteLine(" Lütfen 8 Karakterli Şifrenizi Giriniz.Şifreniz En Az Bir Harf,Bir Büyük ve Bir Küçük Harf İçermelidir.");
                sifre_tekrar:
                    string sifre = Console.ReadLine();
                    if (!IsPasswordValid(sifre)) //Şifre Regexi Hazır Koddur
                    {
                        Console.WriteLine(" Şifreniz Yeteri Kadar Güçlü Değildir. Lütfen 8 Karakterli ve İçerisinde En Az Bir Harf,Bir Büyük ve Bir Küçük Harf İçeren Bir Şifre Giriniz");
                        goto sifre_tekrar;

                    }
                    MSConnection connection = new MSConnection();
                    connection.connect();
                    connection.query_string = "INSERT INTO users(User_Auth,User_Name,User_SurName,User_Mail,User_Password) OUTPUT INSERTED.User_ID VALUES ('1','" + isim + "','" + soyisim + "','" + email + "','" + sifre + "');";
                    connection.execute(false,true);//Veritabanına Kaydediyoruz.

                    Program.yetki = 1;
                    Program.isim = isim;
                    Program.soyisim = soyisim;
                    Program.eposta_adresi = eposta_adresi;
                    Program.giris = true;
                    Program.id = connection.LastID;
                    //Kayıt Olduktan sonra direkt olarak bilgilerini alıyoruz.
                    Console.WriteLine(" Kayıt İşlemi Başarılı. Ana Sayfaya Dönmek için ENTER Tuşuna Basınız.");
                    Console.ReadLine();


                    break;
                case "2": return;

                default: Console.WriteLine(" Girilen İşlem ID'si Geçersizdir.Lütfen Geçerli Bir İşlem ID'si Giriniz."); goto kayit_baslangic;
            }




        }

        public void logout()
        {

            //Çıkış Yapmak İçin Program Classındaki Bütün Public Değişkenleri Sıfırlıyoruz.
            Program.yetki = -1;
            Program.id = -1;
            Program.isim = null;
            Program.soyisim = null;
            Program.eposta_adresi = null;
            Program.giris = false;

        }

       

        public void Users() //Kullanıcılar için
        {

           
            if (Program.yetki != 2)
            {
                return;
            }//Sadece Yöneticiler Girebilir
            MSConnection connection = new MSConnection();
        baslangic:
            Console.Clear();

            connection.connect();
            connection.query_string = "select * from users";
            connection.execute(true);
            Console.WriteLine("\n");
            while (connection.dr.Read())
            {
                string yetki = "";
                if ((int)connection.dr["User_Auth"] == 1)
                {
                    yetki = "Üye";
                }
                else
                {
                    yetki = "Yönetici";
                }//Veri Tabanında Tuttuğumuz veriye göre Üye mi Yoksa Yönetici Mi Olduğunu Belirliyoruz

                Console.WriteLine("---------------------------------");
                Console.WriteLine(" ID->" + (int)connection.dr["User_ID"]);
                Console.WriteLine(" Üye Yetki->" + yetki);
                Console.WriteLine(" Üye İsmi->" + connection.dr["User_Name"].ToString());
                Console.WriteLine(" Üye Soyismi->" + connection.dr["User_SurName"].ToString());
                Console.WriteLine(" Üye Eposta Adresi->" + connection.dr["User_Mail"].ToString());
                Console.WriteLine(" Üye Eklenme Tarihi->" + connection.dr["User_Added_Date"].ToString());
                Console.WriteLine("---------------------------------");
            }//Tüm Üyeleri Ekrana Basıyoruz
            connection.connect_close();
            Console.WriteLine("\n İşlemler\n -------------------\n 1->Üye Oluştur\n 2->Üye Sil\n 3->Geri Dön");

          islem_baslangic:
            switch (Console.ReadLine())
            {
                case "1": Create(); goto baslangic;
                case "2":
                    Console.WriteLine("Silinecek Üye ID'sini Giriniz.");
                    string ID = Console.ReadLine();//ID'ye Göre Silme
                    connection.connect();
                    connection.query_string = "DELETE FROM users WHERE User_ID = '" + ID + "';";
                    connection.execute();
                     goto baslangic;
                case "3": return;



                default:
                    Console.Write("Girilen İşlem ID'si Geçersizdir.");  goto islem_baslangic;

            }

        }

        public void Create()
        {

            //Üye Oluşturma
            Console.Clear();
            Console.WriteLine("Üye Yetkisi Giriniz(1 = Normal Üye,2=Yönetici)");
        yetki:
            int yetki = int.Parse(Console.ReadLine());
            if (yetki != 1 && yetki != 2)
            {
                Console.WriteLine("Girdiğiniz Yetki ID'si Tanımsızdır.Lütfen Sadece Normal Üye için 1 Yönetici için 2 Giriniz.");
                goto yetki;
            }
            Console.WriteLine(" İsminizi Giriniz");
        isim_tekrar:
            string isim = Console.ReadLine();
            if (isim == "")
            {
                Console.WriteLine(" İsiminiz boş bırakılamaz. Lütfen İsim Giriniz.");
                goto isim_tekrar;
            }
            Console.WriteLine(" Soyisminizi Giriniz");
        soyisim_tekrar:
            string soyisim = Console.ReadLine();
            if (soyisim == "")
            {
                Console.WriteLine(" Soyisminiz boş bırakılamaz. Lütfen Soyisminizi Giriniz.");
                goto soyisim_tekrar;
            }

            Console.WriteLine(" E-posta Adresi Giriniz");
        email_tekrar:
            string email = Console.ReadLine();
            if (!EmailRegex(email))
            {
                Console.WriteLine(" E-Posta Adresi Hatalıdır.Lütfen Doğru bir E-Posta Adresi Giriniz");
                goto email_tekrar;
            }
            Console.WriteLine(" Lütfen 8 Karakterli Şifrenizi Giriniz.Şifreniz En Az Bir Harf,Bir Büyük ve Bir Küçük Harf İçermelidir.");
        sifre_tekrar:
            string sifre = Console.ReadLine();
            if (!IsPasswordValid(sifre))
            {
                Console.WriteLine(" Şifreniz Yeteri Kadar Güçlü Değildir. Lütfen 8 Karakterli ve İçerisinde En Az Bir Harf,Bir Büyük ve Bir Küçük Harf İçeren Bir Şifre Giriniz");
                goto sifre_tekrar;

            }
            MSConnection connection = new MSConnection();
            connection.connect();
            connection.query_string = "INSERT INTO users(User_Auth,User_Name,User_SurName,User_Mail,User_Password) VALUES ('" + yetki + "','" + isim + "','" + soyisim + "','" + email + "','" + sifre + "');";
            connection.execute();

            Console.WriteLine("Üye Başarıyla Oluşturuldu.Devam Etmek İçin Enter Tuşuna Basın.");
            Console.Read();
            return;


        }

        //Hazır Email Regex'i Kaynak = http://www.aspmvcnet.com/tr/m/csharp/c-regex-email-address-c-mail-kontrolu.html

        bool EmailRegex(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
            + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;

        }
        //Hazır 8 Karakterli içinde bir küçük, bir büyük ve bir harf içermesi zorunludur. Kaynak = https://kodekrani.com/makale/c-sharp-regex-kullanici-adi-ve-sifre
        bool IsPasswordValid(string password)
        {
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@!%*?+#&'()[=\"€])[A-Za-z\\d$@!%*?+#&'()[=\"€']{8,}");
            return regex.IsMatch(password);
        }



    }
}
