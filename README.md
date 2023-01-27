# AppointmentSchedulerSystem


Yazılım dünyasında gerçekleştirmiş olduğum ilk projemdir. Bu sektörde devam etmek için istikrarlı bir şekilde çalışmalarıma devam etmekteyim. Projeden bahsetmek gerekirse,
açılış ekranında login ekranı karşılıyor ve login olmayan kullanıcılar diğer sayfalara ulaşamıyor. Login olmak için kayıt kullanıcılar kayıt oluşturur ve kayıt oluşturduktan sonra 
Email onayı almadan hesaplarına giriş yapamazlar. Eposta adreslerine giden email doğrulama linkine tıklayarak onay işlemini yaptıktan sonra kullanıcı paneline giriş yapabilirler.
Giriş yapan kullanıcılar klasik CRUD işlemler ile randevu oluşturup, düzenleyip ve silme işlemleri yapabilirler. Sistem üzerinden günde 1 defa çalışan bacground servisi vardır.
Randevu hatırlatma sistemi için çalışıyor ve randevuya 1 gün kala hatırlatma maili gönderiyor. Aynı şekilde arka planda çalışan bir diğer servis ise her dakika çalışıp
zamanı geçen randevuların veritabanındaki karşılığını yani isdeleted kısmını otomatik olarak true yapıyor ve bu sayede kullanıcılar geçmiş randevularını güncel randevu ekranında göremiyorlar.
Kullanıcıların şifresini unutması halinde ise yine email servisi ile şifremi unuttum bölümü üzerinden yeni şifre oluşturmak için bağlantı gönderiliyor ve bu bağlantı ile birlikte yeni bir şifre oluşturup hesaplarına ulaşabiliyorlar.

------------------------------



It is my first project in the software world. I continue to work steadily to continue in this sector. To talk about the project,
The login screen appears on the splash screen and non-login users cannot access other pages. To login, users create registration and after creating registration
They cannot log into their accounts without receiving email confirmation. They can log in to the user panel after completing the confirmation process by clicking on the email verification link that goes to their email address.
Login users can create, edit and delete appointments with classic CRUD operations. There is a bacground service that runs once a day through the system. It works for the appointment reminder system and sends a reminder e-mail 1 day before the appointment. Likewise, another service running in the background is running every minute.
It automatically sets the isdeleted part of the overdue appointments to true in the database, so that users cannot see their past appointments on the current appointment screen.
If users forget their password, a link is sent to create a new password via the forgot password section via the email service, and they can create a new password and access their accounts with this link.
