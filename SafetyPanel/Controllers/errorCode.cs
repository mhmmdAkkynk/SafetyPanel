using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafetyPanel.Controllers
{
    public class errorCode
    {
        public List<KeyValuePair<string, string>> errorCodesValue(int result, int value, int message)
        {
            Dictionary<int, string> results = new Dictionary<int, string>
            {
                {0, "Success"},
                {1, "Unsuccess"}
            };

            Dictionary<int, string> values = new Dictionary<int, string>
            {
                {0, "Ok" },
                {1, "Not" },
            };

            Dictionary<int, string> errorCodes = new Dictionary<int, string>
            {
                {0 , "Kullanıcı Kaydı Başarılı!" },
                {1 , "Şifreler Aynı Olmalı!" },
                {2 , "Email Adresi Kayıtlı!" },
                {3 , "Lütfen Tüm Alanları Doldurunuz!" },
                {4 , "Kullanıcı Girişi Başarılı!" },
                {5 , "Kullanıcı Adı veya Şifre Doğru Değil!" },
                {6 , "Geçerli bir Email adresi giriniz!" },
                {7 , "Doğrulama Kodu Email Adresine Gönderildi!" },
                {8 , "Doğrulama Kodu Doğru!" },
                {9 , "Doğrulama Kodu Hatalı!" },
                {10 , "Şimdiki Şifre Yanlış!" },
                {11 , "Şifre Yenileme Başarılı!" },
                {12 , "Aktivasyon İşlemi Başarısız!" },
                {13 , "Hesabınız aktif değil. Aktivasyon işlemi için size bir E-posta gönderdik!" },
                {14 , "Bu Email adresi kayıtlı değil!" },
                {15 , "Böyle bir kayıt bulunamadı!" },
                {16 , "Güncelleme İşlemi Başarılı!" },
                {17 , "Güncelleme İşlemi Başarısız!" }
            };

            Dictionary<string, string> transaction = new Dictionary<string, string>
            {
                {"Result" , results[result]},
                {"Value" , values[value]},
                {"Message" , errorCodes[message]}
            };


            var returnValue = new List<KeyValuePair<string, string>>();


            foreach (var item in transaction)
            {
                returnValue.Add(new KeyValuePair<string, string>(item.Key, item.Value));
            }

            return returnValue;
        }
}
}