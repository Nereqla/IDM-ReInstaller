# IDM ReInstaller

Bu basit bir konsol uygulamasıdır ve yönetici ayrıcalıklarıyla çalıştırıldığında, Internet Download Manager (IDM) yazılımının bazı kayıt bilgilerini ve ayarlarını temizlemeyi amaçlar.

**Uyarı:** Bu araç, (IDM) yazılımının lisanslama koşullarını etkileyebilecek potansiyel sonuçlara sahip olabilir. Bu tür bir kullanım, IDM'in kullanım şartlarına aykırı olabilir ve yasal sonuçları olabilir. Bu uygulamayı kullanmadan önce ilgili şartları gözden geçirmeniz önemlidir.

## Nasıl Çalışır?

Uygulama aşağıdaki işlemleri gerçekleştirir:

1.  **IDM Proseslerini Sonlandırır:** Çalışan tüm IDM ile ilgili prosesleri (IDMan.exe, IEMonitor.exe vb.) kapatır.
2.  **Kayıt Defteri Temizliği:** IDM'in deneme süresiyle ilişkili olabilecek belirli kayıt defteri anahtarlarını siler.
3.  **Eski Ayar Dosyalarını Silme:** IDM'in uygulama veri klasöründe bulunan `DMCache` adlı eski ayar yedekleme klasörünü siler.
4.  **Kayıt Bilgilerini Silme:** Mevcut kullanıcı ve yerel makine altındaki IDM kayıt defteri girdilerindeki belirli kayıt değerlerini (ad, soyad, e-posta, seri numarası vb.) siler.

Uygulama, komut satırından çalışır ve isteğe bağlı olarak `-hidden` parametresi ile çalıştırıldığında konsol penceresini gizleyebilir.

**Not:** Uygulamanın sistemde değişiklik yapabilmesi için yönetici olarak çalıştırılması gerekmektedir.

## Kullanım

1.  Uygulamanın çalıştırılabilir dosyasını (`.exe`) yönetici olarak çalıştırın.
2.  Uygulama, gerçekleştirdiği işlemleri konsol ekranında gösterecektir (`-hidden` parametresi kullanılmadıysa).
3.  İşlem tamamlandığında, "Bitti" mesajı görüntülenecektir. Herhangi bir tuşa basarak uygulamayı kapatabilirsiniz (`-hidden` parametresi kullanılmadıysa).

**Gizli Çalıştırma:** Uygulamayı konsol penceresi görünmeden çalıştırmak için komut satırında veya bir kısayol aracılığıyla `-hidden` parametresini ekleyebilirsiniz:


## Sorumluluk Reddi

BU YAZILIM TAMAMEN HOBİ AMAÇLI GELİŞTİRİLMİŞTİR. KULLANIMI İLE İLGİLİ HER TÜRLÜ SORUMLULUK KULLANICIYA AİTTİR. YAZAR VEYA TELİF HAKKI SAHİPLERİ, BU YAZILIMIN KULLANIMINDAN KAYNAKLANABİLECEK HİÇBİR DOĞRUDAN VEYA DOLAYLI ZARARDAN, VERİ KAYBINDAN VEYA YAZILIMIN BEKLENEN ŞEKİLDE ÇALIŞMAMASINDAN DOLAYI SORUMLU TUTULAMAZ.

BU YAZILIMIN, ÜÇÜNCÜ ŞAHISLARIN HAKLARINI İHLAL EDEBİLECEK ŞEKİLDE KULLANILMASI KESİNLİKLE ÖNERİLMEZ. KULLANICILARIN YÜRÜRLÜKTEKİ YASALARA VE YAZILIM LİSANS KOŞULLARINA UYGUN HAREKET ETMESİ GEREKMEKTEDİR.
