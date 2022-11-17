# ElasticSearchSample

MySql kurulumu
Network oluşturma
Öncelikle “net_mysqldb” adında bir network oluşturuyorum. Çünkü uygulamalar veritabanımıza bu network üzerinden ulaşacak. Network oluşturmak için:
<code> docker network create net_mysqldb</code>
Volume oluşturma
Veritabanı verilerinin saklanacağı “vol_mysqldb” adında bir volume oluşturuyorum. Oluşturmak için:
 <code> docker volume create vol_mysqldb</code>
Veritabanının hazırlanması:

<code>docker run -d --name=mysqldb  --network net_mysqldb --network-alias mysql -v vol_mysqlddb:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=23tYR!jd1. -e MYSQL_DATABASE=users mysql:latest</code>

Docker run komutu ile bir konteyner ayağa kaldırdım.
--name ile konteynerıma isim verdim,
--network ile mysqldata network’üne bağladım,
--network-alias ile mysql adıyla haberleşmeyi sağladım,
-v ile konteynerımı mysqldata volume’una bağladım,
-e environment komutlaraı ile parola ve defauly veritabanımı belirledim 
Ve son olarak da mysql docker image’ını seçtim.

Docker desktop üzerinde mysqldb konteynerına tıklayıp loglarından işlemin tamamlanmasını takip ediyorum. Tamamlanana kadar beklemeliyiz.

Veritabanına bağlanmak 
mysqldb  konteynerinde mysql komutunu çalıştırmak için:
<code>docker exec -it mysqldb  mysql -p</code>

Komutu çalıştırınca kurulumdaki parolayı yazmanı isteyecektir.

<code>show databases;</code>
komutu sonrasında aşağıdaki görüntüyü alıyorsanız kurulumu başarı ile bitirdiniz demektir.
 

Veritabanını doldurmak:

Projemiz içerisinde bulununan dummy_users.json dosyasını lokalinize indirin. Erişimi kolay bir yere kopyalamanızı tavsiye ederim.

Aşağıdaki komut ile volume içerisine alıyoruz dosyayı
<code> docker cp E:/add_users.txt mysqldb:/add_users.txt </code>
 Konteynerda shell çalıştırmak için aşağıdaki komutu yazıyorum.
<code> docker exec -it mysqldb sh </code>

users.json dosyasının kopyalandığını doğrulamak için  “ls” komutunu çalıştırıyoruz.
  
Artık verileri içeri alabirim. Aşağıdaki komut ile içeri alıyorum.
<code docker exec -it mysqldb mysql -p </code> dedikten sonra parolamı girip <code> mysql> </code> imlecini bekliyorum.
Sonrasında <code>create database kodu </code>
<code>use users;</code>
<code>\. add_users.txt</code>

Elastic search kurulumu
<code>docker network create net_elastic</code> ile network oluşturuyorum.
<code>docker pull docker.elastic.co/elasticsearch/elasticsearch:8.5.1
</code> www.elastic.co sitesinden son sürümünü öğrenip Elasticsearch imajını çekiyorum.

Windows için kısa bir limit ayarı var onu yapıyorum:
<code> wsl -d docker-desktop</code>
<code>sysctl -w vm.max_map_count=262144</code>

Bu işlemden sonra Docker Destop duruyor bu yüzden Restart ediyorum.


<code>docker run --name elasticsearch --net net_elastic -p 9200:9200 -p 9300:9300 -t docker.elastic.co/elasticsearch/elasticsearch:8.5.1</code> Elasticsearch için konteyner ayağa kaldırıyorum.


Ayağa kalktığında aşağıdaki gibi bir çıktı verecektir:
<code>
Elasticsearch security features have been automatically configured!
✅ Authentication is enabled and cluster connections are encrypted.

ℹ️  Password for the elastic user (reset with `bin/elasticsearch-reset-password -u elastic`):
  [A Password, you must note it.]

ℹ️  HTTP CA certificate SHA-256 fingerprint:
  [New http certificate]

ℹ️  Configure Kibana to use this cluster:
• Run Kibana and click the configuration link in the terminal when Kibana starts.
• Copy the following enrollment token and paste it into Kibana in your browser (valid for the next 30 minutes):
  [New token , you must copy and keep for 30 minutes]

ℹ️ Configure other nodes to join this cluster:
• Copy the following enrollment token and start new Elasticsearch nodes with `bin/elasticsearch --enrollment-token <token>` (valid for the next 30 minutes):

[New token , you must copy and keep for 30 minutes]

  
<code> docker cp elasticsearch:/usr/share/elasticsearch/config/certs/http_ca.crt .</code>

ile sertifikayı kopyaladım.

Kibana kurulumu:

<code> docker pull docker.elastic.co/kibana/kibana:8.5.1
</code> ile Kibana imajını çekiyorum. Genellikle sürümü ElasticSearch sürümü ile aynı olur.

<code> docker run --name kibana --net net_elastic -p 5601:5601 docker.elastic.co/kibana/kibana:8.5.1
</code> Kurulum bittikten sonra terminalde “Kibana has not been configured.

Go to http://0.0.0.0:5601/?code=322015 to get started.” benzeri bir ifade çıkacaktır.

Browser açıp adres çubuğuna aşağıdaki gibi yazdığınızda ayarları yapmanız için gerek adımlar açılacaktır.
<code>http://localhost:5601/?code=322015</code>

Açılan ekrana elasticsearch kurduktan sonra bana verilen enrollment_token I yapıştırdım ve Configure ElasticSearch butonuna bastım.
Sonra karşıma ElasticSearch giriş ekranı geldi. Kullanıcı adı <code>elastic</code> parola ise kurulumda verdiği parola ile devam ettim.

Bu sırada size bu aşamalarda karşılaştığım problemlerden bahsetmek istiyorum. 
1-	Docker içerisinde oluşan sertifikayı localhost tanımıyor. O sertifikayı da Windows’unuza import etmeniz gerekmekte.
2-	Bu size verdiği tokenların süresi 30 dk . Bu arada işlemleriniz uzun sürer vey ahata alırsanız tekrar oluşturmanızda fayda var. 
3-	Bazı işlemler için root yetkisi gerekiyor <code> docker exec -it -u root elasticsearch bash</code> komutunu kullanabilirsiniz.


