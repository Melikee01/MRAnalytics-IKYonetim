[README.md](https://github.com/user-attachments/files/24652146/README.md)
# MRAnalytics (İK Yönetim Sistemi)

Bu proje, şirket içi İnsan Kaynakları süreçlerini dijitalleştirmek amacıyla geliştirilmiş kapsamlı bir **C# Windows Forms** uygulamasıdır. Personel yönetimi, izin takibi ve idari onay süreçlerini N-Katmanlı mimari (N-Tier Architecture) yapısına uygun olarak ele alır.

## 📺 Proje Tanıtım Videosu

Projeyi detaylı olarak anlattığım YouTube videosuna aşağıdaki linkten ulaşabilirsiniz:

[![MRAnalytics Tanıtım](https://img.youtube.com/vi/VKV6y_BaAjA/0.jpg)](https://youtu.be/VKV6y_BaAjA)

👉 **Videoyu İzlemek İçin Tıklayın:** [https://youtu.be/VKV6y_BaAjA](https://youtu.be/VKV6y_BaAjA)

---

## 🚀 Özellikler

### 👥 Personel Yönetimi
- Personel ekleme, düzenleme ve listeleme.
- Kişisel bilgilerin güvenli bir şekilde saklanması.
- Rol bazlı yetkilendirme (Admin, İK, Personel, Kullanıcı).

### 📅 İzin Yönetimi (Öne Çıkan Özellik)
- **Talep Oluşturma:** Personel, tarih seçimi yaparak kolayca izin talebi oluşturabilir.
- **Onay Mekanizması:** Yöneticiler gelen talepleri "Onayla" veya "Reddet" seçenekleri ile anında değerlendirebilir.
- **Canlı Geri Bildirim:** Talep oluşturulduğunda veya onaylandığında arayüz donmadan anlık görsel geri bildirim (Yeşil/Kırmızı buton efektleri) verilir.
- **Arka Plan İşlemleri:** Veritabanı işlemleri "Background Worker" yapısı ile asenkron olarak çalışır, bu sayede arayüz asla donmaz.

### 🔐 Güvenlik ve Mimari
- **N-Katmanlı Mimari:** Sürdürülebilir kod yapısı için UI (Kullanıcı Arayüzü), BLL (İş Mantığı), DAL (Veri Erişimi) ve ENTITY katmanlarına ayrılmıştır.
- **MySQL Veritabanı:** Güçlü ve ölçeklenebilir veri saklama.
- **Asenkron Programlama:** `async/await` ve `Task` yapıları ile yüksek performans.

---

## 🛠️ Teknolojiler

- **Dil:** C# (.NET Framework)
- **Arayüz:** Windows Forms (WinForms)
- **Veritabanı:** MySQL
- **ORM/Veri Erişim:** ADO.NET (MySql.Data)

## 📥 Kurulum

1. Projeyi bilgisayarınıza indirin.
2. `IKYonetim.sln` dosyasını Visual Studio ile açın.
3. MySQL veritabanı ayarlarını `BaglantiDAL.cs` içerisinden kendi sunucunuza göre yapılandırın.
4. Projeyi derleyip çalıştırın.

---
*Geliştirici: Melike Arı*
